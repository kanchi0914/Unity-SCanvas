using System;
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
        public EgImage BackgroundImage { get; private set; }

        public EGGameObject FillArea { get; private set; }
        public EgImage Fill { get; private set; }
        public EgImage Handle { get; private set; }
        public EGGameObject HandleSlideArea { get; private set; }
        
        public EgSlider(
            EGGameObject parent,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "EgSlider"
        ) : base(
            parent,
            posRatioX,
            posRatioY,
            widthRatio,
            heightRatio,
            name,
            () => UIFactory.CreateBaseRect(parent.GameObject, name)
            )
        {
            //, "Background"
            BackgroundImage = new EgImage(this, name: "Background");
            BackgroundImage.SetImageSource(UGUIResources.Background);
           
            BackgroundImage.SetRectSizeByRatio(1f, 1);
            BackgroundImage.SetAnchorType(AnchorType.HorizontalStretch);

            FillArea = new EGUIObject(this, name:"Fill Area");
            FillArea.SetAnchorType(AnchorType.HorizontalStretch);
            
            Fill = new EgImage(FillArea, name: "Fill");
            Fill.SetImageSource(UGUIResources.UISprite);
            Fill.SetRectSizeByRatio(1, 1f);
            Fill.SetHorizontalStretchAnchor();

            HandleSlideArea = new EGUIObject(this, name:"Handle Slide Area");
            HandleSlideArea.SetFullStretchAnchor();
            HandleSlideArea.RectTransform.offsetMax = new Vector2(30, 30);

            Handle = new EgImage(HandleSlideArea, name: "Handle");
            Handle.SetImageSource(UGUIResources.Knob);
            
            SliderComponent = gameObject.AddComponent<Slider>();
            SliderComponent.targetGraphic = Handle.Image;
            SliderComponent.fillRect = Fill.RectTransform;
            SliderComponent.handleRect = Handle.RectTransform;

            var setter = gameObject.ObserveEveryValueChanged(_ => RectSize);
            setter.Subscribe(_ => UpdateSize());

            UpdateSize();
        }
        
        private void UpdateSize()
        {
            HandleSlideArea.RectTransform.offsetMax = new Vector2(- RectSize.y / 2, 0);
            HandleSlideArea.RectTransform.offsetMin = new Vector2(- RectSize.y / 2, 0);

            // バーのサイズは親rectの半分になる
            // 4を設定すると親と同じ大きさになる
            BackgroundImage.RectTransform.offsetMax = new Vector2(0, RectSize.y / 4);
            BackgroundImage.RectTransform.offsetMin = new Vector2(0, -RectSize.y / 4);

            Fill.RectTransform.offsetMax = new Vector2(0, RectSize.y / 4);
            Fill.RectTransform.offsetMin = new Vector2(0, -RectSize.y / 4);

            FillArea.RectTransform.offsetMax = new Vector2(0, 0);
            FillArea.RectTransform.offsetMin = new Vector2(0, 0);

            Handle.RectTransform.offsetMax = new Vector2(RectSize.y, 0);
            Handle.RectTransform.offsetMin = new Vector2(0,0);
        }
        
    }

}