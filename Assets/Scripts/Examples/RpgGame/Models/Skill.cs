public enum SkillName
{
    攻撃, アイテム使用, 防御, ファイア, フリーズ, なぎ払い, ヒール
}

public enum TargetType
{
    Enemy, Ally, MySelf
}
public enum TargetScope
{
    Single, All
}

public class Skill
{

    public static int idCount = 0;
    public SkillName Name { get; set; }
    public int Id { get; private set; }
    public int ConsumptionMp { get; set; } = 0;
    public TargetType Target = TargetType.Enemy;
    public TargetScope Scope = TargetScope.Single;

    public string Description = ""; 

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