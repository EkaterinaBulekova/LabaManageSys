using System.ComponentModel.DataAnnotations;
using LabaManageSys.Domain.EntitiesModel;

namespace LabaManageSys.WebUI.Models
{
    public class RoleModel
    {
        public RoleModel()
        {
        }

        public RoleModel(Role role)
        {
            this.RoleId = role.RoleId;
            this.Name = role.Name;
        }

        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}