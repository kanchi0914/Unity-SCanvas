using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

namespace EGUI.GameObjects
{
    public class FileUtils
    {
        /// <summary>
        /// オブジェクトの内容をファイルから読み込み復元する
        /// </summary>
        /// <param name="path">読み込むファイル名</param>
        /// <returns>復元されたオブジェクト</returns>
        public static object LoadFromBinaryFile(string path)
        {
            FileStream fs = new FileStream(path,
                FileMode.Open,
                FileAccess.Read);
            BinaryFormatter f = new BinaryFormatter();
            //読み込んで逆シリアル化する
            object obj = f.Deserialize(fs);
            fs.Close();

            return obj;
        }

        /// <summary>
        /// オブジェクトの内容をファイルに保存する
        /// </summary>
        /// <param name="obj">保存するオブジェクト</param>
        /// <param name="path">保存先のファイル名</param>
        public static void SaveToBinaryFile(object obj, string path)
        {
            FileStream fs = new FileStream(path,
                FileMode.Create,
                FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            //シリアル化して書き込む
            bf.Serialize(fs, obj);
            fs.Close();
        }

        public static Sprite LoadSprite(string path)
        {
            try
            {
                var rawData = System.IO.File.ReadAllBytes(path);
                Texture2D texture2D = new Texture2D(0,0);
                texture2D.LoadImage(rawData);
                var sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height),
                    new Vector2(0.5f, 0.5f), 100f); 
                return sprite;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}