public static class SkillFactory
{
    public static Skill GenerateSkill(SkillName skillName)
    {
        switch (skillName)
        {
            case (SkillName.ヒール):
                {
                    return new Skill(skillName)
                    {
                        Description = "味方一人のHPを 20-30回復",
                        Target = TargetType.Ally,
                        ConsumptionMp = 4
                    };
                }
            case (SkillName.ファイア):
            {
                return new Skill(skillName)
                {
                    Description = "敵一体に 10-15ダメージ",
                    ConsumptionMp = 3,
                    Scope = TargetScope.Single
                };
            }
            case (SkillName.フリーズ):
            {
                return new Skill(skillName)
                {
                    Description = "敵全体に 8-12ダメージ",
                    ConsumptionMp = 5,
                    Scope = TargetScope.All
                };
            }
            default:
                {
                    return new Skill(skillName);
                }
        }
    }
}