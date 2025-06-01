using CardFile.DataAccess.DataCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal interface IFileSaver
    {
        void SaveFile(string fileName, CardCollection collection);
        void OpenFile(string fileName, CardCollection collection);
    }
}
