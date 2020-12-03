public class Ally : Unit
{
    public string Job = "";
    public int Lv = 1;
    
    public Ally(string name, int hp, int mp, int atk, int def, int agi)
        :base(name, name, hp, mp, atk, def, agi){}
}
