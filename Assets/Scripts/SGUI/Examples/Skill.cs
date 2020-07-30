using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Enums;

public class Skill
{
    public enum SkillName
    {
        攻撃, アイテム使用, 防御, ファイア, なぎ払い, ヒール
    }
    public static int idCount = 0;
    public SkillName Name { get; set; }
    public int Id { get; private set; }
    public int ConsumptionMp { get; set; } = 0;
    public TargetType Target = TargetType.Enemy;
    public TargetScope Scope = TargetScope.Single;

    public Skill(SkillName skillName)
    {
        this.Name = skillName;
        Id = idCount++;
    }
    public override string ToString()
    {
        return Name.ToString();
    }
}