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
        /// コンストラクタ
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="imageFilePath"></param>
        /// <param name="posRatioX"></param>
        /// <param name="posRatioY"></param>
        /// <param name="widthRatio"></param>
        /// <param name="heightRatio"></param>
        /// <param name="name"></param>
        public EGScrollBar
        (
            GameObject parent,
            string name = "EGScrollBar"
        ) : base(parent, name)
        {
            gameObject.SetImageColor(Color.gray).SetImageSprite(UGUIResources.UISprite);
            var slidingArea = new EGGameObject(gameObject, name: "Sliding Area").gameObject.SetFullStretchAnchor();
            slidingArea.gameObject.GetRectTransform().sizeDelta = new Vector2(-20, -20);
            HandleImageObject = new EGGameObject(slidingArea, name: "Handle");
            HandleImageObject.gameObject.SetImageSprite(UGUIResources.UISprite);
            HandleImageObject.gameObject.GetRectTransform().sizeDelta = new Vector2(20, 20);
            HandleImageObject.gameObject.GetRectTransform().SetPivot(0.5f, 0.5f);
            ScrollbarComponent = gameObject.AddComponent<Scrollbar>();
            ScrollbarComponent.handleRect = HandleImageObject.gameObject.GetRectTransform();
            ScrollbarComponent.targetGraphic = HandleImageObject.gameObject.GetComponent<Image>();
        }
    }
}