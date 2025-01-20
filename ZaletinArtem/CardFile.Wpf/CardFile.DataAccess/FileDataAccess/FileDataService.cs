using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.FileDataAccess.FileSavers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess
{
    public class FileDataService
    {
        public void SaveToFile(string fileName, CardFileDataCollection collection)
        {
            var saver = GetSaver(fileName);
            saver.SaveFile(fileName, collection);
        }

        public void OpenFromFile(string fileName, CardFileDataCollection collection)
        {
            var saver = GetSaver(fileName);
            saver.OpenFile(fileName, collection);
        }

        private IFileSaver GetSaver(string fileName)
        {
            switch (Path.GetExtension(fileName))
            {
                case ".txt":
                    return new TextFileSaver();
                case ".cardbin":
                    return new BinaryFileSaver();
                case ".xml":
                    return new XmlFileSaver();
                case ".json":
                    return new JsonFileSaver();
                case ".zip":
                    return new ZipFileSaver();
                default:
                    throw new Exception("Неверное расширение файла");
            }
        }
    }
}
