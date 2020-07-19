using System;
using Assets.Scripts.SGUI.Base;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace SGUI.SGameObjects
{
    class SSlider : SGameObject
    {


        private Slider slider;

        SImage boxImageObject;

        SImage checkImageObject;

        SText textObject;

        SImage backgroundImage;

        SGameObject fillArea;
        SImage fill;
        SImage handle;
        SGameObject handleSlideArea;

        private int aaa = 0;

        public SSlider(
            SGameObject parent,
            string name,
            bool isOn = true
        ) : base(parent, name,
            new Func<GameObject>(() =>
           {
               return UIFactory.CreateBaseRect(parent.GameObject, name);
           })
        )
        {
            this.backgroundImage = new SImage(this, "Background");
            backgroundImage.SetImageSource(UGUIResources.Background);
           
            backgroundImage.SetRectSizeByRatio(1f, 1);

            UIFactory.SetHorizontalStretchAnchor(backgroundImage.RectTransform);
            fillArea = new SBaseRect(this, "Fill Area");

            UIFactory.SetHorizontalStretchAnchor(fillArea.RectTransform);
            fill = new SImage(fillArea, "Fill");
            fill.SetImageSource(UGUIResources.UISprite);
            fill.SetRectSizeByRatio(1, 1f);

            UIFactory.SetStretchLeftAnchor(fill.RectTransform);

            fill.RectTransform.offsetMax = new Vector2(0, RectSize.y / 2);
            fill.RectTransform.offsetMin = new Vector2(0, -RectSize.y / 2);

            handleSlideArea = new SBaseRect(this, "Handle Slide Area");
            UIFactory.SetFullStretchAnchor(handleSlideArea.RectTransform);
            handleSlideArea.RectTransform.offsetMax = new Vector2(-RectSize.y / 2, 0);
            handleSlideArea.RectTransform.offsetMin = new Vector2(- RectSize.y / 2, 0);

            handle = new SImage(handleSlideArea, "Handle");
            handle.SetImageSource(UGUIResources.Knob);

            UIFactory.SetStretchLeftAnchor(handle.RectTransform);

            handle.RectTransform.offsetMax = new Vector2(RectSize.y, 0);
            handle.RectTransform.offsetMin = new Vector2(0, 0);

            slider = gameObject.AddComponent<Slider>();
            slider.targetGraphic = handle.Image;
            slider.fillRect = fill.RectTransform;
            slider.handleRect = handle.RectTransform;

            var setter = gameObject.ObserveEveryValueChanged(_ => RectSize);
            setter.Subscribe(_ => UpdateSize());

            slider.onValueChanged.AddListener(e => onValueChanged());
        }

        private void onValueChanged()
        {
            Debug.Log(this.slider.value);
        }

        private void UpdateSize()
        {
            handleSlideArea.RectTransform.offsetMax = new Vector2(-RectSize.y / 2, 0);
            handleSlideArea.RectTransform.offsetMin = new Vector2(-RectSize.y / 2, 0);

            // バーのサイズは親rectの半分になる
            // 4を設定すると親と同じ大きさになる
            backgroundImage.RectTransform.offsetMax = new Vector2(0, RectSize.y / 4);
            backgroundImage.RectTransform.offsetMin = new Vector2(0, -RectSize.y / 4);

            fill.RectTransform.offsetMax = new Vector2(0, RectSize.y / 4);
            fill.RectTransform.offsetMin = new Vector2(0, -RectSize.y / 4);

            // (縦の大きさ　)
            // 
            handle.RectTransform.offsetMax = new Vector2(RectSize.y, 0);
            handle.RectTransform.offsetMin = new Vector2(0,0);
        }

        #region  RequiredMethods

        public new SSlider SetBackGroundColor(ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor(colorType, alpha) as SSlider;
        }

        public new SSlider SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SSlider;
        }

        public new SSlider SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SSlider;
        }

        public new SSlider SetRectSizeByRatio(float ratioX, float ratioY)
        {
            base.SetRectSizeByRatio(ratioX, ratioY);
            return this;
        }

        public new SSlider SetLocalPosByRatio(float posXratio, float posYratio)
        {
            base.SetLocalPosByRatio(posXratio, posYratio);
            return this;
        }

        //public virtual SGameObject SetRectSize(int width, int height)
        //{
        //    return this;
        //}

        //public virtual SGameObject SetLocalPos(int posX, int posY)
        //{
        //    return this;
        //}


        #endregion
    }

}