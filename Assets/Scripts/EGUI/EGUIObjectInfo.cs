using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.EGUI
{
    public class EGUIObjectInfo : MonoBehaviour
    {
        public EGGameObject EgGameObject { get; private set; }

        public void Init(EGGameObject egGameObject)
        {
            EgGameObject = egGameObject;
        }
    }
}