using System.IO;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class SaveData
    {

        public string SceneId;
        public int MessageNumber;
        public int SaveDataNumber;
        public string DataImagePath;
        
        public SaveData(string sceneId, int messageNumber, string dataImagePath)
        {
            SceneId = sceneId;
            MessageNumber = messageNumber;
            DataImagePath = dataImagePath;
        }
    }
}