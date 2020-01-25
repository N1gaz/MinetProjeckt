using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class MagazinesChangedEventArgs<TKey>
    {
        public string name { set; get; }
        public Update what { set; get; }
        public string where { set; get; }
        public TKey key { set; get; }

        public MagazinesChangedEventArgs(string name, Update what, string where, TKey key)
        {
            this.name = name;
            this.what = what;
            this.where = where;
            this.key = key;
        }

        public override string ToString()
        {
            return "Collection " + name + " were changed.\t" + where + " is " + what + " by " + key + ".";
        }
    }
}
