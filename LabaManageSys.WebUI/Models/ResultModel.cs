using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabaManageSys.WebUI.Models
{
    public class ResultModel
    {
        public bool IsSucceed { get; set; }

        public int SucceededItems { get; set; }

        public List<string> NonCommited { get; set; }
    }
}