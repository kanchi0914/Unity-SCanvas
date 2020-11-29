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
        private int defaultWidth = 200;
        private int defaultHeight = 20;

        /// <summary>
        /// Sliderコンポーネント
        /// </summary>
        public Slider SliderComponent { get; private set; }

        /// <summary>
        /// 背景画像のオブジェクト
        /// </summary>
        public EGGameObject BackgroundImageObject { get; private set; }

        /// <summary>
        /// Handle部分のオブジェクト
        /// </summary>
        public EGGameObject HandleObject { get; private set; }

        private EGGameObject fillAreaObject;
        private EGGameObject fillObject;
        private EGGameObject handleSlideAreaObject;

        /// <summary>
        /// Sliderオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        public EgSlider
        (
            EGGameObject parent
        ) : this
        (
            parent.gameObject
        )
        {
        }

        /// <summary>
        /// Sliderオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        public EgSlider(
            GameObject parent,
            string name = "EgSlider"
        ) : base(
            parent,
            name
        )
        {
            SetSize(defaultWidth, defaultHeight);
            BackgroundImageObject = new EGGameObject(gameObject, name: "Background");
            BackgroundImageObject
                .SetImage(UGUIResources.Background)
                .SetRelativeSize(1, 1)
                .SetHorizontalStretchAnchor();

            fillAreaObject = new EGGameObject(gameObject, name: "Fill Area")
                .SetHorizontalStretchAnchor();

            fillObject = new EGGameObject(fillAreaObject.gameObject, name: "Fill");
            fillObject.SetImage(UGUIResources.UISprite)
                .SetRelativeSize(1, 1f)
                .SetHorizontalStretchAnchor();

            handleSlideAreaObject = new EGGameObject(this.gameObject, name: "Handle Slide Area");
            handleSlideAreaObject.SetFullStretchAnchor();
            handleSlideAreaObject.rectTransform.offsetMax = new Vector2(30, 30);

            HandleObject = new EGGameObject(handleSlideAreaObject.gameObject, name: "Handle");
            HandleObject.gameObject.SetImage(UGUIResources.Knob);

            SliderComponent = gameObject.AddComponent<Slider>();
            SliderComponent.targetGraphic = HandleObject.gameObject.GetComponent<Image>();
            SliderComponent.fillRect = fillObject.gameObject.GetRectTransform();
            SliderComponent.handleRect = HandleObject.gameObject.GetRectTransform();

            var setter = gameObject.ObserveEveryValueChanged(_ => rectTransform.sizeDelta);
            setter.Subscribe(_ => UpdateSize());

            UpdateSize();
        }

        private void UpdateSize()
        {
            handleSlideAreaObject.rectTransform.offsetMax = new Vector2(-rectTransform.sizeDelta.y / 2, 0);
            handleSlideAreaObject.rectTransform.offsetMin = new Vector2(-rectTransform.sizeDelta.y / 2, 0);

            // バーのサイズは親rectの半分になる
            // 4を設定すると親と同じ大きさになる
            BackgroundImageObject.rectTransform.offsetMax = new Vector2(0, rectTransform.sizeDelta.y / 4);
            BackgroundImageObject.rectTransform.offsetMin = new Vector2(0, -rectTransform.sizeDelta.y / 4);

            fillObject.rectTransform.offsetMax = new Vector2(0, rectTransform.sizeDelta.y / 4);
            fillObject.rectTransform.offsetMin = new Vector2(0, -rectTransform.sizeDelta.y / 4);

            fillAreaObject.rectTransform.offsetMax = new Vector2(0, 0);
            fillAreaObject.rectTransform.offsetMin = new Vector2(0, 0);

            HandleObject.rectTransform.offsetMax = new Vector2(rectTransform.sizeDelta.y, 0);
            HandleObject.rectTransform.offsetMin = new Vector2(0, 0);
        }
    }
}