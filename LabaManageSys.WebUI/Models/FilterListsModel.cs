using System.Collections.Generic;

namespace LabaManageSys.WebUI.Models
{
    public class FilterListsModel
    {
        public IEnumerable<string> Authors { get; set; }

        public IEnumerable<string> Topics { get; set; }

        public IEnumerable<string> Levels { get; set; }
    }
}