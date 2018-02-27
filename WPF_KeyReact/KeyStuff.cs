using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_KeyReact
{
    public class KeyStuff
    {
        public bool isDown;
        public Key key;

        public KeyStuff(Key key)
        {
            this.isDown = false;
            this.key = key;
        }
    }
}
