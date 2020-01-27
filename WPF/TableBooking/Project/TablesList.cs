using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class TablesList
    {
        private List<Table> tables = null;

        public List<Table> Tables { get => tables; set => tables = value; }
        public TablesList()
        {
            tables = new List<Table>();
        }

        public void Add(Table table)
        {
            tables.Add(table);
        }

        public int Count
        {
            get { return tables.Count; }
        }

        public Table this[int i]
        {
            get { return tables[i]; }
            set { tables[i] = value; }
        }
        public void Clear()
        {
            tables.Clear();
        }
    }
}
