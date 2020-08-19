using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Enemy : Unit
{
    public Enemy() { }

    public Enemy(string name, int hp, int mp, int atk, int def, int agi, string imageFilePath)
        : base(name, hp, mp, atk, def, agi, imageFilePath) { }

    public Command ChooseCommand()
    {
        var skill = Skills.GetAtRandom();
        var target = GameInfos.Allies.GetAtRandom();
        return new Command() { User = this, Target = target, Skill = skill };
    }

}
