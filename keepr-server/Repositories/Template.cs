// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using keepr_server.Models;
// using Dapper;

// namespace keepr_server.Repositories
// {
//     public class TemplatesRepository
//     {
//         private readonly IDbConnection _db;

//         public TemplatesRepository(IDbConnection db)
//         {
//             _db = db;
//         }
//         internal IEnumerable<Template> GetByProfileId(string id)
//         {
//             string sql = @"
//             SELECT
//             t.*,
//             p.*
//             FROM templates t
//             JOIN profiles p ON t.creatorId = p.id
//             WHERE creatorId = @id;
//             ";
//             return _db.Query<Template, Profile, Template>(sql, (template, profile) =>
//             {
//                 template.CreatorId = profile.Id;
//                 return template;
//             }, new { id }, splitOn: "id");
//         }

//         internal Template GetById(int id)
//         {
//             string sql = @"
//             SELECT
//             t.*,
//             p.*
//             FROM templates t
//             JOIN profiles p ON t.creatorId = p.id
//             WHERE b.id = @id;
//             ";
//             return _db.Query<Template, Profile, Template>(sql, (template, profile) =>
//             {
//                 template.Creator = profile;
//                 return template;
//             }, new { id }, splitOn: "id").FirstOrDefault();
//         }

//         internal IEnumerable<Template> GetAll()
//         {
//             string sql = @"
//             SELECT
//             t.*,
//             p.*
//             FROM templates t
//             JOIN profiles p ON t.creatorId = p.id
//             ";
//             return _db.Query<Template, Profile, Template>(sql, (template, profile) =>
//             {
//                 template.Creator = profile;
//                 return template;
//             }, splitOn: "id");
//         }
//         internal Template Create(Template newTemplate)
//         {
//             string sql = @"
//             INSERT INTO templates
//             ()
//             VALUES
//             ();
//             SELECT LAST_INSERT_ID();
//             ";
//             int id = _db.ExecuteScalar<int>(sql, newTemplate);
//             newTemplate.Id = id;
//             return newTemplate;
//         }

//         internal Template Edit(Template data)
//         {
//             string sql = @"
//             UPDATE templates
//             SET
//             WHERE id = @Id;
//             SELECT * FROM templates WHERE id = @Id;
//             ";
//             Template returnTemplate = _db.QueryFirstOrDefault<Template>(sql, data);
//             return returnTemplate;
//         }

//         internal void Delete(int id, string userId)
//         {
//             string sql = @"
//             DELETE FROM templates WHERE id = @id AND creatorId = @userId LIMIT 1;
//             ";
//             _db.Execute(sql, new { id, userId });
//         }
//     }
// }