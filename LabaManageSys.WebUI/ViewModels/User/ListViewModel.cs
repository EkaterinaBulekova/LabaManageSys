using System.Collections.Generic;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.ViewModels.User
{
    public class ListViewModel
    {
        public IEnumerable<UserModel> Users { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}