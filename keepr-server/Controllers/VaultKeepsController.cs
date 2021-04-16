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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vkserv;

        public VaultKeepsController(VaultKeepsService vkserv)
        {
            _vkserv = vkserv;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> CreateAsync([FromBody] VaultKeep newVK)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVK.CreatorId = userInfo.Id;
                return Ok(_vkserv.Create(newVK));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                _vkserv.Delete(id, userInfo.Id);
                return Ok("deleted");
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}