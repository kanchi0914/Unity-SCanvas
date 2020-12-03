//  ListExtension.cs
//  http://kan-kikuchi.hatenablog.com/entry/ListExtension
//
//  Created by kan.kikuchi on 2016.04.29.

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class ListExtension
{

    /// <summary>
    /// ランダムに取得する
    /// </summary>
    public static T GetAtRandom<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            Debug.LogError("The list is empty.");
        }
        return list[Random.Range(0, list.Count)];
    }

    public static void AddItems<T>(this List<T> list, params T[] items)
    {
        items.ToList().ForEach(item => list.Add(item));
    }
    
    
    /// <summary>
    /// 指定したNoのものを取得し、リストから消す
    /// </summary>
    public static T GetAndRemove<T>(this List<T> list, int targetNo)
    {
        if (list.Count <= targetNo || targetNo < 0)
        {
            Debug.LogError("リストの範囲を超えています！(ListCount : " + list.Count + ", No : " + targetNo + ")");
        }

        T target = list[targetNo];
        list.Remove(target);
        return target;
    }

}