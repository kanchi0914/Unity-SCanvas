using System;

public enum ItemName
{
    薬草
}

public class Item
{
    public static int idCount = 0;
    public ItemName Name { get; set; }
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public TargetType Target { get; set; } = TargetType.Ally;
    public TargetScope Scope { get; set; } = TargetScope.Single;
    public string Description { get; set; }
    public Item()
    {
    }
    
    public override string ToString()
    {
        return Name.ToString();
    }

    public override bool Equals(object obj)
    {
        var item = obj as Item;
        return item.Id == this.Id;
    }
}
