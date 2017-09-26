using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaManageSys.Domain.EntitiesModel
{
    public class Tag
    {
        public Tag()
        {
            this.Tasks = new HashSet<Task>();
        }

        public int TagId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
