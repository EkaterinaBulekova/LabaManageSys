using System;
using System.Collections.Generic;
using System.Globalization;
using LabaManageSys.WebUI.Models;

namespace LabaManageSys.WebUI.ViewModels.TeachPlan
{
    public class EditViewModel
    {
        public CourseModel Course { get; set; }

        public string Dates { get; set; }

        public IEnumerable<DateTime> GetArrayDates()
        {
            var str_dates = this.Dates.Replace(".", "/").Trim().Split(',');
            foreach (var str in str_dates)
            {
                yield return DateTime.ParseExact(str, "g", new CultureInfo("fr-FR"));
            }
        }
    }
}