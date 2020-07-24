using System;
using Assets.Scripts.SGUI.Base;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Assets.Scripts.Extensions;

namespace SGUI.SGameObjects
{
    public class SToggle : SGameObject
    {
        private Toggle toggle;

        SImage boxImageObject;

        SImage checkImageObject;

        public Color EnabledTextColor = Color.black;
        public Color DisabledTextColor = Color.black;
        public Color EnabledBackGroundImageColor = Color.white;
        public Color DisabledBackGroundImageColor = Color.gray;
        public SText Text;

        private bool isWithBoxImage;

        public SToggle(SGameObject parent) : this(parent, name:"SToggle") { }

        public SToggle(
            SGameObject parent,
            bool isOn = false,
            bool isGrouped = false,
            bool isWithBoxImage = true,
            string name = "SToggle"
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
            Text = new SText(this, "toggleLabel");
            this.isWithBoxImage = isWithBoxImage;

            boxImageObject.SetActive(isWithBoxImage);

            SetBackGroundColor(ColorType.Gray, 1f);
            boxImageObject.SetBackGroundColor(ColorType.White, 1f);

            checkImageObject.SetImageSource(UGUIResources.Checkmark);
            UIFactory.SetFullStretchAnchor(checkImageObject.GameObject.GetComponent<RectTransform>());

            toggle.targetGraphic = boxImageObject.Image;
            toggle.graphic = checkImageObject.Image;

            Text.SetText("Text");
            Text.SetTextConfig(24, ColorType.Black, UGUIResources.Font);

            if (isGrouped && parent != null)
            {
                var group = parent.GameObject.TryAddComponent<ToggleGroup>();
                group.allowSwitchOff = false;
                toggle.group = group;
            }

            var spacing = RectSize.y * 0.1f;


            var rectsizeObserver = gameObject.ObserveEveryValueChanged(_ => RectSize);
            rectsizeObserver.Subscribe(_ => UpdateSize());

            toggle.onValueChanged.AddListener(e => OnSelected());
        }

        /// <summary>
        /// SetRectSizeしたとき変更できるように
        /// </summary>
        private void UpdateSize()
        {
            var spacing = RectSize.y * 0.1f;
            if (!isWithBoxImage)
            {
                Text.SetLocalPos(spacing * 1, spacing);
                Text.SetRectSize((int)(RectSize.x - (spacing * 2)), (int)(0.8f * RectSize.y));
            }

            else
            {
                var imageSize = 0.8f * RectSize.y;
                boxImageObject.SetLocalPos(spacing, spacing);
                boxImageObject.SetRectSize((int)imageSize, (int)imageSize);
                Text.SetLocalPos(spacing * 2 + imageSize, spacing);
                Text.SetRectSize((int)(RectSize.x - (spacing * 3 + imageSize)), (int)imageSize);
            }

        }

        public SToggle AddOnValueChangedTrue(Action action, bool isOn = true)
        {
            toggle.onValueChanged.AddListener(e =>
            {
                if (isOn)
                {
                    if (toggle.isOn) action.Invoke();
                }
                else
                {
                    if (!toggle.isOn) action.Invoke();
                }

            });
            return this;
        }

        //public SToggle SetEnabledTextColor(Color color)
        //{
        //    return this;
        //}

        //public SToggle SetDisabledTextColor()
        //{
        //    return this;
        //}

        private void OnSelected()
        {
            if (toggle.isOn)
            {
                Text.SetTextColor(EnabledTextColor);
                SetBackGroundColor(EnabledBackGroundImageColor);
            }
            else
            {
                Text.SetTextColor(DisabledTextColor);
                SetBackGroundColor(DisabledBackGroundImageColor);
            }
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

        #endregion
    }

}