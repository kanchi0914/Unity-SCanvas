using System;
using System.Collections.Generic;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class GameData
    {
        public static List<Character> Characters = new List<Character>();
        public static SaveData[] SaveDatas = new SaveData[6];

        public static readonly String SAVE_DATA_FILE_PATH = "Assets/Scripts/Examples/AdvGame/SaveData";

        public static bool HasAskedAboutSeat = false;

        public static bool IsAllFlagCompleted = false;

        static GameData()
        {
            var heroine = new Character(){Name = "ヒロ子"};
            heroine.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/ryuugakusei_seifuku_asia_woman")},
                {"joy", Resources.Load<Sprite>("Images/happy_schoolgirl")},
                {"running", Resources.Load<Sprite>("Images/chikoku_chugakusei_girl")},
                {"sumaho", Resources.Load<Sprite>("Images/smartphone_schoolgirl_stand")},
                {"tere", Resources.Load<Sprite>("Images/sick_atsui_school_girl")},
                {"smile", Resources.Load<Sprite>("Images/school_blazer_girl_kurubushi")},
                {"depressed", Resources.Load<Sprite>("Images/school_girl_cry_walk")},
                {"love", Resources.Load<Sprite>("Images/message_loveletter_girl")},
            };
            var saito = new Character(){Name = "斎藤"};
            saito.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/shisyunki_girl2")},
            };
            var danshi = new Character(){Name = "男子生徒"};
            danshi.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/himan_pocchari_blazer_schoolboy")},
            };
            var joshi = new Character(){Name = "女子生徒"};
            joshi.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/school_joshikousei_kogyaru_90s")},
            };
            Characters.Add(heroine);
            Characters.Add(saito);
            Characters.Add(danshi);
            Characters.Add(joshi);
        }
        
        public static void LoadData()
        {
            for (int i = 0; i < 6; i++){
                SaveData data = null;
                try
                {
                    data = FileUtils.LoadFromBinaryFile($"{SAVE_DATA_FILE_PATH}/data_{i + 1}") as SaveData;
                    data.Init();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                SaveDatas[i] = data;
            }
        }
        
        public static void SaveData(SaveData saveData, int dataNumber)
        {
            var tempImageFilePath = $"{SAVE_DATA_FILE_PATH}/ScreenShots/image_{dataNumber}";
            ScreenCapture.CaptureScreenshot(tempImageFilePath);
            FileUtils.SaveToBinaryFile(saveData, $"{SAVE_DATA_FILE_PATH}/data_{dataNumber + 1}");
            SaveDatas[dataNumber] = saveData;
        }

    }
}