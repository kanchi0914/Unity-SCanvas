using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Examples.AdvGame;
using Assets.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;
using static EGUI.Base.Utils;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEditor;

public class rMain : MonoBehaviour
{
    void Start()
    {
        // var obj = GameObject.Find("Sample");
        // var prefab = PrefabUtility.SaveAsPrefabAsset(obj.gameObject, "Assets/BBB.prefab");
        // AssetDatabase.SaveAssets();
        new AdvGameOpening();
        //a.gameObject.transform.SetPivot();
        //var save = new SaveData("1", 10, "path/to/image");
        // var path = "Assets/Scripts/Examples/AdvGame/Sdsadadsasd1"
        // var data = Main.LoadFromBinaryFile(path);
        // Debug.Log("saved!");
    }
    
    // /// <summary>
    // /// オブジェクトの内容をファイルから読み込み復元する
    // /// </summary>
    // /// <param name="path">読み込むファイル名</param>
    // /// <returns>復元されたオブジェクト</returns>
    // public static object LoadFromBinaryFile(string path)
    // {
    //     FileStream fs = new FileStream(path,
    //         FileMode.Open,
    //         FileAccess.Read);
    //     BinaryFormatter f = new BinaryFormatter();
    //     //読み込んで逆シリアル化する
    //     object obj = f.Deserialize(fs);
    //     fs.Close();
    //
    //     return obj;
    // }
    //
    // /// <summary>
    // /// オブジェクトの内容をファイルに保存する
    // /// </summary>
    // /// <param name="obj">保存するオブジェクト</param>
    // /// <param name="path">保存先のファイル名</param>
    // public static void SaveToBinaryFile(object obj, string path)
    // {
    //     FileStream fs = new FileStream(path,
    //         FileMode.Create,
    //         FileAccess.Write);
    //     BinaryFormatter bf = new BinaryFormatter();
    //     //シリアル化して書き込む
    //     bf.Serialize(fs, obj);
    //     fs.Close();
    // }
}