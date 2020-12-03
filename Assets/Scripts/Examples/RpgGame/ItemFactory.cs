namespace Examples.RpgGame
{
    public class ItemFactory
    {
        public static Item GenerateItem(ItemName name)
        {
            switch (name)
            {
                case ItemName.薬草:
                    return new Item()
                    {
                        Name = ItemName.薬草,
                        Description = "味方一人のHPを 20回復"
                    };
                default:
                    return null;
            }
        }
    }
}