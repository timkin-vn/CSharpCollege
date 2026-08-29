using CardFile.DataStore.DataCollection;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public interface IFileManager
    {
        void SaveToFile(string fileName, StudentCollection collection);
        void OpenFromFile(string fileName, StudentCollection collection);
    }
}