using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Enums;

public class Item
{
    public enum ItemName
    {
        薬草,上薬草,火炎ビン
    }

    public static int idCount = 0;
    public ItemName Name { get; set; }
    public int Id { get; private set; }
    public TargetType Target = TargetType.Enemy;
    public TargetScope Scope = TargetScope.Single;
    public Item()
    {
        idCount += 10;
        Id = idCount;
    }
    public override string ToString()
    {
        return Name.ToString();
    }
}
