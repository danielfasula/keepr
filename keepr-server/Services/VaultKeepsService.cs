using System;
using keepr_server.Models;
using keepr_server.Repositories;

namespace keepr_server.Services
{
    public class VaultKeepsService
    {

        private readonly VaultKeepsRepository _vkrepo;
        public VaultKeepsService(VaultKeepsRepository vkrepo)
        {
            _vkrepo = vkrepo;
        }

        internal VaultKeep Create(VaultKeep newVK)
        {
            //TODO if they are creating a wishlistproduct, make sure they are the creator of the list
            return _vkrepo.Create(newVK);

        }

        internal void Delete(int id, string userId)
        {
            VaultKeep vk = _vkrepo.GetById(id);
            if (vk == null)
            {
                throw new Exception("Invalid Id");
            }
            if (vk.CreatorId != userId)
            {
                throw new Exception("Invalid User");
            }
            _vkrepo.Delete(id);
        }
    }
}