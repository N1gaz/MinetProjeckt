using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.ComponentModel;

namespace ConsoleApp2
{
    [Serializable]
    class Edition : IComparable<Edition>, IComparer<Edition>, INotifyPropertyChanged
    {
        protected string name;
        protected DateTime income;
        protected int edition;

        protected void RaisePropertyCHanged(string field, string how)
        {
            var e = PropertyChanged;
            if (e != null)
            {
                var PropertyName = "Field " + field + " in edition by " + how;
                e(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;


        protected void ClearEvents()
        {
            this.PropertyChanged = null;
        }

        protected System.ComponentModel.PropertyChangedEventHandler Events()
        {
            return PropertyChanged;
        }

        public Edition()
        {
            name = "none";
            income = new DateTime(2019, 01, 01);
            edition = 0;
        }

        public Edition(string name, DateTime income, int edition)
        {
            this.name = name;
            this.income = income;
            this.edition = edition;
        }

        public string m_name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public DateTime m_income
        {
            get
            {
                return income;
            }
            set
            {
                income = value;
                RaisePropertyCHanged("Edition.income",value.ToString());
            }
        }

        public int m_edition
        {
            get
            {
                return edition;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Номер издания не может быть отрицательным\n\n\n");
                }
                else
                {
                    edition = value;
                    RaisePropertyCHanged("Edition.edition",value.ToString());
                }
            }
        }

        public virtual object DeepCopy()
        {
            Edition ret = new Edition(name, income, edition);

            return ret;
        }

        public override bool Equals(object obj)
        {
            Edition obj1;
            if (obj is Edition)
                obj1 = (Edition)obj;
            else
                return false;
            return (this.name == obj1.name && this.income == obj1.income && this.edition == obj1.edition);
        }

        public static bool operator ==(Edition obj1, Edition obj2)
        {
            return (obj1.name == obj2.name && obj1.income == obj2.income && obj1.edition == obj2.edition);
        }

        public static bool operator !=(Edition obj1, Edition obj2)
        {
            return (obj1.name != obj2.name || obj1.income != obj2.income || obj1.edition != obj2.edition);
        }

        public override int GetHashCode()
        {
            int HashCode = 0;
            for (int i = 0; i < name.Length; i++)
            {
                HashCode = HashCode + name[i];
            }

            HashCode = HashCode + income.Day + income.Month + income.Year + edition;
            return HashCode;
        }

        public override string ToString()
        {
            return (name + " " + income + " " + edition);
        }

        public int CompareTo([AllowNull] Edition other)
        {
            return this.name.CompareTo(other.m_name);
        }

        public int Compare([AllowNull] Edition x, [AllowNull] Edition y)
        {
            return x.m_income.CompareTo(y.m_income);
        }

        public class EditionComparer : IComparer<Edition>
        {
            public int Compare([AllowNull] Edition x, [AllowNull] Edition y)
            {
                return x.m_edition.CompareTo(y.m_edition);
            }
        }
    }
}
