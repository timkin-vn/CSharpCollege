using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.Entities
{
    internal class LinqRunner
    {
        public void Run()
        {
            // LINQ = Language integrated query
            var collection = new List<int> { 5, 12, 65, 35, 84, 95, 27, 44 };

            //foreach (var item in collection)
            //{
            //    if (IsEven(item))
            //    {
            //        Console.WriteLine(item);
            //    }
            //}

            //foreach (var item in collection.Where(x => x % 2 == 0))
            //{
            //    Console.WriteLine(item);
            //}

            //foreach (var item in collection.Select(x => x + 5))
            //{
            //    Console.WriteLine(item);
            //}

            //foreach (var item in collection.Where(x => x % 2 == 0).Select(x => x + 5))
            //{
            //    Console.WriteLine(item);
            //}

            //foreach (var item in collection.Select(x => x + 5).Where(x => x % 2 == 0))
            //{
            //    Console.WriteLine(item);
            //}

            var query = collection.Select(x => x + 5).Where(x => x % 2 == 0);
            var results = query.ToList();

            collection.Add(43);
            collection.Add(58);
            collection.Add(11);

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

            foreach (var item in results)
            {
                Console.WriteLine(item);
            }

            var query2 = from i in collection
                         where i % 2 == 0
                         select i + 5;

            foreach (var item in query2)
            {
                Console.WriteLine(item);
            }
        }

        bool IsEven(int x) => x % 2 == 0;
    }
}
