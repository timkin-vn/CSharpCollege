using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.FileDataAccess.FileSavers;
using System;

namespace CardFile.DataAccess.FileDataAccess
{
    public class FileDataService
    {
        private readonly IFileSaver _binaryFileSaver = new BinaryFileSaver();
        private readonly IFileSaver _textFileSaver = new TextFileSaver();
        private readonly IFileSaver _xmlFileSaver = new XmlFileSaver();
        private readonly IFileSaver _jsonFileSaver = new JsonFileSaver();
        private readonly IFileSaver _zipFileSaver = new ZipFileSaver();

        public void SaveToFile(string fileName, CardCollection collection)
        {
            string extension = System.IO.Path.GetExtension(fileName).ToLower();
            IFileSaver saver = null;
            if (extension == ".txt")
                saver = _textFileSaver;
            else if (extension == ".cardbin")
                saver = _binaryFileSaver;
            else if (extension == ".cardxml")
                saver = _xmlFileSaver;
            else if (extension == ".cardjson")
                saver = _jsonFileSaver;
            else if (extension == ".cardzip")
                saver = _zipFileSaver;
            else
                throw new ArgumentException("Неизвестный формат файла");

            saver.SaveToFile(fileName, collection);
        }

        public void OpenFromFile(string fileName, CardCollection collection)
        {
            string extension = System.IO.Path.GetExtension(fileName).ToLower();
            IFileSaver saver = null;
            if (extension == ".txt")
                saver = _textFileSaver;
            else if (extension == ".cardbin")
                saver = _binaryFileSaver;
            else if (extension == ".cardxml")
                saver = _xmlFileSaver;
            else if (extension == ".cardjson")
                saver = _jsonFileSaver;
            else if (extension == ".cardzip")
                saver = _zipFileSaver;
            else
                throw new ArgumentException("Неизвестный формат файла");

            saver.OpenFromFile(fileName, collection);
        }
    }
}