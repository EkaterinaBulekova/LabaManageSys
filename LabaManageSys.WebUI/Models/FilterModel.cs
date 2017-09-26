using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabaManageSys.WebUI.Models
{
    public class FilterModel
    {
        public string Author { get; set; }

        public int Level { get; set; }

        public string Topic { get; set; }

        public string Tags { get; set; }
    }
}