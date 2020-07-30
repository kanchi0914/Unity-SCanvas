using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ItemEffecter
{
    public static string Effect(Item item, Unit user, Unit receiver)
    {
        switch (item.Name)
        {
            case Item.ItemName.薬草:
                {
                    receiver.CurrentHp += 20;
                    return $"{receiver}のhpが20回復した";
                }
            case Item.ItemName.上薬草:
                {
                    receiver.CurrentHp += 50;
                    return $"{receiver}のhpが50回復した";
                }
            case Item.ItemName.火炎ビン:
                {
                    receiver.CurrentHp -= 20;
                    return $"{receiver}は20のダメージ";
                }
        }
        return "";
    }
}
