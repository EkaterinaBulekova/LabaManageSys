using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabaManageSys.Domain.EntitiesModel
{
    public class Role
    {
        public Role()
        {
            this.AppUsers = new HashSet<AppUser>();
        }

        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}