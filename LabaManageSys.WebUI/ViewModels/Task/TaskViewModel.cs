using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.ViewModels.Task
{
    public class TaskViewModel
    {
        public TaskModel Task { get; set; }
        
        public int[] PercentFeature { get; set; }

        public int RatingsCount { get; set; }

        public double AvgRating { get; set; }
    }
}