using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Ally : Unit
{
    public Ally(string name, int hp, int mp, int atk, int def, int agi)
        :base(name, hp, mp, atk, def, agi){}
}
