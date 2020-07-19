using System;
using Assets.Scripts.SGUI.Base;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace SGUI.SGameObjects
{
    class SToggle : SGameObject
    {


        private Toggle toggle;

        SImage boxImageObject;

        SImage checkImageObject;

        SText textObject;

        private int aaa = 0;

        public SToggle(
            SGameObject parent,
            string name,
            bool isOn = true
        ) : base(parent, name,
            new Func<GameObject>(() =>
           {
               return UIFactory.CreatePanel(parent.GameObject, name);
           })
        )
        {
            toggle = gameObject.AddComponent<Toggle>();
            toggle.isOn = isOn;
            boxImageObject = new SImage(this, "toggleBackGround");

            checkImageObject = new SImage(boxImageObject, "box_gray_name")
                .SetRectSizeByRatio(1, 1);

            checkImageObject.SetImageSource(UGUIResources.Checkmark);

            textObject = new SText(this, "toggleLabel");

            UIFactory.SetFullStretchAnchor(checkImageObject.GameObject.GetComponent<RectTransform>());

            toggle.targetGraphic = boxImageObject.Image;
            toggle.graphic = checkImageObject.Image;

            textObject.SetText("Text");
            textObject.SetTextConfig(24, ColorType.Black, "Fonts/genju");

            var rectsizeObserver = gameObject.ObserveEveryValueChanged(_ => RectSize);
            rectsizeObserver.Subscribe(_ => UpdateSize());
        }

        private void UpdateSize()
        {
            SetBackGroundColor(ColorType.Gray, 1f);
            var spacing = RectSize.y * 0.1f;
            var imageSize = 0.8f * RectSize.y;

            boxImageObject.SetLocalPos(spacing, spacing);
            boxImageObject.SetRectSize((int)imageSize, (int)imageSize);

            boxImageObject.SetBackGroundColor(ColorType.White, 1f);
            var textRect = textObject.GameObject.GetComponent<RectTransform>();
            UIFactory.SetTopLeftAnchor(textRect);
            textObject.SetLocalPos(spacing * 2 + imageSize, spacing);
            textObject.SetRectSize((int)(RectSize.x - (spacing * 3 + imageSize)), (int)imageSize);
        }

        #region  RequiredMethods

        public new SToggle SetBackGroundColor(ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor(colorType, alpha) as SToggle;
        }

        public new SToggle SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SToggle;
        }

        public new SToggle SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SToggle;
        }

        public new SToggle SetRectSizeByRatio(float ratioX, float ratioY)
        {
            base.SetRectSizeByRatio(ratioX, ratioY);
            //UpdateSize();
            return this;
        }

        public new SToggle SetLocalPosByRatio(float posXratio, float posYratio)
        {
            base.SetLocalPosByRatio(posXratio, posYratio);
            //UpdateSize();
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