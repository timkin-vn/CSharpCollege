using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardFile.DataStore.DataCollection;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public interface IFileManager
    {
        void SaveToFile(string fileName, CardCollection collection);
        void OpenFromFile(string fileName, CardCollection collection);
    }
}
