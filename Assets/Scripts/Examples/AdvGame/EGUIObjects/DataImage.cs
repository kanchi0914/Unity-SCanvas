using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame.Objects
{
    public class DataImage : EGGameObject
    {
        public string imageFilePath = null;
        public int dataNumber = 0;
        private string sceneId;
        private int messageNumber;
        private EGGameObject saveMenu;

        public DataImage(
            EGGameObject parent,
            bool isSave,
            int dataNumber, string sceneId, int messageNumber,
            string imageFilePath = null) : base(parent)
        {
            this.saveMenu = parent;
            this.sceneId = sceneId;
            this.messageNumber = messageNumber;
            if (imageFilePath != null)
            {
                this.imageFilePath = imageFilePath;
                SetImage(imageFilePath).SetImageColor(Color.white);
            }
            else
            {
                SetImageColor(Color.gray);
                new EGText(this, "No Data")
                    .SetRectSizeByRatio(1, 1)
                    .SetMiddleCenterAnchor()
                    .SetLocalPos(0, 0)
                    .SetFullStretchAnchor();
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
            if (imageFilePath == null)
            {
                Save();
            }
            else
            {
                var canvas = new EGCanvas("");
                var window = new EGGameObject(canvas)
                    .SetImageColor(Color.gray)
                    .SetMiddleCenterAnchor()
                    .SetRectSizeByRatio(0.3f, 0.2f)
                    .SetLocalPos(0, 0);
                new EGText(window, "上書きしていいですか？").SetRectSizeByRatio(1f, 0.4f)
                    .SetTopCenterAnchor().SetLocalPos(0, 0);
                var okButton = new EGButton(window, "OK").SetBottomCenterAnchor()
                    .SetRectSizeByRatio(0.6f, 0.2f)
                    .SetLocalPosByRatio(0, 0.2f) as EGButton;
                okButton.SetOnOnClick(Save);
                okButton.AddOnClick(() => { CanvasStack.Pop(); });
                var buttton = new CloseButton(window);
                buttton.AddOnClick(() => canvas.DestroySelf());
            }
        }

        private void Save()
        {
            CanvasStack.Pop();
            ScreenCapture.CaptureScreenshot("Assets/Resources/SaveData/SuperScreenShot.png");
            var saveDate = new SaveData(sceneId, messageNumber, "SaveData/SuperScreenShot");
            GameData.SaveSatas[dataNumber] = saveDate;
        }

        private void AddOnClickInLoadMenu()
        {
            if (imageFilePath != null)
            {
                CanvasStack.ClearAll();
                var savedata = GameData.SaveSatas[dataNumber];
                //new Scenario().Init();
            }
        }
    }
}