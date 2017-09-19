using System.Collections.Generic;
using System.Linq;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.ViewModels.Task
{
    public class ListViewModel
    {
        public IEnumerable<TaskViewModel> Tasks { get; set; }

        public FilterListsModel Lists { get; set; }

        public FilterModel Filter { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public void SetTotalItems()
        {
            this.PagingInfo.TotalItems = this.Tasks.Count();
        }
    }
}