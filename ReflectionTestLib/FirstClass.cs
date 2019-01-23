using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTestLib
{
    public class FirstClass
    {
        private string _Data;

        [Description("Свойство данных")]
        public string Data => _Data;

        public FirstClass(string Data) => _Data = Data;

        private void PrivatePrint() => Console.WriteLine("Private print method executed");
    }
}
