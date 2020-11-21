using EalgeEyeTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EalgeEyeTest.Repository
{
    public class BackendRepository : IBackendRepository
    {
        private static List<Movie> movies = new List<Movie>();
        private static List<Stats> stats = new List<Stats>();
        public BackendRepository()
        {
            LoadData();
        }

        private void LoadData()
        {
            var appDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            var moviesPath = Path.Combine(appDirectory, "Data/metadata.csv");
            var statsPath = Path.Combine(appDirectory, "Data/stats.csv");

            // Load movies
            using (var reader = new StreamReader(moviesPath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    // Ignore headers line
                    if (line == "Id,MovieId,Title,Language,Duration,ReleaseYear") continue;

                    // We use regular expressions for the case where there are commas inside a text field
                    Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                    var values = CSVParser.Split(line);

                    // If not all values provided ignore this entry
                    if (values.Length < 6) continue;
                    var movie = new Movie
                    {
                        Id = int.Parse(values[0]),
                        MovieId = int.Parse(values[1]),
                        Title = values[2],
                        Language = values[3],
                        Duration = values[4],
                        ReleaseYear = int.Parse(values[5])
                    };
                    movies.Add(movie);
                }
            }

            // Load stats
            // Load movies
            using (var reader = new StreamReader(statsPath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    // Ignore headers line
                    if (line == "movieId,watchDurationMs") continue;
                    var values = line.Split(',');
                    var stat = new Stats
                    {
                        MovieId = int.Parse(values[0]),
                        watchDurationMs = long.Parse(values[1])
                    };
                    stats.Add(stat);
                }
            }
        }



        public IEnumerable<Movie> GetMetadataForMovie(int movieId)
        {
            var movieVersions = movies.Where(x => x.MovieId == movieId).ToList();
            // If no movies found, return 404
            if (movieVersions.Count == 0) return null;

            // In case there are several records for the same language, select the latest
            var highestIds = movieVersions.GroupBy(i => (i.MovieId, i.Language))
                .Select(g => g.Max(row => row.Id)).ToList();

            // Order by language
            return movieVersions.Where(x => highestIds.Contains(x.Id))
                .OrderBy(y => y.Language);
        }

        public List<StatsItem> GetStats()
        {
            var mostWatched = stats
                // Filter out movies for which we have no data
            .Where(x=> movies.Where(y=>y.MovieId==x.MovieId).ToList().Count>0)
            .GroupBy(n => n.MovieId)
            .Select(n => new StatsItem
            {
                MovieId = n.Key,
                Title =  movies.Where(x => x.MovieId == (int)n.Key).FirstOrDefault().Title,
                Watches = n.Count(),
                AverageWatchDuration = (int)Math.Round(n.Average(p => p.watchDurationMs)),
                ReleaseYear = movies.Where(x => x.MovieId == (int)n.Key).FirstOrDefault().ReleaseYear
            }
            )
            .OrderByDescending(n => (n.Watches, n.ReleaseYear)).ToList();

            return mostWatched;
        }
    }
}