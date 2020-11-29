using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame
{
    [Serializable()]
    public class SaveData : IDeserializationCallback
    {
        public string SceneId;
        [FormerlySerializedAs("MessageNumber")] public int sectionNumber;
        
        [NonSerialized] private Lazy<Sprite> image;

        public Sprite Image
        {
            get
            {
                if (image == null)
                {
                    return null;
                }
                else
                {
                    return image.Value;
                }
            }
        }
        
        public String ImageFilePath;
        public String ScenarioName;
        public HashSet<string> SelectedOptions = new HashSet<string>();
        public String SavedDate = "";

        public SaveData(string scenarioName, string sceneId, int sectionNumber, string imageFilePath)
        {
            image = new Lazy<Sprite>(LoadScreenShotImage);
            ScenarioName = scenarioName;
            SceneId = sceneId;
            this.sectionNumber = sectionNumber;
            ImageFilePath = imageFilePath;
            SelectedOptions = GameData.SelectedOptions;
        }

        public void OnDeserialization(object sender)
        {
            image = new Lazy<Sprite>(LoadScreenShotImage);
        }

        public Sprite LoadScreenShotImage()
        {
            try
            {
                var image = FileUtils.LoadSprite(ImageFilePath);
                return image;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}