using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;
using keepr_server.Models;
using keepr_server.Services;

namespace keepr_server.Controllers
{
    [ApiController]
    [Route("[controller]")]

    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ProfilesService _pserv;
        private readonly KeepsService _kserv;
        private readonly VaultsService _vserv;

        public AccountController(ProfilesService pserv, KeepsService kserv, VaultsService vserv)
        {
            _pserv = pserv;
            _kserv = kserv;
            _vserv = vserv;
        }

        [HttpGet]
        public async Task<ActionResult<Profile>> GetAsync()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_pserv.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("keeps")]
        public async Task<ActionResult<IEnumerable<Keep>>> GetKeepsByAccountId()
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            return Ok(_kserv.GetKeepsByAccountId(userInfo.Id));
        }

        [HttpGet("vaults")]
        public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsByAccountId()
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            return Ok(_vserv.GetVaultsByAccountId(userInfo.Id));
        }
    }
}