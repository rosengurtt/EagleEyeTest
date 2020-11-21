using EalgeEyeTest.Models;
using EalgeEyeTest.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EalgeEyeTest.Controllers
{
    [ApiController]
    [Route("movies/stats")]
    public class StatsController : ControllerBase
    {
        private IBackendRepository BackendRepository;

        public StatsController(IBackendRepository BackendRepository)
        {
            this.BackendRepository = BackendRepository;
        }
        [HttpGet]
        public ActionResult<List<Movie>> GetStats(int movieId)
        {

            return Ok(BackendRepository.GetStats());
        }
    }
}
