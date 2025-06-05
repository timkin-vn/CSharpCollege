using CardFile.DataAccess.DataCollection;

namespace CardFile.DataAccess.FileDataAccess
{
    internal interface IFileSaver
    {
        void SaveToFile(string fileName, CardCollection collection);
        void OpenFromFile(string fileName, CardCollection collection);
    }
}