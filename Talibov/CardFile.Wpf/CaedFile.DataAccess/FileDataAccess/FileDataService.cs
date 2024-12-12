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
            var fileSaver = GetFileSaver(fileName);
            fileSaver.Save(fileName, collection);
        }

        public void OpenFile(string fileName, CardCollection collection)
        {
            var fileSaver = GetFileSaver(fileName);
            fileSaver.Open(fileName, collection);
        }

        private IFileSaver GetFileSaver(string fileName)
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
            }

            return new TextFileSaver();
        }
    }
}
