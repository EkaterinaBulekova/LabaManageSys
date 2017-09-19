using System.Collections.Generic;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.ViewModels.Role
{
    public class ListViewModel
    {
        public IEnumerable<RoleModel> Roles { get; set; }
    }
}