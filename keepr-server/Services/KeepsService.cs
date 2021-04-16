using System;
using System.Collections.Generic;
using System.Linq;
using keepr_server.Models;
using keepr_server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace keepr_server.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _krepo;

        public KeepsService(KeepsRepository krepo)
        {
            _krepo = krepo;
        }

        internal IEnumerable<Keep> GetAll()
        {
            return _krepo.GetAll();
        }

        internal Keep GetById(int id)
        {
            Keep data = _krepo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid ID");
            }
            return data;
        }

        internal Keep Create(Keep newKeep)
        {
            return _krepo.Create(newKeep);
        }

        internal Keep Edit(Keep editData, string userId)
        {
            Keep original = GetById(editData.Id);
            if (original.CreatorId != userId)
            {
                throw new Exception("You must be the Creator to Edit this");
            }
            editData.Name = editData.Name == null ? original.Name : editData.Name;
            editData.Description = editData.Description == null ? original.Description : editData.Description;
            editData.Img = editData.Img == null ? original.Img : editData.Img;

            return _krepo.Edit(editData);
        }

        internal IEnumerable<Keep> GetKeepsByAccountId(string id)
        {
            return _krepo.GetByCreatorId(id);
        }

        internal IEnumerable<Keep> GetKeepsByProfileId(string id)
        {
            return _krepo.GetByCreatorId(id);
        }

        internal string Delete(int id, string userId)
        {
            Keep original = GetById(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("You must be the Creator to Delete this");
            }
            _krepo.Delete(id);
            return "Successfully deleted";
        }

        internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            return _krepo.GetByVaultId(id);
        }
    }
}