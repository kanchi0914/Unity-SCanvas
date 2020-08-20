//  ListExtension.cs
//  http://kan-kikuchi.hatenablog.com/entry/ListExtension
//
//  Created by kan.kikuchi on 2016.04.29.

using UnityEngine;
using System.Collections.Generic;

public static class ListExtension
{

    /// <summary>
    /// ランダムに取得する
    /// </summary>
    public static T GetAtRandom<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            Debug.LogError("リストが空です！");
        }
        return list[Random.Range(0, list.Count)];
    }

}