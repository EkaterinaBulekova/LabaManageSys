using System.Collections.Generic;
using System.IO;
using LabaManageSys.XML.Models;

namespace LabaManageSys.XML.Abstract
{
    public interface IxmlProcessor
    {
        IEnumerable<TaskXMLModel> UploadTasksFromFile(Stream inputStream);

        void DownloadTasksToFile(IEnumerable<TaskXMLModel> models, string fullPath);
    }
}
