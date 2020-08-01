using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class GameInfos
{
    public static List<Item> Items { get; private set; } = new List<Item>()
    {
        new Item(){ Name = Item.ItemName.薬草 },
        new Item(){ Name = Item.ItemName.薬草 },
        new Item(){ Name = Item.ItemName.薬草 },
        new Item(){ Name = Item.ItemName.薬草 }
    };

    public static List<Ally> Allies { get; private set; } = new List<Ally>()
    {
        new Ally("田中", 50, 0, 12, 9, 5){ },
        new Ally("井上", 35, 20, 7, 7, 7){ Skills = new List<Skill>(){ SkillFactory.GenerateSkill(Skill.SkillName.ヒール) } },
        new Ally("佐藤", 40, 0, 10, 7, 10){ },
        new Ally("鈴木", 30, 25, 6, 6, 8){ Skills = new List<Skill>(){ SkillFactory.GenerateSkill(Skill.SkillName.ファイア) } },
    };

    public static List<Enemy> Enemies { get; private set; } = new List<Enemy>();

    static GameInfos()
    {
        CreateEnemies();
    }

    private static void CreateEnemies()
    {
        var enemy1 = new Enemy("ゴリラ", 20, 0, 10, 10, 5, "Images/animal_gorilla")
        {
            Skills = new List<Skill>() { new Skill(Skill.SkillName.攻撃) }
        };
        var enemy2 = new Enemy("アリクイ", 15, 0, 10, 12, 7, "Images/animal_ooarikui_arikui")
        {
            Skills = new List<Skill>() { new Skill(Skill.SkillName.攻撃) }
        };
        var enemy3 = new Enemy("ウシ", 25, 0, 8, 10, 8, "Images/animal_ushi_aurochs")
        {
            Skills = new List<Skill>() { new Skill(Skill.SkillName.攻撃) }
        };
        Enemies.Add(enemy1);
        Enemies.Add(enemy2);
        Enemies.Add(enemy3);
    }

}