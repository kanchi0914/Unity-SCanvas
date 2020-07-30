using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Unit
{
    public string Name { get; set; }
    public int MaxHp { get; set; }
    public int CurrentHp
    {
        get
        {
            return currentHp;
        }
        set
        {
            if (value > MaxHp) currentHp = MaxHp;
            else if (value < 1) currentHp = 0;
            else currentHp = value;
        }
    }
    private int currentHp;
    public int MaxMp { get; set; }
    public int CurrentMp
    {
        get
        {
            return currentMp;
        }
        set
        {
            if (value > MaxMp) currentMp = MaxMp;
            else if (value < 1) currentMp = 0;
            else currentMp = value;
        }
    }
    private int currentMp;
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Agi { get; set; }
    public string ImageFilePath { get; set; }
    public bool IsAlive { get { return currentHp > 0; } }

    public Command CurrentCommand { get; set; }

    public List<Skill> Skills = new List<Skill>();

    public Unit()
    {

    }

    public Unit(string name, int hp, int mp, int atk, int def, int agi, string imageFilePath = null)
    {
        Name = name;
        CurrentHp = MaxHp = hp;
        CurrentMp = MaxMp = mp;
        Atk = atk;
        Def = def;
        Agi = agi;
        ImageFilePath = imageFilePath;
    }

    public override string ToString()
    {
        return Name;
    }

}
