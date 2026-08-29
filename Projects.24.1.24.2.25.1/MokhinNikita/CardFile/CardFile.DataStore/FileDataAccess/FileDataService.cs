using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.FileDataAccess.FileManagers;

namespace CardFile.DataStore.FileDataAccess
{
    public class FileDataService
    {
        public void SaveToFile(string filename, CardCollection collection)
        {
            var manager = GetFileManager(filename);
            manager.SaveToFile(filename, collection);
        }
        public void OpenFromFile(string filename, CardCollection collection)
        {
            var manager = GetFileManager(filename);
            manager.OpenFromFile(filename, collection);
        }
        private IFileManager GetFileManager(string filename)
        {
            switch (Path.GetExtension(filename))
            {
                case ".txt":
                case ".csv":
                    return new TextFileManager();
                case ".cardbin":
                    return new BinaryFileManager();
                case ".cardxml":
                    return new XmlFileManager();
                case ".cardjson":
                    return new JsonFileManager();
                case ".cardzip":
                    return new ZipFileManager();
            }

            throw new FieldAccessException("Неизвестное расширение файла");
        }
    }
}
