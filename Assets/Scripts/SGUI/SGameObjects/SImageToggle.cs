//using System;
//using Assets.Scripts.SGUI.Base;
//using SGUI.Base;
//using UnityEngine;
//using UnityEngine.UI;
//using UniRx;
//using System.Collections.Generic;
//using System.Linq;
//using Assets.Scripts.Extensions;

//namespace SGUI.SGameObjects
//{
//    class SImageToggle : SGameObject
//    {
//        private Toggle toggle;

//        SImage boxImageObject;

//        SImage checkImageObject;

//        SText textObject;

//        public SImageToggle(
//            SGameObject parent,
//            bool isOn = false,
//            bool isGrouped = false,
//            string imageName = "",
//            string name = "SToggle"
//        ) : base(parent, name,
//            new Func<GameObject>(() =>
//           {
//               return UIFactory.CreatePanel(parent.GameObject, name);
//           })
//        )
//        {
//            toggle = gameObject.AddComponent<Toggle>();
//            toggle.isOn = isOn;
//            boxImageObject = new SImage(this, "toggleBackGround");
//            checkImageObject = new SImage(boxImageObject, "box_gray_name")
//                .SetRectSizeByRatio(1, 1);

//            //SetBackGroundColor(ColorType.Gray, 1f);
//            //boxImageObject.SetBackGroundColor(ColorType.White, 1f);

//            checkImageObject.SetImageSource("");
//            UIFactory.SetFullStretchAnchor(checkImageObject.GameObject.GetComponent<RectTransform>());

//            toggle.targetGraphic = boxImageObject.Image;
//            toggle.graphic = checkImageObject.Image;

//            textObject.SetText("Text");
//            textObject.SetTextConfig(24, ColorType.Black, UGUIResources.Font);

//            if (isGrouped && parent != null)
//            {
//                var group = parent.GameObject.TryAddComponent<ToggleGroup>();
//                group.allowSwitchOff = true;
//                toggle.group = group;
//            }

//            var rectsizeObserver = gameObject.ObserveEveryValueChanged(_ => RectSize);
//            rectsizeObserver.Subscribe(_ => UpdateSize());
//        }

//        /// <summary>
//        /// SetRectSizeしたとき変更できるように
//        /// </summary>
//        private void UpdateSize()
//        {
//            var spacing = RectSize.y * 0.1f;
//            var imageSize = 0.8f * RectSize.y;
//            boxImageObject.SetLocalPos(spacing, spacing);
//            boxImageObject.SetRectSize((int)imageSize, (int)imageSize);

//            var textRect = textObject.GameObject.GetComponent<RectTransform>();
//            UIFactory.SetTopLeftAnchor(textRect);
//            textObject.SetLocalPos(spacing * 2 + imageSize, spacing);
//            textObject.SetRectSize((int)(RectSize.x - (spacing * 3 + imageSize)), (int)imageSize);
//        }

//        public SToggle AddOnValueChangedTrue(Action action, bool isOn = true)
//        {
//            toggle.onValueChanged.AddListener(e =>
//            {
//                if (isOn)
//                {
//                    if (toggle.isOn) action.Invoke();
//                }
//                else
//                {
//                    if (!toggle.isOn) action.Invoke();
//                }

//            });
//            return this;
//        }

//        #region  RequiredMethods

//        public new SToggle SetBackGroundColor(ColorType colorType, float alpha)
//        {
//            return base.SetBackGroundColor(colorType, alpha) as SToggle;
//        }

//        public new SToggle SetBackGroundColor(Color color)
//        {
//            return base.SetBackGroundColor(color) as SToggle;
//        }

//        public new SToggle SetParentSGameObject(SGameObject parent)
//        {
//            return base.SetParentSGameObject(parent) as SToggle;
//        }

//        public new SToggle SetRectSizeByRatio(float ratioX, float ratioY)
//        {
//            base.SetRectSizeByRatio(ratioX, ratioY);
//            //UpdateSize();
//            return this;
//        }

//        public new SToggle SetLocalPosByRatio(float posXratio, float posYratio)
//        {
//            base.SetLocalPosByRatio(posXratio, posYratio);
//            //UpdateSize();
//            return this;
//        }

//        #endregion
//    }

//}