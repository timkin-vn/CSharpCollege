using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimerList.DataStore.Dtos;
using LimerList.DataStore.Infrastructure;

namespace LimerList.DataStore.DataCollection
{
    public class LimerCollection
    {
        private readonly List<LimerDto> _limers = new List<LimerDto>();
        internal int NextId { get; set; } = 2;
        public LimerCollection()
        {
            _limers.Add(new LimerDto()
            {
                FirstName = "Никита",
                LastName = "Мохин",
                MiddleName = "Денисович",
                BirthDate = new DateTime(2007, 10, 26),
                Group = "9/2-РПО-24/1"
                
            });
            MapperInitialization.PreRegister();
        }
        public int Save(LimerDto limer)
        {
            if (limer.Id == 0)
            {
                var newCard = limer.Clone();
                var id = NextId++;
                NextId = id;
                _limers.Add(limer);
                return id;
            }
            var index = _limers.FindIndex(l => l.Id == limer.Id);
            if(index == -1)
            {
                return -1;
            }
            _limers[index] = limer.Clone();
            return limer.Id;
        }
        public bool Delete(int limerId)
        {
            var index = _limers.FindIndex(_ => _.Id == limerId);
            if (index == -1)
            {
                return false;
            }
            _limers.RemoveAt(index);
            return true;
        }
        public IEnumerable<LimerDto> GetAll()
        {
            return _limers.Select(x => x.Clone()).ToList();
        }
        internal void ReplaceAll(IEnumerable<LimerDto> limers)
        {
            _limers.Clear();
            _limers.AddRange(limers);
            NextId = _limers.Max(x => x.Id) + 1;
        }
        internal void ReplaceAll(IEnumerable<LimerDto> limers, int nextId)
        {
            _limers.Clear();
            _limers.AddRange(limers);
            NextId = nextId;
        }
    }
}
