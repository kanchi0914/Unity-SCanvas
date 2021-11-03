using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examples.RpgGame;

public static class GameInfos
{
    public static List<Item> Items { get; private set; } = new List<Item>()
    {
        ItemFactory.GenerateItem(ItemName.薬草),
        ItemFactory.GenerateItem(ItemName.薬草),
        ItemFactory.GenerateItem(ItemName.薬草),
        ItemFactory.GenerateItem(ItemName.薬草),
    };

    public static List<Item> TempRemovedItems = new List<Item>();

    public static List<Ally> Allies { get; private set; } = new List<Ally>()
    {
        new Ally("タナカ", 45, 0, 12, 9, 5) {Job = "戦士"},
        new Ally("イノウエ", 35, 20, 7, 7, 7)
            {Job = "僧侶", Skills = new List<Skill>() {SkillFactory.GenerateSkill(SkillName.ヒール)}},
        new Ally("サトウ", 40, 0, 10, 7, 10) {Job = "武闘家"},
        new Ally("スズキ", 30, 25, 6, 6, 8)
        {
            Job = "魔法使い",
            Skills = new List<Skill>()
                {SkillFactory.GenerateSkill(SkillName.ファイア), SkillFactory.GenerateSkill(SkillName.フリーズ)}
        },
    };

    public static List<Enemy> Enemies { get; private set; } = new List<Enemy>()
    {
        new Enemy("ゴリラ", "gorilla_1", 20, 0, 10, 10, 5, "Images/pixel_gorilla")
        {
            Skills = new List<Skill>() {new Skill(SkillName.攻撃)}
        },
        new Enemy("トラ", "tiger_1", 15, 0, 10, 12, 7, "Images/pixel_tiger2")
        {
            Skills = new List<Skill>() {new Skill(SkillName.攻撃)}
        },
        new Enemy("ウシ", "cow_1", 25, 0, 8, 10, 8, "Images/pixel_cow2")
        {
            Skills = new List<Skill>() {new Skill(SkillName.攻撃)}
        }
    };
}