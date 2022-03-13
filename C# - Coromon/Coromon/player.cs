using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coromon
{
    public class Player : Character
    {
        public Player()
        {
            this.changeUs();
            this.setItems("mask", 0);
            this.setItems("handSan", 0);
            this.setItems("health", 0);
        }
    }

}
