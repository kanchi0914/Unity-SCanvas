using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameManager
{
    public static List<Item> Items { get; set; } = new List<Item>()
    {
        new Item(){ Name = Item.ItemName.薬草 },
        new Item(){ Name = Item.ItemName.薬草 },
        new Item(){ Name = Item.ItemName.薬草 },
        new Item(){ Name = Item.ItemName.薬草 }
    };

    public static List<Ally> Allies { get; set; } = new List<Ally>()
    {
        new Ally("田中", 1, 0, 12, 9, 5){ },
        new Ally("井上", 999, 20, 7, 7, 7){ Skills = new List<Skill>(){ SkillFactory.GenerateSkill(Skill.SkillName.ヒール) } },
        new Ally("佐藤", 1, 0, 10, 7, 10){ },
        new Ally("鈴木", 999, 25, 6, 6, 8){ Skills = new List<Skill>(){ SkillFactory.GenerateSkill(Skill.SkillName.ファイア) } },
    };

    public GameManager()
    {

    }
}