using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class ItemEffecter
{
    public static string Effect(Item item, Unit user, Unit receiver)
    {
        switch (item.Name)
        {
            case ItemName.薬草:
                {
                    receiver.CurrentHp += 20;
                    return $"{receiver}のhpが20回復した";
                }
            default:
                return "";
        }
    }
}
