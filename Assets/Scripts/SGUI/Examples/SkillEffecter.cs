using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class SkillEffecter
{
    public static string Effect(Skill skill, Unit user, Unit target)
    {
        if (skill.Name == Skill.SkillName.アイテム使用)
        {
            var item = GameInfos.Items.Find(i => i.Id == user.CurrentCommand.itemId);
            return UseItem(item, user, target);
        }
        var effectText = $"{user.Name}の{skill.Name}！ ";
        switch (skill.Name)
        {
            case Skill.SkillName.攻撃:
                {
                    var damage = CalculateAttackDamage(user, target);
                    target.CurrentHp -= damage;
                    effectText += $"{target.Name}に{damage}ダメージ";
                    break;
                }
            case Skill.SkillName.ヒール:
                {
                    target.CurrentHp += 50;
                    effectText += $"{target.Name}のhpが50回復した";
                    break;
                }
            case Skill.SkillName.ファイア:
                {
                    target.CurrentHp -= 20;
                    effectText += $"{target.Name}に20のダメージ";
                    break;
                }
            case Skill.SkillName.防御:
                {
                    effectText += $"{user.Name}は防御している";
                    break;
                }
        }
        if (target?.CurrentHp <= 0) effectText += $"\n{target.Name}は倒れた";
        return effectText;
    }

    private static int CalculateAttackDamage(Unit user, Unit target)
    {
        int damage = user.Atk - target.Def / 2;
        if (damage < 1) return 1;
        return damage;
    }

    public static string UseItem(Item item, Unit user, Unit target)
    {
        GameInfos.Items.RemoveAll(i => i.Id == item.Id);
        var text = $"{user}は{item}を使った！ ";
        switch (item.Name)
        {
            case Item.ItemName.薬草:
                {
                    target.CurrentHp += 30;
                    text += $"{target}のHPが30回復した";
                    break;
                }
        }
        return text;
    }

    public static string Effect(Skill item, Unit user, List<Unit> taker)
    {
        switch (item.Name)
        {
            case Skill.SkillName.なぎ払い:
                {
                    taker.ForEach(t => t.CurrentHp -= 20);
                    return $"全体に20ダメージ";
                }
        }
        return "";
    }
}
