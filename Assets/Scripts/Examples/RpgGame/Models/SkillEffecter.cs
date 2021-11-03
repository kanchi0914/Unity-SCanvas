using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class SkillEffecter
{
    public static string Invoke(Skill skill, Unit user, Unit target)
    {
        var message = GetInvokeText(skill, user);
        message += Effect(skill, user, target);
        return message;
    }

    public static string Invoke<T>(Skill skill, Unit user, List<T> targets) where T : Unit
    {
        var message = GetInvokeText(skill, user);
        targets.ForEach(t =>
        {
            message += "\n";
            message += Effect(skill, user, t);
        });
        return message;
    }

    private static string GetInvokeText(Skill skill, Unit user)
    {
        var message = (skill.Name == SkillName.アイテム使用) ?
            $"{user.Name}は{GameInfos.Items.Find(it => it.Id == user.CurrentCommand.itemId)}を使った！" :
            $"{user.Name}の{skill.Name}！";
        return message;
    }
    
    private static string Effect(Skill skill, Unit user, Unit target)
    {
        if (skill.Name == SkillName.アイテム使用)
        {
            var item = GameInfos.Items.Find(i => i.Id == user.CurrentCommand.itemId);
            return UseItem(item, user, target);
        }

        var message = "";
        switch (skill.Name)
        {
            case SkillName.攻撃:
            {
                var damage = CalculateAttackDamage(user, target);
                target.CurrentHp -= damage;
                message += $"{target.Name}に{damage}ダメージ";
                break;
            }
            case SkillName.ヒール:
            {
                var value = Random.Range(20, 25);
                target.CurrentHp += value;
                message += $"{target.Name}のhpが{value}回復した";
                break;
            }
            case SkillName.ファイア:
            {
                var damage = Random.Range(10, 15);
                target.CurrentHp -= damage;
                message += $"{target.Name}に{damage}のダメージ";
                break;
            }
            case SkillName.フリーズ:
            {
                var damage = Random.Range(8, 12);
                target.CurrentHp -= damage;
                message += $"{target.Name}に{damage}のダメージ";
                break;
            }
            case SkillName.防御:
            {
                message += $"{user.Name}は身を守っている";
                break;
            }
        }

        if (target?.CurrentHp <= 0) message += $"\n{target.Name}は倒れた";
        user.CurrentMp -= skill.ConsumptionMp;
        return message;
    }

    private static int CalculateAttackDamage(Unit user, Unit target)
    {
        int damage = user.Atk - target.Def / 2;
        if (target.CurrentCommand?.Skill?.Name == SkillName.防御) damage /= 2;
        if (damage < 1) return 1;
        return damage;
    }

    public static string UseItem(Item item, Unit user, Unit target)
    {
        GameInfos.Items.RemoveAll(i => i.Id == item.Id);
        var text = "";
        switch (item.Name)
        {
            case ItemName.薬草:
            {
                target.CurrentHp += 20;
                text += $"{target}のHPが20回復した";
                break;
            }
        }
        return text;
    }
}