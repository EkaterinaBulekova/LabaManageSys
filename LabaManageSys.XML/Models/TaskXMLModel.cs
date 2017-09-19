using System.ComponentModel.DataAnnotations;

namespace LabaManageSys.XML.Models
{
    public class TaskXMLModel
    {
        [Required]
        public int TaskId { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
