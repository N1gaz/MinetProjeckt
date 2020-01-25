using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp2
{
    delegate void MagazineChangedHandler<Tkey>(object source, MagazinesChangedEventArgs<Tkey> args);

    class MagazineCollection<TKey> : EventArgs
    {
        public string name { set; get; }
        private System.Collections.Generic.Dictionary<TKey, Magazine> collection = new Dictionary<TKey, Magazine>();
        private KeySelector<TKey> selector;

        private void RaisePropertyChanged(Update what, string where, TKey key)
        {
            var e = MagazineChanged;
            if (e != null)
            {
                MagazinesChangedEventArgs<TKey> args = new MagazinesChangedEventArgs<TKey>(this.name, what, where, key);
                e(this, args);
            }
        }

        public event MagazineChangedHandler<TKey> MagazineChanged;

        public MagazineCollection(string name, KeySelector<TKey> selector)
        {
            this.name = name;
            this.selector = selector;
        }
        public MagazineCollection(KeySelector<TKey> selector)
        {
            name = "none";
            this.selector = selector;
        }

        public void AddDefaults()
        {
            Magazine i = new Magazine();
            collection.Add(selector(i), i);
            RaisePropertyChanged( Update.Add, "list of magazines", selector(i));
        }

        public void AddMagazines(Magazine[] mg)
        {
            foreach (Magazine i in mg)
            {
                collection.Add(selector(i), i);
                RaisePropertyChanged(Update.Add, "list of magazines", selector(i));
            }
        }

        public bool Replace(Magazine mold, Magazine mnew)
        {
            bool change = false;
            if (collection.ContainsKey(selector(mold)))
            {
                collection.Add(selector(mnew), mnew);
                collection.Remove(selector(mold));
                RaisePropertyChanged(Update.Replace, selector(mold).ToString() + " changed by " + selector(mnew), selector(mnew));
                change = true;
            }
            return change;
        }

        public void ChangeMagazine(TKey key,int edition)
        {
            collection[key].m_edition = edition;
        }

        public void ChangeMagazine(TKey key, DateTime income)
        {
            collection[key].m_income = income;
        }

        public override string ToString()
        {
            string res = "";

            foreach (KeyValuePair<TKey, Magazine> keyValue in collection)
            {
                res += keyValue.Key;
                res += " - ";
                res += keyValue.Value.ToString();
                res += "\n";
            }

            return res;
        }

        public string ToShortString()
        {
            string res = "";

            foreach (KeyValuePair<TKey, Magazine> keyValue in collection)
            {
                res += keyValue.Key;
                res += " - ";
                res += keyValue.Value.ToShortString();
                res += "\n";
            }

            return res;
        }

        public double MaxRating
        {
            get
            {
                double res = 0;

                if (collection.Count > 0)
                {
                    List<double> mas = new List<double>();

                    foreach (KeyValuePair<TKey, Magazine> keyValue in collection)
                    {
                        mas.Add(keyValue.Value.rating);
                    }

                    res = System.Linq.Enumerable.Max(mas);
                }

                return res;
            }
        }

        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency value)
        {
            foreach (KeyValuePair<TKey, Magazine> keyValue in collection)
            {
                if (keyValue.Value.m_freq == value)
                {
                    yield return keyValue;
                }
            }
        }

        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> grouping
        {
            get
            {
                return collection.GroupBy(x => x.Value.m_freq);
            }
        }

    }
}
