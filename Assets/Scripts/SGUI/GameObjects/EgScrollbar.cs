using Assets.Scripts.SGUI.Base;
using EGUI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGScrollBar : EgImage
    {
        public EgImage HandleImage { get; private set; }
        
        public  Scrollbar ScrollbarComponent { get; private set; }
        
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
            EGGameObject parent,
            string imageFilePath = null,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "EGScrollBar"
        ) : base(
            parent,
            null,
            posRatioX,
            posRatioY,
            widthRatio,
            heightRatio,
            false,
            name
        )
        {
            var slidingArea = new EGUIObject(this, name: "Sliding Area").SetFullStretchAnchor();
            slidingArea.RectTransform.sizeDelta = new Vector2(-20, -20);
            HandleImage = new EgImage(slidingArea, name: "Handle");
            HandleImage.SetImageSource(UGUIResources.UISprite);
            HandleImage.RectTransform.sizeDelta = new Vector2(20, 20);
            HandleImage.SetPivot(0.5f, 0.5f);
            ScrollbarComponent = GameObject.AddComponent<Scrollbar>();
            ScrollbarComponent.handleRect = HandleImage.RectTransform;
            ScrollbarComponent.targetGraphic = HandleImage.Image;
        }
    }
}