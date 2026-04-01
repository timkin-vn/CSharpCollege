using CardFile.DataStore.DataCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public interface IFileManager
    {
        void SaveToFile(string fileName, BookCollection collection);

        void OpenFromFile(string fileName, BookCollection collection);
    }
}
