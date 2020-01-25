using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    [Serializable]
    class Person
    {
        private string name;
        private string surname;
        private System.DateTime birthday;
        public Person()
        {
            name = "none";
            surname = "none";
            birthday = new DateTime(1900, 01, 01);
        }

        public Person(string a, string b, DateTime c)
        {
            name = a;
            surname = b;
            birthday = c;
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

        public string m_surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }

        public System.DateTime m_birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }

        public int year
        {
            get
            {
                return birthday.Year;
            }
            set
            {
                birthday = new DateTime(value, birthday.Month, birthday.Day);
            }
        }

        public override string ToString()
        {
            return name + "\t" + surname + "\t" + birthday;
        }

        public string ToShortString()
        {
            return name + "\t" + surname;
        }

        public override bool Equals(object obj)
        {
            Person obj1;
            if (obj is Person)
                obj1 = (Person)obj;
            else
                return false;

            return (name == obj1.name && surname == obj1.surname && birthday == obj1.birthday);
        }

        public static bool operator ==(Person obj1, Person obj2)
        {
            return (obj1.name == obj2.name && obj1.surname == obj2.surname && obj1.birthday == obj2.birthday);
        }

        public static bool operator !=(Person obj1, Person obj2)
        {
            return (obj1.name != obj2.name || obj1.surname != obj2.surname || obj1.birthday != obj2.birthday);
        }

        public override int GetHashCode()
        {
            int HashCode = 0;
            for (int i = 0; i < name.Length; i++)
            {
                HashCode += name[i];
            }

            for (int i = 0; i < surname.Length; i++)
            {
                HashCode += surname[i];
            }

            HashCode = HashCode + birthday.Day + birthday.Month + birthday.Year;
            return HashCode;
        }

        public object DeepCopy()
        {
            Person ret = new Person(name, surname, birthday);
            return ret;
        }
    }
}