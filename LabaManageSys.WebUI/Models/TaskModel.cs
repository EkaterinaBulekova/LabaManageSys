using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LabaManageSys.Domain.EntitiesModel;
using LabaManageSys.XML.Models;

namespace LabaManageSys.WebUI.Models
{
    public class TaskModel
    {
        public TaskModel()
        {
        }

        public TaskModel(Task task)
        {
            this.Author = task.Author;
            this.Level = task.Level;
            this.Name = task.Name;
            this.TaskId = task.TaskId;
            this.Text = task.Text;
            this.Topic = task.Topic;
            this.Tags = task.Tags.Select(_ => 
            new TagModel
            {
                TagId = _.TagId,
                Name = _.Name
            }).ToList();
            this.TagString = string.Join(", ", this.Tags.Select(_ => _.Name).ToList());
        }

        public TaskModel(TaskXMLModel task)
        {
            this.Author = task.Author;
            this.Level = task.Level;
            this.Name = task.Name;
            this.TaskId = task.TaskId;
            this.Text = task.Text;
            this.Topic = task.Topic;
        }

        [Required]
        public int TaskId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Уровень сложности")]
        public int Level { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Наименование предмета")]
        public string Topic { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Наименование задачи")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Текст задания")]
        public string Text { get; set; }

        public List<TagModel> Tags { get; set; }

        public string TagString { get; set; }
    }
}