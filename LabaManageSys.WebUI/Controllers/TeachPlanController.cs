using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LabaManageSys.WebUI.Abstract;
using LabaManageSys.WebUI.Models;
using LabaManageSys.WebUI.ViewModels.TeachPlan;

namespace LabaManageSys.WebUI.Controllers
{
    [Authorize(Roles = "Teachers")]
    public class TeachPlanController : Controller
    {
        private ILessonsRepository repository;
        
        public TeachPlanController(ILessonsRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult List()
        {
            ListViewModel model = new ListViewModel { Courses = this.repository.Courses };
            return this.View(model);
        }

        public ActionResult Edit(int courseId)
        {
            var dates = this.repository.GetDatesByCourse(courseId).Select(x => x.ToString("yyyy-MM-dd hh:mm:ss")).ToArray();
            EditViewModel course = new EditViewModel
            {
                Course = this.repository.GetCourseById(courseId),
                Dates = string.Join(",", dates)
            };
            return this.View(course);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel courseModel)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (ModelState.IsValid)
            {
                this.repository.CourseUpdate(courseModel.Course, courseModel.GetArrayDates());
                this.TempData["message"] = string.Format("Изменения в курсе \"{0}\" были сохранены", courseModel.Course.Name);
                return this.RedirectToAction("List");
            }
            else
            {
                return this.View(new EditViewModel
                {
                    Course = courseModel.Course
                });
            }
        }

        public ViewResult Create()
        {
            return this.View("Edit", new EditViewModel { Course = new CourseModel() });
        }

        [HttpPost]
        public ActionResult Delete(int courseId)
        {
           CourseModel deletedCourse = this.repository.CourseDelete(courseId);
           if (deletedCourse != null)
              {
                this.TempData["message"] = string.Format("Курс \"{0}\" успешно удален.", deletedCourse.Name);
              }

            return this.RedirectToAction("List");
        }
    }
}