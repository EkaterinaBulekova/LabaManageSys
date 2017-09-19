using System.ComponentModel.DataAnnotations;
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
    }
}