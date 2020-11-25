using System;
using System.Collections.Generic;
using System.Linq;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class GameData
    {
        public static SaveData[] SaveDatas = new SaveData[6];

        public static readonly String SAVE_DATA_FILE_PATH = "Assets/Scripts/Examples/AdvGame/SaveData";

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
                return KeyOptionIds.ToList().All(it => SelectedOptions.Contains(it));
            }
        }

        public static void ReadDatasFromDisc()
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