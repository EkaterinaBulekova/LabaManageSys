using System.Collections.Generic;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.ViewModels.TeachPlan
{
    public class ListViewModel
    {
        public IEnumerable<CourseModel> Courses { get; set; }
    }
}