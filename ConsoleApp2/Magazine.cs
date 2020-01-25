using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ConsoleApp2
{
    [Serializable]
    class Magazine : Edition, IRateAndCopy
    {
        private Frequency freq;
        private System.Collections.Generic.List<Person> Editors = new List<Person>();
        private System.Collections.Generic.List<Article> Articles = new List<Article>();

        public Magazine()
        {
            name = "none";
            freq = Frequency.weekly;
            income = new DateTime(1900, 01, 01);
            edition = 0;
        }

        public Magazine(string name, Frequency freq, DateTime income, int edition)
        {
            this.name = name;
            this.freq = freq;
            this.income = income;
            this.edition = edition;
        }

        public Magazine(Edition ed, Frequency freq)
        {
            this.name = ed.m_name;
            this.income = ed.m_income;
            this.m_edition = ed.m_edition;
            this.freq = freq;
        }

        public double rating
        {
            get
            {
                if (Articles.Count == 0)
                    return 0;
                else
                {
                    int count = 0;
                    double sum = 0;
                    foreach (Article i in Articles)
                    {
                        sum += i.rating;
                        count++;
                    }
                    double mid = sum / count;
                    return mid;
                }
            }
        }

        public System.Collections.Generic.List<Article> m_Articles
        {
            get
            {
                return Articles;
            }
            set
            {
                Articles = value;
            }
        }

        public System.Collections.Generic.List<Person> m_Editors
        {
            get
            {
                return Editors;
            }
            set
            {
                Editors = value;
            }
        }

        public Frequency m_freq
        {
            get
            {
                return freq;
            }
            set
            {
                freq = value;
            }
        }

        public bool this[Frequency index]
        {
            get
            {
                if (freq == index)
                    return true;
                else
                    return false;
            }
        }

        public void AddArticles(Article[] Mas)
        {
            Articles.AddRange(Mas);
        }

        public void AddEditors(Person[] Mas)
        {
            Editors.AddRange(Mas);
        }

        public override string ToString()
        {
            string ret;
            ret = name + "\t" + freq + "\t" + income + "\t" + edition + "\n" + "\nCписок статей в журнале\n";
            foreach (Article i in Articles)
            {
                ret += i.ToString();
            }
            ret += "\nСписок редакторов в журнале\n";
            foreach (Person i in Editors)
            {
                ret += i.ToString() + "\n";
            }
            return ret;
        }

        public string ToShortString()
        {
            return name + "\t" + freq + "\t" + income + "\t" + edition + "\t" + rating;
        }

        public override object DeepCopy()
        {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    ms.Position = 0;

                    this.ClearEvents();

                    formatter.Serialize(ms, this);
                    ms.Position = 0;

                return formatter.Deserialize(ms);
                }

            //Magazine ret = new Magazine(name, freq, income, edition);

            //Article[] rA = new Article[Articles.Count];
            //Person[] rE = new Person[Editors.Count];

            //int i = 0;
            //foreach (Article A in Articles)
            //{
            //    rA[i] = (Article)A.DeepCopy();
            //    i++;
            //}

            //i = 0;
            //foreach (Person E in Editors)
            //{
            //    rE[i] = (Person)E.DeepCopy();
            //    i++;
            //}

            //ret.AddArticles(rA);
            //ret.AddEditors(rE);

            //return ret;
        }

        public bool Save(string filename)
        {
            bool check = false;

            using (FileStream fs = File.Create(filename))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                this.ClearEvents();
                formatter.Serialize(fs, this);
                check = true;
                
            }

            return check;
        }

        public bool Load(string filename)
        {
            bool check = false;

            using (FileStream fs = File.OpenRead(filename))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                fs.Position = 0;
                Magazine buff  = (Magazine)formatter.Deserialize(fs);

                this.m_Edition = buff.m_Edition;
                this.m_freq = buff.m_freq;
                this.Editors = buff.Editors;
                this.Articles = buff.Articles;
                check = true;
            }

                return check;
        }

        public bool AddFromConsole()
        {
            bool check = false;
            Console.WriteLine("Добавление статьи.");
            Console.WriteLine("Введите Имя/Фамилию/Дату рождения автора(дд.мм.гггг через разделитель)/Название статьи/Рейтинг статьи(дробная часть через точку)(Разделители: пробел, запятая, символ \"/\")");
            string input = Console.ReadLine();

            char[] separator = { ' ', ',', '/' };

            try
            {
                string[] args = input.Split(separator);
                DateTime birthday = new DateTime(Convert.ToInt32(args[4]), Convert.ToInt32(args[3]), Convert.ToInt32(args[2]));
                double rating = Convert.ToDouble(args[6]);
                Article add = new Article(new Person(args[0],args[1], birthday), args[5], rating);
                Articles.Add(add);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return check;
        }

        public Edition m_Edition
        {
            get
            {
                Edition ret = new Edition(name, income, edition);
                return ret;
            }
            set
            {
                base.m_name = value.m_name;
                base.m_income = value.m_income;
                base.m_edition = value.m_edition;
            }
        }

        public System.Collections.IEnumerable GetMoreThan(double rate)
        {
            foreach (Article i in Articles)
            {
                if (i.rating > rate)
                {
                    yield return i;
                }
            }
        }

        public System.Collections.IEnumerable GetWithString(string name)
        {
            foreach (Article i in Articles)
            {
                if (i.name.Contains(name))
                {
                    yield return i;
                }
            }
        }

        public System.Collections.IEnumerable AuthorNotEditor()
        {
            bool check;
            foreach (Article i in Articles)
            {
                check = true;
                foreach (Person j in Editors)
                {
                    if (i.author == j)
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    yield return i;
                }
            }
        }

        public System.Collections.IEnumerable AuthorIsEditor()
        {
            foreach (Person i in Editors)
            {
                foreach (Article j in Articles)
                {
                    if (i == j.author)
                    {
                        yield return j;
                    }
                }
            }
        }

        public System.Collections.IEnumerable EditorNotAuthor()
        {
            bool check;
            foreach (Person i in Editors)
            {
                check = true;
                foreach (Article j in Articles)
                {
                    if (i == j.author)
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    yield return i;
                }
            }
        }

        public void SortByName()
        {
            Articles.Sort();
        }

        public void SortByAuthor()
        {
            Articles.Sort(0, Articles.Count, new Article());
        }

        public void SortByRating()
        {
            Articles.Sort(0, Articles.Count, new Article.RatingComparer());
        }
    }
}