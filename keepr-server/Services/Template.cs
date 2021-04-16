// using System;
// using System.Collections;
// using System.Collections.Generic;
// using keepr_server.Models;
// using keepr_server.Repositories;

// namespace keepr_server.Services
// {
//     public class TemplatesService
//     {
//         private readonly TemplatesRepository _repo;

//         public TemplatesService(TemplatesRepository repo)
//         {
//             _repo = repo;
//         }

//         internal IEnumerable<Template> GetTemplatesByProfileId(string id)
//         {
//             return _repo.GetByProfileId(id);
//         }

//         internal IEnumerable<Template> GetAll()
//         {
//             return _repo.GetAll();
//         }

//         internal Template GetById(int id)
//         {
//             Template data = _repo.GetById(id);
//             if (data == null)
//             {
//                 throw new Exception("Invalid ID");
//             }
//             return data;
//         }

//         // NOTE in order to only get objects back where published equals true

//         // IENumerable<Template> template = _repo.GetAll()
//         // return template.ToList().FindAll(t => t.Published)

//         internal Template Create(Template newTemplate)
//         {
//             return _repo.Create(newTemplate);
//         }

//         internal Template Edit(Template updated)
//         {
//             Template data = GetById(updated.Id);
//             // NOTE add null checks for each property you'll allow the user to edit
//             // data.Title = updated.Title != null ? updated.Title : data.Title;

//             return _repo.Edit(data);
//         }

//         internal string Delete(int id, string userId)
//         {
//             GetById(id);
//             _repo.Delete(id, userId);
//             return "Template Deleted";
//         }
//     }
// }