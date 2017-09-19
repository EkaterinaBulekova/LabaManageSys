using System.Collections.Generic;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.ViewModels.Task
{
    public class ListJsonModel
    {
        public int TotalPages { get; set; }

        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}