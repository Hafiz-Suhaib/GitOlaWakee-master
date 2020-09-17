using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Dto
{

    public class Rootobject
    {
        public Parent[] Property1 { get; set; }
    }

    public class Parent
    {
        public string title { get; set; }
        public bool expanded { get; set; }
        public bool folder { get; set; }
        public Child[] children { get; set; }
        public bool lazy { get; set; }
    }

    public class Child
    {
        public string title { get; set; }
        public string type { get; set; }
        public string author { get; set; }
        public int year { get; set; }
        public int qty { get; set; }
        public float price { get; set; }
        public bool folder { get; set; }
        public Child[] children { get; set; }
    }

   

}
