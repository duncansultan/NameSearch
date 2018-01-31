using System;

namespace NameSearch.Models.Utility.Interfaces
{
    public interface IProgressReport
    {
        bool IsRunning { get; set; }
        TimeSpan ElapsedTimeSpan { get; set; }
        int ProgressCount { get; set; }
        int TotalCount { get; set; }
        double PercentRemaining { get; set; }
        string Message { get; set; }
        void IncrementCount();
        void UpdatePercentRemaining();
        void UpdateRemaining();
    }
}
