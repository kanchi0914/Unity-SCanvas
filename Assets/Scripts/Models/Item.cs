using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class Item
    {
        private static int incrementer = 0;
        public string Id {
            get
            { return id; }
            private set
            {
                id = value;
            }
        } 
        private string id = (incrementer++).ToString();
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
