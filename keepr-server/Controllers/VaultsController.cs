using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr_server.Models;
using keepr_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vserv;
        private readonly KeepsService _kserv;

        public VaultsController(VaultsService vserv, KeepsService kserv)
        {
            _vserv = vserv;
            _kserv = kserv;
        }

        [HttpGet]
        public ActionResult<Vault> Get()
        {
            try
            {
                return Ok(_vserv.GetAll());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> Get(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vserv.GetById(id, userInfo?.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> CreateAsync([FromBody] Vault newVault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVault.CreatorId = userInfo.Id;
                newVault.Creator = userInfo;
                Vault created = _vserv.Create(newVault);

                return Ok(created);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> EditAsync([FromBody] Vault editData, int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                editData.Id = id;
                editData.CreatorId = userInfo.Id;
                editData.Creator = userInfo;
                return Ok(_vserv.Edit(editData, userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> DeleteAsync(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vserv.Delete(id, userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<IEnumerable<VaultKeepViewModel>>> GetKeepsByVaultId(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                _vserv.GetById(id, userInfo?.Id);
                return Ok(_kserv.GetKeepsByVaultId(id));

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}