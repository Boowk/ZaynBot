using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTestes
{
    public class ClassCopia
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ClassCopia DeepCopy()
        {
            // Clone the root ...
            return (ClassCopia)this.MemberwiseClone();
        }
    }
}
