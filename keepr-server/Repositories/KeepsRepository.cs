using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace keepr_server.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Keep> GetAll()
        {
            string sql = @"
            SELECT
            k.*,
            p.*
            FROM keeps k
            JOIN profiles p ON k.creatorId = p.id;
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, splitOn: "id");
        }

        internal Keep GetById(int id)
        {
            string sql = @"
            UPDATE keeps k
            SET k.views = k.views + 1
            WHERE k.id = @id;
            SELECT
            k.*,
            p.*
            FROM keeps k
            JOIN profiles p ON k.creatorId = p.id
            WHERE k.id = @id;
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Keep Create(Keep newKeep)
        {
            string sql = @"
            INSERT INTO keeps
            (id, creatorId, name, description, img, views, shares, keeps)
            VALUES
            (@Id, @CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newKeep);
            newKeep.Id = id;

            return newKeep;
        }

        internal IEnumerable<Keep> GetByCreatorId(string id)
        {
            string sql = @"
            SELECT
            k.*,
            p.*
            FROM keeps k
            JOIN profiles p ON k.creatorId = p.id
            WHERE k.creatorId = @id;
            ";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
           {
               keep.Creator = profile;
               return keep;
           }, new { id }, splitOn: "id");
        }

        internal Keep Edit(Keep editData)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            img = @Img
            WHERE id = @Id;
            ";
            _db.Execute(sql, editData);
            return editData;
        }

        internal IEnumerable<VaultKeepViewModel> GetByVaultId(int id)
        {
            string sql = @"
            SELECT
            k.*,
            vk.id AS VaultKeepId,
            p.*
            FROM vaultkeeps vk
            JOIN keeps k ON k.id = vk.keepId
            JOIN profiles p ON p.id = k.creatorId
            WHERE vaultId = @id;
            ";
            return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }, splitOn: "id");
        }

        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM keeps WHERE id = @id LIMIT 1;
            ";
            _db.Execute(sql, new { id });
        }
    }
}