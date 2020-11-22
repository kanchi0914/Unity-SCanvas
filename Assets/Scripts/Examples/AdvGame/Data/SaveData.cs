using System;
using System.IO;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    [Serializable()]
    public class SaveData
    {
        public string SceneId;
        public int MessageNumber;

        public Sprite Image
        {
            get { return image; }
            private set { image = value; }
        }

        [NonSerialized]
        private Sprite image = null;
        public String ImageFilePath;
        public String ScenarioName;

        public SaveData(string scenarioName, string sceneId, int messageNumber, string imageFilePath)
        {
            ScenarioName = scenarioName;
            SceneId = sceneId;
            MessageNumber = messageNumber;
            ImageFilePath = imageFilePath;
            Init();
        }

        public void Init()
        {
            LoadScreenShotImage();
        }

        private void LoadScreenShotImage()
        {
            try
            {
                Image = FileUtils.LoadFromBinaryFile(ImageFilePath) as Sprite;
            }
            catch (Exception e)
            {
            }
        }
    }
}