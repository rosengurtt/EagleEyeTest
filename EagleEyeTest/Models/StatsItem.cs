using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EalgeEyeTest.Models
{
    public class StatsItem
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Watches { get; set; }
        public double AverageWatchDuration { get; set; }

        public int ReleaseYear { get; set; }
    }
}
