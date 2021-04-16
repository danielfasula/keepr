using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _pserv;
        private readonly KeepsService _kserv;
        private readonly VaultsService _vserv;

        public ProfilesController(ProfilesService pserv, KeepsService kserv, VaultsService vserv)
        {
            _pserv = pserv;
            _kserv = kserv;
            _vserv = vserv;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> Get(string id)
        {
            try
            {
                return Ok(_pserv.GetProfileById(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<IEnumerable<Keep>> GetKeepsByProfileId(string id)
        {
            try
            {
                return Ok(_kserv.GetKeepsByProfileId(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}/vaults")]
        public ActionResult<IEnumerable<Vault>> GetVaultsByProfileId(string id)
        {
            try
            {
                return Ok(_vserv.GetVaultsByProfileId(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}