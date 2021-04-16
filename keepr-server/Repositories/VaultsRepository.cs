using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr_server.Models;

namespace keepr_server.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Vault> GetAll()
        {
            string sql = @"
            SELECT
            v.*,
            p.*
            FROM vaults v
            JOIN profiles p ON v.creatorId = p.id;
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, splitOn: "id");
        }

        internal Vault GetById(int id)
        {
            string sql = @"
            SELECT
            v.*,
            p.*
            FROM vaults v
            JOIN profiles p ON v.creatorId = p.id
            WHERE v.id = @id;
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Vault Create(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults
            (id, creatorId, name, description, isPrivate)
            VALUES
            (@Id, @CreatorId, @Name, @Description, @IsPrivate);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newVault);
            newVault.Id = id;

            return newVault;
        }

        internal Vault Edit(Vault editData)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description,
            isPrivate = @IsPrivate
            WHERE id = @Id;
            ";
            _db.Execute(sql, editData);
            return editData;
        }

        internal IEnumerable<Vault> GetByCreatorId(string id)
        {
            string sql = @"
            SELECT
            v.*,
            p.*
            FROM vaults v
            JOIN profiles p ON v.creatorId = p.id
            WHERE v.creatorId = @id;
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }, splitOn: "id");
        }

        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM vaults WHERE id = @id LIMIT 1;
            ";
            _db.Execute(sql, new { id });
        }
    }
}