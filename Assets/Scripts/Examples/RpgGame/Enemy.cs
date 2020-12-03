using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy : Unit
{

    public Sprite Image;
    
    public Enemy(string name, string id, int hp, int mp, int atk, int def, int agi, string imageFilePath)
        : base(name, id, hp, mp, atk, def, agi, imageFilePath)
    {
        Image = Resources.Load<Sprite>(ImageFilePath);
    }

    public Command ChooseCommand()
    {
        var skill = Skills.GetAtRandom();
        var target = GameInfos.Allies.GetAtRandom();
        return new Command() { User = this, Target = target, Skill = skill };
    }

}
