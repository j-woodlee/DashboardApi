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

        [HttpGet("{projectName:length(4)}", Name = "GetJenkins")]
        public ActionResult<Jenkins> Get(string projectName) {
            var jenkins = this._jenkinsService.Get(projectName);

            if (jenkins == null) {
                return NotFound();
            }

            return jenkins;
        }

        [HttpPost]
        public ActionResult<Jenkins> Create(Jenkins jenkins) {
            this._jenkinsService.Create(jenkins);

            return CreatedAtRoute("GetJenkins", new { projectName = jenkins.ProjectName.ToString() }, jenkins);
        }

        [HttpPut("{projectName:length(4)}")]
        public IActionResult Update(string projectName, Jenkins jenkinsIn) {
            var jenkins = this._jenkinsService.Get(projectName);

            if (jenkins == null) {
                return NotFound();
            }

            this._jenkinsService.Update(projectName, jenkinsIn);

            return NoContent();
        }

        [HttpDelete("{projectName:length(4)}")]
        public IActionResult Delete(string projectName) {
            var jenkins = this._jenkinsService.Get(projectName);

            if (jenkins == null) {
                return NotFound();
            }

            this._jenkinsService.Remove(jenkins.ProjectName);

            return NoContent();
        }
    }
}