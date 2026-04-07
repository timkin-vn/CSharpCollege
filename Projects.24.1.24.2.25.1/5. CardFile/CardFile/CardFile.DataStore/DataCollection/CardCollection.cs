using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CardFile.DataStore.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId { get; set; } = 5;

        private readonly List<string> _heroes = new List<string>();

        private readonly List<string> _items = new List<string>();

        private readonly List<string> _neutrals = new List<string>();

        public CardCollection()
        {
            if (!TryLoadSeedCatalog())
            {
                _heroes.AddRange(new[] { "Anti-Mage", "Juggernaut", "Phantom Assassin", "Pudge", "Storm Spirit", "Axe" });
                _items.AddRange(new[] { "Phase Boots", "Power Treads", "Radiance", "Battle Fury", "Maelstrom", "Manta Style", "Butterfly", "Eye of Skadi", "Abyssal Blade", "Black King Bar", "Blink Dagger", "Aghanim's Scepter", "Aghanim's Shard" });
                _neutrals.AddRange(new[] { "Pupil's Gift", "Possessed Mask", "Grove Bow", "Vampire Fangs", "Mind Breaker" });
            }

            _cards.Add(new CardDto
            {
                Id = 1,
                Hero = "Anti-Mage",
                Slot1 = "Power Treads",
                Slot2 = "Radiance",
                Slot3 = "Manta Style",
                Slot4 = "Abyssal Blade",
                Slot5 = "Aghanim's Scepter",
                Slot6 = "Butterfly",
                Neutral = "Pupil's Gift",
            });
            _cards.Add(new CardDto
            {
                Id = 2,
                Hero = "Pudge",
                Slot1 = "Phase Boots",
                Slot2 = "Blink Dagger",
                Slot3 = "Aghanim's Shard",
                Slot4 = "Black King Bar",
                Slot5 = "Aghanim's Scepter",
                Slot6 = "Abyssal Blade",
                Neutral = "Vampire Fangs",
            });
            _cards.Add(new CardDto
            {
                Id = 3,
                Hero = "Phantom Assassin",
                Slot1 = "Power Treads",
                Slot2 = "Black King Bar",
                Slot3 = "Aghanim's Shard",
                Slot4 = "Butterfly",
                Slot5 = "Eye of Skadi",
                Slot6 = "Abyssal Blade",
                Neutral = "Mind Breaker",
            });
            _cards.Add(new CardDto
            {
                Id = 4,
                Hero = "Axe",
                Slot1 = "Phase Boots",
                Slot2 = "Blink Dagger",
                Slot3 = "Black King Bar",
                Slot4 = "Aghanim's Shard",
                Slot5 = "Aghanim's Scepter",
                Slot6 = "Abyssal Blade",
                Neutral = "Possessed Mask",
            });

            MapperInitialization.PreRegister();
        }

        public IEnumerable<CardDto> GetAll()
        {
            return _cards.Select(c => c.Clone()).ToList();
        }

        public IEnumerable<string> GetHeroes()
        {
            return _heroes.ToList();
        }

        public IEnumerable<string> GetItems()
        {
            return _items.ToList();
        }

        public IEnumerable<string> GetNeutrals()
        {
            return _neutrals.ToList();
        }

        public bool AddHero(string name)
        {
            return AddToCatalog(_heroes, name);
        }

        public bool AddItem(string name)
        {
            return AddToCatalog(_items, name);
        }

        public bool AddNeutral(string name)
        {
            return AddToCatalog(_neutrals, name);
        }

        public int Save(CardDto card)
        {
            if (card.Id == 0)
            {
                var newCard = card.Clone();
                var id = NextId++;
                newCard.Id = id;
                _cards.Add(newCard);
                return id;
            }

            var index = _cards.FindIndex(c => c.Id == card.Id);
            if (index < 0)
            {
                return -1;
            }

            _cards[index] = card.Clone();
            return card.Id;
        }

        public bool Delete(int cardId)
        {
            var index = _cards.FindIndex(c => c.Id == cardId);
            if (index < 0)
            {
                return false;
            }

            _cards.RemoveAt(index);
            return true;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = _cards.Count == 0 ? 1 : _cards.Max(c => c.Id) + 1;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(newCollection);
            NextId = nextId;
        }

        internal void ReplaceAll(IEnumerable<CardDto> newCollection, int nextId, IEnumerable<string> heroes, IEnumerable<string> items, IEnumerable<string> neutrals)
        {
            ReplaceAll(newCollection, nextId);

            _heroes.Clear();
            _items.Clear();
            _neutrals.Clear();

            _heroes.AddRange((heroes ?? Enumerable.Empty<string>()).Where(n => !string.IsNullOrWhiteSpace(n)).Distinct());
            _items.AddRange((items ?? Enumerable.Empty<string>()).Where(n => !string.IsNullOrWhiteSpace(n)).Distinct());
            _neutrals.AddRange((neutrals ?? Enumerable.Empty<string>()).Where(n => !string.IsNullOrWhiteSpace(n)).Distinct());
        }

        private static bool AddToCatalog(List<string> catalog, string name)
        {
            name = (name ?? "").Trim();
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            if (catalog.Any(x => x.Equals(name, System.StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            catalog.Add(name);
            return true;
        }

        private bool TryLoadSeedCatalog()
        {
            try
            {
                var assembly = typeof(CardCollection).Assembly;
                var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith("Seed.DotaCatalog.json"));
                if (string.IsNullOrEmpty(resourceName))
                {
                    return false;
                }

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        return false;
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        var json = reader.ReadToEnd();
                        var seed = JsonConvert.DeserializeObject<DotaCatalogSeed>(json);
                        if (seed == null)
                        {
                            return false;
                        }

                        _heroes.AddRange(Normalize(seed.Heroes));
                        _items.AddRange(Normalize(seed.Items));
                        _neutrals.AddRange(Normalize(seed.Neutrals));

                        return _heroes.Count > 0 && _items.Count > 0 && _neutrals.Count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private static IEnumerable<string> Normalize(IEnumerable<string> values)
        {
            return (values ?? Enumerable.Empty<string>()).Select(v => (v ?? "").Trim()).Where(v => !string.IsNullOrEmpty(v)).Distinct();
        }

        private class DotaCatalogSeed
        {
            public List<string> Heroes { get; set; }

            public List<string> Items { get; set; }

            public List<string> Neutrals { get; set; }
        }
    }
}
