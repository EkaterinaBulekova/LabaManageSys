using LabaManageSys.Domain.EntitiesModel;

namespace LabaManageSys.WebUI.Models
{
    public class RatingModel
    {
        public RatingModel()
        {
        }

        public RatingModel(Rating rating)
        {
            if (rating != null)
            {
                this.RatingId = rating.RatingId;
                this.Evaluation = rating.Evaluation;
                this.Comment = rating.Comment ?? string.Empty;
                this.UserId = rating.UserId;
                this.TaskId = rating.TaskId;
                this.UserName = rating.User.Name;
            }
        }

        public int RatingId { get; set; }

        public int Evaluation { get; set; }

        public string Comment { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int TaskId { get; set; }
    }
}