using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ConsoleApp2
{

    [Serializable]
    class Article : IRateAndCopy, IComparable<Article>, System.Collections.Generic.IComparer<Article>
    {
        public Person author { set; get; }
        public string name { set; get; }
        public double rating { set; get; }

        private bool IsCorrect()
        {
            bool check = true;
            if (rating > 10 || rating < 0)
            {
                check = false;
            }
            return check;
        }
        public Article()
        {
            author = new Person();
            name = "none";
            rating = 0;
        }

        public Article(Person a, string b, double c)
        {
            Article buff = new Article();
            author = a;
            name = b;
            rating = c;
            if (!IsCorrect())
            {
                this.author = buff.author;
                this.name = buff.name;
                this.rating = buff.rating;
                throw new ArgumentException("Выход за границы значения рейтинга");
            }
            
        }

        public override string ToString()
        {
            return author.ToString() + "\t" + name + "\t" + rating + "\n";
        }

        public object DeepCopy()
        {
            Article ret = new Article(author, name, rating);
            return ret;
        }

        public int CompareTo([AllowNull] Article other)
        {
            return this.name.CompareTo(other.name);
        }

        public int Compare([AllowNull] Article x, [AllowNull] Article y)
        {
            return x.author.m_surname.CompareTo(y.author.m_surname);
        }

        public class RatingComparer : IComparer<Article>
        {
            public int Compare([AllowNull] Article x, [AllowNull] Article y)
            {
                return -x.rating.CompareTo(y.rating);
            }
        }
    }
}
