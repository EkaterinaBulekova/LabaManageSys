using System.Collections.Generic;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.ViewModels.Task
{
    public class ComentListJson
    {
        public List<RatingModel> Ratings { get; set; }

        public RatingModel CurrentUserRating { get; set; }
    }
}