using CardFile.DataStore.DataCollection;
using CardFile.DataStore.FileDataAccess.FileManagers;
using System;
using System.IO;

namespace CardFile.DataStore.FileDataAccess
{
    public class FileDataService
    {
        public void SaveToFile(string fileName, StudentCollection collection)
        {
            var manager = GetFileManager(fileName);
            manager.SaveToFile(fileName, collection);
        }

        public void OpenFromFile(string fileName, StudentCollection collection)
        {
            var manager = GetFileManager(fileName);
            manager.OpenFromFile(fileName, collection);
        }

        private IFileManager GetFileManager(string fileName)
        {
            switch (Path.GetExtension(fileName))
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

            throw new Exception("Неизвестное расширение имени файла");
        }
    }
}
