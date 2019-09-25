using DashboardApi.Models;
using DashboardApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DashboardApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class JenkinsController : ControllerBase {
        private readonly JenkinsService _jenkinsService;

        public JenkinsController(JenkinsService jenkinsService) {
            this._jenkinsService = jenkinsService;
        }

        [HttpGet]
        public ActionResult<List<Jenkins>> Get() =>
            this._jenkinsService.Get();

        [HttpGet("{id:length(24)}", Name = "GetJenkins")]
        public ActionResult<Jenkins> Get(string id) {
            var jenkins = this._jenkinsService.Get(id);

            if (jenkins == null)
            {
                return NotFound();
            }

            return jenkins;
        }

        [HttpPost]
        public ActionResult<Jenkins> Create(Jenkins jenkins) {
            this._jenkinsService.Create(jenkins);

            return CreatedAtRoute("GetJenkins", new { id = jenkins.Id.ToString() }, jenkins);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Jenkins jenkinsIn) {
            var jenkins = this._jenkinsService.Get(id);

            if (jenkins == null) {
                return NotFound();
            }

            this._jenkinsService.Update(id, jenkinsIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id) {
            var jenkins = this._jenkinsService.Get(id);

            if (jenkins == null) {
                return NotFound();
            }

            this._jenkinsService.Remove(jenkins.Id);

            return NoContent();
        }
    }
}