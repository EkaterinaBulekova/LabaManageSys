using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaManageSys.Domain.EntitiesModel
{
    public class Rating
    {
        public int RatingId { get; set; }

        public int Evaluation { get; set; }

        public string Comment { get; set; }

        public int UserId { get; set; }

        public int TaskId { get; set; }

        public virtual AppUser User { get; set; }

        public virtual Task Task { get; set; }
    }
}
