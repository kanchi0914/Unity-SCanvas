using Assets.Scripts.Extensions;
using UnityEngine;
using EGUI.Base;

namespace EGUI.GameObjects
{
    public class EGGameObject
    {
        public GameObject gameObject { get; private set; }
        public EGGameObject
        (
            GameObject parent,
            string name
        )
        {
            gameObject = new GameObject(name);
            if (parent != null) gameObject.transform.SetParent(parent.transform, false);
            gameObject.TryAddComponent<RectTransform>().SetTopLeftAnchor();
            gameObject.SetLocalPos(0, 0).SetRectSize(100, 100);
        }
    }
}