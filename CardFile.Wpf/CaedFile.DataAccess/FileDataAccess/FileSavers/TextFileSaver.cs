using CardFile.DataAccess.DataCollection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class TextFileSaver : IFileSaver
    {
        public void Open(string fileName, CardCollection collection)
        {
            throw new NotImplementedException();
        }

        public void Save(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.FirstName};{item.MiddleName};{item.LastName};" +
                            $"{item.BirthDate.ToShortDateString()};{item.PaymentAmount};{item.ChildrenCount}");
                    }
                }
            }
        }
    }
}
