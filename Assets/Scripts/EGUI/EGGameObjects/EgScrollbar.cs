using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using EGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGScrollBar : EGGameObject
    {
        public EGGameObject HandleImageObject { get; private set; }

        public Scrollbar ScrollbarComponent { get; private set; }

        /// <summary>
        /// ScrollBarオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        public EGScrollBar
        (
            EGGameObject parent
        ) : this
        (
            parent.gameObject
        )
        {
        }

        /// <summary>
        /// ScrollBarオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="name">オブジェクト名</param>
        public EGScrollBar
        (
            GameObject parent,
            string name = "EGScrollBar"
        ) : base(parent, name)
        {
            gameObject.SetImageColor(Color.gray).SetImage(UGUIDefaultResources.UISprite);
            var slidingArea = new EGGameObject(gameObject, name: "Sliding Area").SetFullStretchAnchor();
            slidingArea.gameObject.GetRectTransform().sizeDelta = new Vector2(-20, -20);
            HandleImageObject = new EGGameObject(slidingArea.gameObject, name: "Handle");
            HandleImageObject.gameObject.SetImage(UGUIDefaultResources.UISprite);
            HandleImageObject.gameObject.GetRectTransform().sizeDelta = new Vector2(20, 20);
            HandleImageObject.gameObject.GetRectTransform().SetPivot(0.5f, 0.5f);
            ScrollbarComponent = gameObject.AddComponent<Scrollbar>();
            ScrollbarComponent.handleRect = HandleImageObject.gameObject.GetRectTransform();
            ScrollbarComponent.targetGraphic = HandleImageObject.gameObject.GetComponent<Image>();
        }

        public EGScrollBar SetDirection(Scrollbar.Direction direction)
        {
            ScrollbarComponent.direction = direction;
            return this;
        }
    }
}