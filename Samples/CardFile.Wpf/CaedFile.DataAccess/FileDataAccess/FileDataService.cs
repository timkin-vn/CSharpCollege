using CardFile.DataAccess.DataCollection;
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
            var fileSever = GetFileSaver(fileName);
            fileSever.Save(fileName, collection);
        }

        public void OpenFile(string fileName, CardCollection collection)
        {
            var fileSaver = GetFileSaver(fileName);
            fileSaver.Open(fileName, collection);
        }

        public IFileSevar GetFileSaver(string fileName)
        {
            switch (Path.GetExtension(fileName))
            {
                case ".txt":
                    return new TextFileSever();
                case ".cardbin":
                    return new BinaryDileSever();
                case ".xml":
                    return new XmlFileSever();
                case ".json":
                    return new JsonFileSever();
                case ".zip":
                    return new ZipFileSever();
            }
            return new TextFileSever();
        }
    }
}
