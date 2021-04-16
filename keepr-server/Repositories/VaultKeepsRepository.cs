using System;
using System.Data;
using Dapper;
using keepr_server.Models;

namespace keepr_server.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }



        internal VaultKeep Create(VaultKeep newVK)
        {
            string sql = @"
            INSERT INTO vaultkeeps 
            (id, vaultId, keepId, creatorId) 
            VALUES 
            (@Id, @vaultId, @keepId, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newVK);
            newVK.Id = id;
            return newVK;
        }

        internal VaultKeep GetById(int id)
        {
            string sql = @"
            SELECT * FROM vaultkeeps WHERE id = @id;
            ";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}