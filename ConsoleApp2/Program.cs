using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ConsoleApp2
{
    delegate TKey KeySelector<TKey>(Magazine mg);
    delegate System.Collections.Generic.KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    delegate void PropertyChangedEventHandler(Object sender, PropertyChangedEventArgs e);

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа номер 3!!!\n===========================================================================\n\n");

            Magazine M = new Magazine("Какая-то еженедельная хрень", Frequency.weekly, new DateTime(2019, 10, 23), 3);
            Magazine MNew1 = new Magazine("Какая-то ежемесечная хрень", Frequency.monthly, new DateTime(2018, 11, 5), 1);
            Magazine MNew2 = new Magazine("Какая-то ежегодная хрень", Frequency.yearly, new DateTime(2020, 1, 1), 1);

            Article[] A = new Article[5]
            {
                new Article(new Person("Василий","Пупкин",new DateTime(1998,02,14)),"Полегончеки",8.65),
                new Article(new Person("Иван", "Иванов", new DateTime(1987, 05, 29)), "Теория победы сферического коня в вакууме", 10.00),
                new Article(new Person("Петр", "Алексеев", new DateTime(1990, 06, 01)), "Я не умею читать", 6.11),
                new Article(new Person("Михаил", "Горшенев", new DateTime(1973, 08, 07)), "Как умереть от передозировки в 38", 5.81),
                new Article(new Person("Михаил", "Круг", new DateTime(1962, 04, 07)), "Я не опущенный", 9.24)
            };

            M.AddArticles(A);

            Person[] P = new Person[5]
            {
                new Person("Василий","Пупкин",new DateTime(1998,02,14)),
                new Person("Иван", "Иванов", new DateTime(1987, 05, 29)),
                new Person("Петр", "Алексеев", new DateTime(1990, 06, 01)),
                new Person("Просто","Петрович",new DateTime(1954,01,01)),
                new Person("Прошел","Войну",new DateTime(1923,07,02))
            };

            M.AddEditors(P);

            Magazine Mcopy = M.DeepCopy() as Magazine;

            P[1] = new Person("Михаил", "Зыбенкенко", new DateTime(1998, 02, 14));

            M.m_name = "Особенная еженедельная хрень";
            M.AddEditors(P);
            Console.WriteLine("Без сортировки статей:");
            Console.WriteLine(M.ToString());
            Console.WriteLine("Сортировка статей по названию:");
            M.SortByName();
            Console.WriteLine(M.ToString());
            Console.WriteLine("Сортировка статей по фамилии автора:");
            M.SortByAuthor();
            Console.WriteLine(M.ToString());
            Console.WriteLine("Сортировка статей по рейтингу:");
            M.SortByRating();
            Console.WriteLine(M.ToString());

            KeySelector<string> selector = new KeySelector<string>(Key);
            MagazineCollection<string> collection = new MagazineCollection<string>(selector);

            collection.AddDefaults();

            Console.WriteLine(collection.ToShortString());

            Magazine[] Mas = new Magazine[4];
            Mas[0] = M;
            Mas[1] = Mcopy;
            Mas[2] = MNew1;
            Mas[3] = MNew2;

            collection.AddMagazines(Mas);

            Console.WriteLine(collection.ToShortString() + "\n\n\n");

            Console.WriteLine("Максимальный средний рейтинг журналов:" + "\n" + collection.MaxRating + "\n");

            Console.WriteLine("\nЖурналы с еженедельным изданием:");
            foreach (KeyValuePair<string, Magazine> i in collection.FrequencyGroup(Frequency.weekly))
            {
                Console.WriteLine(i.Value.ToShortString());
            }

            Console.WriteLine("\nЖурналы с ежемесячным изданием:");
            foreach (KeyValuePair<string, Magazine> i in collection.FrequencyGroup(Frequency.monthly))
            {
                Console.WriteLine(i.Value.ToShortString());
            }

            Console.WriteLine("\nЖурналы с ежегодным изданием:");
            foreach (KeyValuePair<string, Magazine> i in collection.FrequencyGroup(Frequency.yearly))
            {
                Console.WriteLine(i.Value.ToShortString());
            }

            Console.WriteLine("\n\n\n Группировка по периодичности:");
            foreach (var i in collection.grouping)
            {
                Console.WriteLine();
                Console.WriteLine(i.Key);
                foreach (var item in i)
                {
                    Console.WriteLine(item.Value.ToShortString());
                }
            }

            Console.WriteLine("Проверка времени поиска\n");
            GenerateElement<Edition, Magazine> generator = new GenerateElement<Edition, Magazine>(Generate);

            try
            {
                TestCollections<Edition, Magazine> test = new TestCollections<Edition, Magazine>(100000, generator);


                Console.WriteLine("\n\n");
                test.TimeForEditions();
                Console.WriteLine("\n\n");
                test.TimeForNames();
                Console.WriteLine("\n\n");
                test.TimeForPairWithEditions();
                Console.WriteLine("\n\n");
                test.TimeForPairWithNames();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("====================================конец лабораторной номер 3============================================");
            Console.WriteLine("Лабораторная работа номер 4\n=============================================================================");
            MagazineCollection<string> newcollection1 = new MagazineCollection<string>("collection1", selector);
            MagazineCollection<string> newcollection2 = new MagazineCollection<string>("collection2", selector);

            Listener subscriber = new Listener();

            foreach (Magazine i in Mas)
            {
                i.PropertyChanged += subscriber.EditionsChanged;
            }

            //newcollection1.MagazineChanged += subscriber.MagazinesChanged;
            newcollection2.MagazineChanged += subscriber.MagazinesChanged;



            newcollection1.AddDefaults();
            newcollection2.AddDefaults();
            newcollection1.AddMagazines(Mas);
            newcollection2.Replace(new Magazine(), Mas[1]);
            newcollection2.ChangeMagazine(selector(Mas[1]), 10);
            newcollection2.ChangeMagazine(selector(Mas[1]), new DateTime(2020, 1, 1));



            Console.WriteLine(subscriber.ToString());

            Console.WriteLine("====================================конец лабораторной номер 4============================================");
            Console.WriteLine("Лабораторная работа номер 5\n=============================================================================");

            Console.WriteLine(Mas[0].ToString());
            Magazine copy1 = (Magazine)Mas[0].DeepCopy();
            Console.WriteLine(copy1.ToString());


            Console.WriteLine("Введите имя файла:");
            string filename = Console.ReadLine();

            Magazine copy2 = new Magazine();
            if (System.IO.File.Exists(filename))
            {
                copy2.Load(filename);
            }
            else
            {
                copy2.Save(filename);
            }

            copy2.AddFromConsole();

            Console.WriteLine("Введите имя файла:");
            filename = Console.ReadLine();
            Save(filename, copy2);




            Console.WriteLine(copy2.ToString());
        
        }

        static string Key(Magazine mg)
        {
            return mg.m_name;
        }


        static KeyValuePair<Edition, Magazine> Generate(int j)
        {
            Frequency freq = (Frequency)(0);
            Edition key = new Edition($"Name{j}", new DateTime(2000, 1,1), 1);
            Magazine value = new Magazine(key, freq);
            return new KeyValuePair<Edition, Magazine>(key, value);
        }
        static bool Save(string filename, Magazine obj)
        {
            bool check = false;

            if (obj.Save(filename))
            {
                check = true;
            }

            return check;
        }

        static bool Load(string filename, Magazine obj)
        {
            bool check = false;

            if (obj.Load(filename))
            {
                check = true;
            }

            return check;
        }
    }
}