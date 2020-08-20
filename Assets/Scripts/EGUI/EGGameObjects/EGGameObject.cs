using Assets.Scripts.Extensions;
using UnityEngine;
using EGUI.Base;

namespace EGUI.GameObjects
{
    public class EGGameObject
    {
        /// <summary>
        /// インスタンス生成時に生成したゲームオブジェクト
        /// </summary>
        public GameObject gameObject { get; private set; }
        
        /// <summary>
        /// RectTransformコンポーネント
        /// </summary>
        public RectTransform rectTransform
        {
            get => gameObject.GetRectTransform();
        }

        /// <summary>
        /// RectTransform.sizeDelta
        /// </summary>
        public Vector2 RectSize
        {
            get => rectTransform.sizeDelta;
            set => rectTransform.sizeDelta = value;
        }
        
        /// <summary>
        /// ゲームオブジェクトを生成し、参照を保持するクラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="name">オブジェクト名</param>
        public EGGameObject
        (
            GameObject parent,
            string name = "GameObject"
        )
        {
            gameObject = new GameObject(name);
            if (parent != null) gameObject.transform.SetParent(parent.transform, false);
            gameObject.GetOrAddComponent<RectTransform>().SetTopLeftAnchor();
            gameObject.SetLocalPos(0, 0).SetRectSize(100, 100);
        }
    }
}