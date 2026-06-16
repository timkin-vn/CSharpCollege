using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.FileDataAccess.FileSavers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess
{
    public class FileDataService
    {
        public void SaveToFile(string fileName, CardCollection collection)
        {
            var saver = GetFileSaver(fileName);
            saver.SaveFile(fileName, collection);
        }

        public void OpenFromFile(string fileName, CardCollection collection)
        {
            var saver = GetFileSaver(fileName);
            saver.OpenFile(fileName, collection);
        }

        private IFileSaver GetFileSaver(string fileName)
        {
            switch (Path.GetExtension(fileName))
            {
                case ".txt":
                    return new TextFileSaver();

                case ".cardbin":
                    return new BinaryFileSaver();

                case ".cardxml":
                    return new XmlFileSaver();

                case ".cardjson":
                    return new JsonFileSaver();

                case ".cardzip":
                    return new ZipFileSaver();

                default:
                    throw new Exception("Неверное расширение имени файла");
            }
        }
    }
}