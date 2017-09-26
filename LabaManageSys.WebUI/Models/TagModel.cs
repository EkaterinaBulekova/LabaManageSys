using LabaManageSys.Domain.EntitiesModel;

namespace LabaManageSys.WebUI.Models
{
    public class TagModel
    {
        public TagModel()
        {
        }

        public TagModel(Tag tag)
        {
            this.TagId = tag.TagId;
            this.Name = tag.Name;
        }

        public int TagId { get; set; }

        public string Name { get; set; }
    }
}