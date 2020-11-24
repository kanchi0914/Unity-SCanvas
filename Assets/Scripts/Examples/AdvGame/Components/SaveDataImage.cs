using System;
using System.Reflection;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    /// <summary>
    /// セーブデータの画像オブジェクト
    /// クリックでセーブ/ロードを行う
    /// </summary>
    public class SaveDataImage : EGGameObject
    {
        public int dataNumber = 0;
        private string scenarioName;
        private string sceneId;
        private int messageNumber;
        private EGGameObject saveMenu;
        private SaveData saveData;

        // 
        public SaveDataImage(
            EGGameObject parent,
            bool isSave,
            int dataNumber,
            string scenarioName,
            string sceneId,
            int messageNumber,
            SaveData saveData
        ) : base(parent)
        {
            this.saveMenu = parent;
            this.scenarioName = scenarioName;
            this.sceneId = sceneId;
            this.saveData = saveData;
            this.messageNumber = messageNumber;

            if (saveData?.Image == null)
            {
                SetImageColor(Color.gray);
                new EGText(this, "No Data")
                    .SetRectSizeByRatio(1, 1)
                    .SetMiddleCenterAnchor()
                    .SetLocalPos(0, 0)
                    .SetFullStretchAnchor();
            }
            else
            {
                SetImageSprite(saveData.Image);
            }
            

            this.dataNumber = dataNumber;
            if (isSave)
            {
                SetOnClick(AddOnClickInSaveMenu);
            }
            else
            {
                SetOnClick(AddOnClickInLoadMenu);
            }
        }


        private void AddOnClickInSaveMenu()
        {
            if (saveData == null)
            {
                Save();
            }
            else
            {
                Save();
                // var canvas = new EGCanvas("");
                // var window = new EGGameObject(canvas)
                //     .SetImageColor(Color.gray)
                //     .SetMiddleCenterAnchor()
                //     .SetRectSizeByRatio(0.3f, 0.2f)
                //     .SetLocalPos(0, 0);
                // new EGText(window, "上書きしていいですか？").SetRectSizeByRatio(1f, 0.4f)
                //     .SetTopCenterAnchor().SetLocalPos(0, 0);
                // var okButton = new EGButton(window, "OK").SetBottomCenterAnchor()
                //     .SetRectSizeByRatio(0.6f, 0.2f)
                //     .SetLocalPosByRatio(0, 0.2f) as EGButton;
                // // セーブ
                // okButton.SetOnOnClick(() => { Save(); });
                // var buttton = new CloseButton(window);
                // buttton.AddOnClick(() => canvas.DestroySelf());
            }
        }

        private void Save()
        {
            var imageFilePath = $"{GameData.SAVE_DATA_FILE_PATH}/ScreenShots/image_{dataNumber}.png";
            Debug.Log(imageFilePath);
            var saveData = new SaveData(scenarioName, sceneId, messageNumber - 1, imageFilePath);
            GameData.SaveData(saveData, dataNumber);
            CanvasStack.Pop();
        }

        private void AddOnClickInLoadMenu()
        {
            if (saveData != null)
            {
                CanvasStack.ClearAll();
                var savedata = GameData.SaveDatas[dataNumber];
                //Type type = Type.GetType(savedata.ScenarioName);
                Type type = GetTypeByClassName(savedata.ScenarioName);
                Scenario scenario = Activator.CreateInstance(type) as Scenario;
                scenario.LoadScript(savedata.SceneId, savedata.sectionNumber);
            }
        }

        public static Type GetTypeByClassName(string className)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Name == className)
                    {
                        return type;
                    }
                }
            }
            return null;
        }
    }
}