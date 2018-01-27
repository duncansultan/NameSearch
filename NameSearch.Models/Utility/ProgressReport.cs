using System;

namespace NameSearch.Models.Utility
{
    public class ProgressReport : IProgressReport
    {
        public bool IsRunning { get; set; }
        public TimeSpan ElapsedTimeSpan { get; set; }
        public int ProgressCount { get; set; }
        public int TotalCount { get; set; } = 1;
        public double PercentRemaining { get; set; } = 0.00;
        public string Message { get; set; }
        public void IncrementCount()
        {
            ProgressCount++;
        }
        public void UpdatePercentRemaining()
        {
            if (TotalCount < 1)
            {
                TotalCount = 1;
            }
            PercentRemaining = ProgressCount / TotalCount;
        }
        public void UpdateRemaining()
        {
            ProgressCount++;
            PercentRemaining = ProgressCount / TotalCount;
        }
    }
}
