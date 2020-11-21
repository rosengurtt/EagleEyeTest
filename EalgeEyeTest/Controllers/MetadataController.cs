using EalgeEyeTest.Models;
using EalgeEyeTest.Repository;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;

namespace EalgeEyeTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetadataController : ControllerBase
    {
        private IBackendRepository BackendRepository;

        public MetadataController(IBackendRepository BackendRepository)
        {
            this.BackendRepository = BackendRepository;
        }

        [HttpGet("{movieId}")]
        public ActionResult<List<Movie>> GetMetadataForMovie(int movieId)
        {
            var movies = BackendRepository.GetMetadataForMovie(movieId);

            if (movies==null) return new NotFoundResult();

            return Ok(movies);
        }

        [HttpPost]
        public ActionResult<Movie> AddMetadata(Movie movie)
        {
            if (ModelState.IsValid)
            {
                Log.Information($"A movie with title {movie.Title} has been added");

                return Ok(movie);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
