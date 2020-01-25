using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp2
{

    class TestCollections<TKey,TValue>
    {
        private System.Collections.Generic.List<TKey> editions = new List<TKey>();
        private System.Collections.Generic.List<string> names = new List<string>();
        private System.Collections.Generic.Dictionary<TKey, TValue> byEdition = new Dictionary<TKey, TValue>();
        private System.Collections.Generic.Dictionary<string, TValue> byName = new Dictionary<string, TValue>();
        private GenerateElement<TKey, TValue> generate;


        public TestCollections(int count, GenerateElement<TKey, TValue> generate)
        {
            if (count < 0)
            {
                throw new ArgumentException("Количество элементов не может быть отрицательным!");
            }
            this.generate = generate;

            for (int i = 0; i < count; i++)
            {
                KeyValuePair<TKey,TValue > pair = generate(i);
                editions.Add(pair.Key);
                names.Add(pair.Key.ToString());
                byEdition.Add(pair.Key, pair.Value);
                byName.Add(pair.Key.ToString(), pair.Value);
            }
        }

        public void TimeForEditions()
        {
            TKey first = generate(0).Key;
            TKey mid = generate(editions.Count/2).Key;
            TKey last = generate(editions.Count - 1).Key;
            TKey none = generate(editions.Count + 100).Key;

            Stopwatch time = new Stopwatch();

            time = Stopwatch.StartNew();
            editions.Contains(first);
            time.Stop();
            Console.WriteLine("В списке изданий:\nВремени для первого элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            editions.Contains(mid);
            time.Stop();
            Console.WriteLine("Времени для центрального элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            editions.Contains(last);
            time.Stop();
            Console.WriteLine("Времени для последнего элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            editions.Contains(none);
            time.Stop();
            Console.WriteLine("Времени для невключенного элемента прошло" + time.Elapsed + "\n");
        }

        public void TimeForNames()
        {
            string first = generate(0).Key.ToString();
            string mid = generate(names.Count/2).Key.ToString();
            string last = generate(names.Count - 1).Key.ToString();
            string none = generate(names.Count + 100).Key.ToString();

            Stopwatch time = new Stopwatch();

            time = Stopwatch.StartNew();
            names.Contains(first);
            time.Stop();
            Console.WriteLine("В списке строк с информацией об издании:\nВремени для первого элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            names.Contains(mid);
            time.Stop();
            Console.WriteLine("Времени для центрального элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            names.Contains(last);
            time.Stop();
            Console.WriteLine("Времени для последнего элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            names.Contains(none);
            time.Stop();
            Console.WriteLine("Времени для невключенного элемента прошло" + time.Elapsed + "\n");
        }

        public void TimeForPairWithEditions()
        {
            TKey first = generate(0).Key;
            TKey mid = generate(editions.Count / 2).Key;
            TKey last = generate(editions.Count - 1).Key;
            TKey none = generate(editions.Count + 100).Key;

            Stopwatch time = new Stopwatch();

            time = Stopwatch.StartNew();
            byEdition.ContainsKey(first);
            time.Stop();
            Console.WriteLine("В словаре с ключом - изданием:\nВремени для первого элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            byEdition.ContainsKey(mid);
            time.Stop();
            Console.WriteLine("Времени для центрального элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            byEdition.ContainsKey(last);
            time.Stop();
            Console.WriteLine("Времени для последнего элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            byEdition.ContainsKey(none);
            time.Stop();
            Console.WriteLine("Времени для невключенного элемента прошло" + time.Elapsed + "\n");
        }

        public void TimeForPairWithNames()
        {
            string first = generate(0).Key.ToString();
            string mid = generate(names.Count / 2).Key.ToString();
            string last = generate(names.Count - 1).Key.ToString();
            string none = generate(names.Count + 100).Key.ToString();

            Stopwatch time = new Stopwatch();

            time = Stopwatch.StartNew();
            byName.ContainsKey(first);
            time.Stop();
            Console.WriteLine("В словаре с ключом - строкой:\nВремени для первого элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            byName.ContainsKey(mid);
            time.Stop();
            Console.WriteLine("Времени для центрального элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            byName.ContainsKey(last);
            time.Stop();
            Console.WriteLine("Времени для последнего элемента прошло" + time.Elapsed + "\n");

            time = Stopwatch.StartNew();
            byName.ContainsKey(none);
            time.Stop();
            Console.WriteLine("Времени для невключенного элемента прошло" + time.Elapsed + "\n");
        }
    }
}
