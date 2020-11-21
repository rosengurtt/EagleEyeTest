using EalgeEyeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EalgeEyeTest.Repository
{
    public interface IBackendRepository
    {
        public IEnumerable<Movie> GetMetadataForMovie(int movieId);
        List<StatsItem> GetStats();
    }
}
