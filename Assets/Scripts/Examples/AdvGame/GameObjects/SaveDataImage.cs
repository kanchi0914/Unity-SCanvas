using System;
using System.Collections.Generic;
using System.Reflection;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame.GameObjects
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
        ) : base(parent.gameObject, "SaveDataImage")
        {
            this.scenarioName = scenarioName;
            this.sceneId = sceneId;
            this.saveData = saveData;
            this.messageNumber = messageNumber;

            if (saveData?.Image == null)
            {
                SetImageColor(Color.gray);
                new EGText(this, "No Data")
                    .SetRelativeSize(1, 1)
                    .SetAnchorType(AnchorType.MiddleCenter)
                    .SetPosition(0, 0)
                    .SetAnchorType(AnchorType.FullStretch);
            }
            else
            {
                SetImage(saveData.Image);
            }

            var savedDate = new EGText(this, saveData?.SavedDate)
                .SetTextPreset(GUIData.DefaultText)
                .SetParagraph(resizeTextForBestFit: true)
                .SetAnchorType(AnchorType.BottomCenter)
                .SetRelativeSize(.9f, .2f);

                this.dataNumber = dataNumber;
            if (isSave)
            {
                AddOnClick(ClickInSaveMenu);
            }
            else
            {
                AddOnClick(ClickInLoadMenu);
            }
        }


        private void ClickInSaveMenu()
        {
            if (saveData == null)
            {
                Save();
            }
            else
            {
                var saveConfirmDialogCanvas = new EGCanvas("saveConfirmDialogCanvas");
                var saveConirmWindow = new EGGameObject(saveConfirmDialogCanvas)
                    .SetImageColor(Color.gray)
                    .SetAnchorType(AnchorType.MiddleCenter)
                    .SetRelativeSize(0.3f, 0.2f)
                    .SetPosition(0, 0);
                new EGText(saveConirmWindow, "上書きしていいですか？")
                    .SetTextPreset(GUIData.DefaultText)
                    .SetRelativeSize(1f, 0.4f)
                    .SetAnchorType(AnchorType.TopCenter).SetPosition(0, 0);
                var okButton = new EGButton(saveConirmWindow, "OK").SetAnchorType(AnchorType.BottomCenter)
                    .SetRelativeSize(0.6f, 0.2f)
                    .SetRelativePosition(0, -0.2f) as EGButton;
                // セーブ
                okButton.SetOnOnClick(() =>
                {
                    saveConfirmDialogCanvas.DestroySelf();
                    Save();
                });
                var buttton = new CloseButton(saveConirmWindow);
                buttton.AddOnClick(() => saveConfirmDialogCanvas.DestroySelf());
            }
        }

        private void Save()
        {
            var imageFilePath = $"{GameData.SAVE_DATA_IMAGE_FILE_PATH}/image_{dataNumber}.png";
            var saveData = new SaveData(scenarioName, sceneId, messageNumber - 1, imageFilePath);
            saveData.SavedDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            GameData.SaveData(saveData, dataNumber);
            CanvasStack.Pop();
        }

        private void ClickInLoadMenu()
        {
            if (saveData != null)
            {
                CanvasStack.ClearAll();
                var savedata = GameData.SaveDatas[dataNumber];
                GameData.SelectedOptions = new HashSet<string>(savedata.SelectedOptions);
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