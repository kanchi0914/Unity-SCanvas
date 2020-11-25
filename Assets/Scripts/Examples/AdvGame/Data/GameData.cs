using System;
using System.Collections.Generic;
using System.Linq;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class GameData
    {
        public static List<Character> Characters = new List<Character>();
        public static SaveData[] SaveDatas = new SaveData[6];

        public static readonly String SAVE_DATA_FILE_PATH = "Assets/Scripts/Examples/AdvGame/SaveData";

        // フラグ：席について聞いた
        //public static bool HasAskedAboutSeat = false;
        
        // 選択済みの選択肢
        public static HashSet<string> SelectedOptions = new HashSet<string>();
        
        private static HashSet<string> KeyOptionIds = new HashSet<string>()
        {
            $"{nameof(Scenario1_School)}_ask_about_situation",
            $"{nameof(Scenario2_Classroom)}_ask_about_saito",
            $"{nameof(Scenario2_Classroom)}_ask_about_wallet",
            $"{nameof(Scenario2_Classroom)}_check_seat_with_info",
            $"{nameof(Scenario3_PCroom)}_search_about_incident",
            $"{nameof(Scenario3_PCroom)}_options_where_to_go"
        };

        public static bool IsAllFlagCompleted
        {
            get
            {
                //return true;
                return KeyOptionIds.ToList().All(it => SelectedOptions.Contains(it));
                // return isAllKeyOptionSelected && HasAskedAboutSeat;
            }
        } 

        public static string CurrentScenarioId;
        public static string CurrentScriptId;

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
            var tempImageFilePath = saveData.ImageFilePath;
            try
            {
                ScreenCapture.CaptureScreenshot(tempImageFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            FileUtils.SaveToBinaryFile(saveData, $"{SAVE_DATA_FILE_PATH}/data_{dataNumber + 1}");
            SaveDatas[dataNumber] = saveData;
        }

    }
}