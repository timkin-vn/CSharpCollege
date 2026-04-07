using CardFile.DataStore.DataCollection;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public interface IFileManager
    {
        void SaveToFile(string fileName, LetterCollection collection);
        void OpenFromFile(string fileName, LetterCollection collection);
    }
}