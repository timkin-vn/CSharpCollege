using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimerList.DataStore.DataCollection;

namespace LimerList.DataStore.FileAccess.FileManagers
{
    public interface IFileManager
    {
        void SaveToFile(string filePath, LimerCollection collection);
        void OpenFromFile(string filePath, LimerCollection collection);
    }
}
