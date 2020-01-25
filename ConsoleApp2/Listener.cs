using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ConsoleApp2
{

    class Listener
    {
        private List<ListEntry> changes = new List<ListEntry>();

        public void MagazinesChanged(object sender,MagazinesChangedEventArgs<string> info)
        {
            ListEntry newListener = new ListEntry(info.name, info.what, info.where, info.key.ToString());
            changes.Add(newListener);
        }

        public void EditionsChanged(object sender, PropertyChangedEventArgs info)
        {
            if (sender is Edition)
            {
                Edition copy = (Edition)sender;
                ListEntry newListener = new ListEntry(copy.m_name, Update.Property, info.PropertyName, "something");
                changes.Add(newListener);
            }
            else 
            {
                throw new ArgumentException("Ошибка. Объект неверного типа");
            }
            
        }

        public override string ToString()
        {
            string res = "";
            foreach (ListEntry i in changes)
            {
                res += i.ToString() + "\n";
            }
            return res;
        }
    }
}
