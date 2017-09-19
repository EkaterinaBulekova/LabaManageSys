using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;
using LabaManageSys.XML.Abstract;
using LabaManageSys.XML.Models;

namespace LabaManageSys.WebUI.Controllers
{
    [Authorize(Roles = "Teachers")]
    public class TaskLoadController : Controller
    {
        private IxmlProcessor processor;
        private ITasksRepository repository;

        public TaskLoadController(IxmlProcessor proc, ITasksRepository repo)
        {
            this.processor = proc;
            this.repository = repo;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Downloadfile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
            string fileName = "tasks.xml";

            var tasks = this.repository.Tasks.Select(_ => 
            new TaskXMLModel
            {
                TaskId = _.TaskId,
                Author = _.Author,
                Level = _.Level,
                Topic = _.Topic,
                Name = _.Name,
                Text = _.Text
            }).ToList();
            this.processor.DownloadTasksToFile(tasks, path + fileName);
            return this.File(path + fileName, "text/xml", fileName);
        }

        [HttpPost]
        public ActionResult Uploadfile(HttpPostedFileBase xmlfile = null)
        {
            if (xmlfile != null && xmlfile.ContentLength > 0 && xmlfile.ContentType == "text/xml")
            {
                var tasks = this.processor.UploadTasksFromFile(xmlfile.InputStream);
                var result = this.repository.TaskAddAll(tasks.Select(_ =>
                new TaskModel
                {
                    TaskId = _.TaskId,
                    Author = _.Author,
                    Level = _.Level,
                    Topic = _.Topic,
                    Name = _.Name,
                    Text = _.Text
                }).ToList());
                var innerMessage = (!result.IsSucceed) ? string.Format(
                    ". Не сохранены следующие задания: {0}",
                    string.Join(",", result.NonCommited.Select(_ => _))) : string.Empty;
                this.TempData["message"] = string
                    .Format("Загружены {0} из {1} заданий{2}.", result.SucceededItems, tasks.Count(), innerMessage);
                return this.RedirectToAction("List", "TaskManage");
            }
            else
            {
                this.TempData["message"] = "Задания не были загружены! Неверный формат файла.";
                return this.RedirectToAction("Index");
            }
        }
    }
}