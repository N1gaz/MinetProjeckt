using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class ListEntry
    {
        public string name { get; set; }
        public Update what { get; set; }
        public string where { get; set; }
        public string key { get; set; }

        public ListEntry(string name, Update what, string where, string key)
        {
            this.name = name;
            this.what = what;
            this.where = where;
            this.key = key;
        }

        public override string ToString()
        {
            return "Collection or magazine " + name + " were changed. " + what + ". In " + where + ".";
        }
    }
}
