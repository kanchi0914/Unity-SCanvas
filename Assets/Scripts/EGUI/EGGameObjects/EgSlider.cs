using System;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using EGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using static EGUI.Base.Utils;

namespace EGUI.GameObjects
{
    class EgSlider : EGGameObject
    {
        public Slider SliderComponent { get; private set; }
        public EGGameObject BackgroundImage { get; private set; }

        public EGGameObject FillArea { get; private set; }
        public EGGameObject Fill { get; private set; }
        public EGGameObject Handle { get; private set; }
        public EGGameObject HandleSlideArea { get; private set; }
        
        public EgSlider(
            GameObject parent,
            string name = "EgSlider"
        ) : base(
            parent,
            name
            )
        {
            BackgroundImage = new EGGameObject(this.gameObject, name: "Background");
            BackgroundImage.gameObject
                .SetImageSprite(UGUIResources.Background)
                .SetRectSizeByRatio(1, 1)
                .SetHorizontalStretchAnchor();

            FillArea = new EGGameObject(gameObject, name:"Fill Area");
            FillArea.gameObject.SetHorizontalStretchAnchor();
            
            Fill = new EGGameObject(FillArea.gameObject, name: "Fill");
            Fill.gameObject.SetImageSprite(UGUIResources.UISprite)
                .SetRectSizeByRatio(1, 1f)
                .SetHorizontalStretchAnchor();

            HandleSlideArea = new EGGameObject(this.gameObject, name:"Handle Slide Area");
            HandleSlideArea.gameObject
                .SetFullStretchAnchor();
            HandleSlideArea.gameObject.GetRectTransform().offsetMax = new Vector2(30, 30);

            Handle = new EGGameObject(HandleSlideArea.gameObject, name: "Handle");
            Handle.gameObject.SetImageSprite(UGUIResources.Knob);
            
            SliderComponent = gameObject.AddComponent<Slider>();
            SliderComponent.targetGraphic = Handle.gameObject.GetComponent<Image>();
            SliderComponent.fillRect = Fill.gameObject.GetRectTransform();
            SliderComponent.handleRect = Handle.gameObject.GetRectTransform();

            var setter = gameObject.ObserveEveryValueChanged(_ => rectTransform.sizeDelta);
            setter.Subscribe(_ => UpdateSize());

            UpdateSize();
        }
        
        private void UpdateSize()
        {
            HandleSlideArea.rectTransform.offsetMax = new Vector2(- rectTransform.sizeDelta.y / 2, 0);
            HandleSlideArea.rectTransform.offsetMin = new Vector2(- rectTransform.sizeDelta.y / 2, 0);

            // バーのサイズは親rectの半分になる
            // 4を設定すると親と同じ大きさになる
            BackgroundImage.rectTransform.offsetMax = new Vector2(0, rectTransform.sizeDelta.y / 4);
            BackgroundImage.rectTransform.offsetMin = new Vector2(0, -rectTransform.sizeDelta.y / 4);

            Fill.rectTransform.offsetMax = new Vector2(0, rectTransform.sizeDelta.y / 4);
            Fill.rectTransform.offsetMin = new Vector2(0, -rectTransform.sizeDelta.y / 4);

            FillArea.rectTransform.offsetMax = new Vector2(0, 0);
            FillArea.rectTransform.offsetMin = new Vector2(0, 0);

            Handle.rectTransform.offsetMax = new Vector2(rectTransform.sizeDelta.y, 0);
            Handle.rectTransform.offsetMin = new Vector2(0,0);
        }
        
    }

}