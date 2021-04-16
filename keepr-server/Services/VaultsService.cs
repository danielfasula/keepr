using System;
using System.Collections.Generic;
using System.Linq;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vrepo;

        public VaultsService(VaultsRepository vrepo)
        {
            _vrepo = vrepo;
        }

        internal IEnumerable<Vault> GetAll()
        {
            IEnumerable<Vault> vaults = _vrepo.GetAll();
            return vaults.ToList().FindAll(v => v.IsPrivate);
        }

        internal Vault GetById(int id, string userId)
        {
            Vault data = _vrepo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid ID");
            }
            else if (data.IsPrivate == true && userId != data.CreatorId)
            {
                throw new Exception("Access Denied: This Vault is Private");
            }
            return data;

        }

        internal Vault Create(Vault newVault)
        {
            return _vrepo.Create(newVault);
        }

        internal Vault Edit(Vault editData, string userId)
        {
            Vault original = _vrepo.GetById(editData.Id);
            if (original.CreatorId != userId)
            {
                throw new Exception("You must be the Creator to Edit this");
            }
            editData.Name = editData.Name == null ? original.Name : editData.Name;
            editData.Description = editData.Description == null ? original.Description : editData.Description;
            editData.IsPrivate = editData.IsPrivate == original.IsPrivate ? original.IsPrivate : editData.IsPrivate;

            return _vrepo.Edit(editData);
        }

        internal object GetVaultsByProfileId(string id)
        {
            IEnumerable<Vault> vaults = _vrepo.GetByCreatorId(id);
            return vaults.ToList().FindAll(v => v.IsPrivate == false);
        }

        internal object GetVaultsByAccountId(string id)
        {
            return _vrepo.GetByCreatorId(id);
        }

        internal string Delete(int id, string userId)
        {
            Vault original = _vrepo.GetById(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("You must be the Creator to Delete this");
            }
            _vrepo.Delete(id);
            return "Successfully deleted";
        }
    }
}