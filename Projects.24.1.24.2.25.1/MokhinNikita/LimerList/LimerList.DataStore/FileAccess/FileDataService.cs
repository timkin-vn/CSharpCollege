using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimerList.DataStore.DataCollection;
using LimerList.DataStore.FileAccess.FileManagers;

namespace LimerList.DataStore.FileAccess
{
    public class FileDataService
    {
        public void SaveToFile(string filePath, LimerCollection collection)
        {
            var manager = GetFileManager(filePath);
            manager.SaveToFile(filePath, collection);
        }
        public void OpenFromFile(string filePath, LimerCollection collection)
        {
            var manager = GetFileManager(filePath);
            manager.OpenFromFile(filePath, collection);
        }
        private IFileManager GetFileManager(string fileName)
        {
            switch (Path.GetExtension(fileName))
            {
                case ".liml":
                    return new XmlFileManager();
                case ".txt":
                case ".csv":
                    return new TextFileManager();
                case ".lzip":
                    return new ZipFileManager();
                case ".ljson":
                    return new JsonFileManager();
                default:
                    throw new FieldAccessException("Неизвестное расширение файла");
            }
        }
    }
}
