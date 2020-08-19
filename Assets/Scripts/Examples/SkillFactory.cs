using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Enums;

public static class SkillFactory
{
    public static Skill GenerateSkill(Skill.SkillName skillName)
    {
        switch (skillName)
        {
            case (Skill.SkillName.ヒール):
                {
                    return new Skill(Skill.SkillName.ヒール) { Target = TargetType.Ally };
                }
            default:
                {
                    return new Skill(skillName);
                }
        }
    }
}