// using System;
// using Assets.Scripts.SGUI.Base;
// using UnityEngine;
// using UnityEngine.UI;
// using UniRx;
// using Assets.Scripts.Extensions;
// using EGUI.Base;
//
// namespace EGUI.GameObjects
// {
//     public class EgToggle : EGGameObject
//     {
//         private Toggle toggle;
//
//         EgImage boxImageObject;
//
//         EgImage checkImageObject;
//
//         public Color EnabledTextColor = Color.black;
//         public Color DisabledTextColor = Color.black;
//         public Color EnabledBackGroundImageColor = Color.white;
//         public Color DisabledBackGroundImageColor = Color.gray;
//         public EGText Text;
//
//         private bool isWithBoxImage;
//
//         public EgToggle(EGGameObject parent) : this(parent, name:"SToggle") { }
//
//         public EgToggle(
//             EGGameObject parent,
//             bool isOn = false,
//             bool isGrouped = false,
//             bool isWithBoxImage = true,
//             string name = "SToggle"
//         ) : base(parent, name,
//             new Func<GameObject>(() =>
//            {
//                return UIFactory.CreatePanel(parent.GameObject, name);
//            })
//         )
//         {
//             toggle = gameObject.AddComponent<Toggle>();
//             toggle.isOn = isOn;
//             boxImageObject = new EgImage(this, "toggleBackGround");
//             checkImageObject = new EgImage(boxImageObject, "box_gray_name")
//                 .SetRectSizeByRatio(1, 1);
//             Text = new EGText(this, "toggleLabel");
//             this.isWithBoxImage = isWithBoxImage;
//
//             boxImageObject.SetActive(isWithBoxImage);
//
//             SetBackGroundColor(ColorType.Gray, 1f);
//             boxImageObject.SetBackGroundColor(ColorType.White, 1f);
//
//             checkImageObject.SetImageSource(UGUIResources.Checkmark);
//
//             checkImageObject.SetAnchorType(Utils.AnchorType.FullStretch);
//             //UIFactory.SetFullStretchAnchor(checkImageObject.GameObject.GetComponent<RectTransform>());
//
//             toggle.targetGraphic = boxImageObject.Image;
//             toggle.graphic = checkImageObject.Image;
//
//             Text.SetText("Text");
//             Text.SetTextConfig(24, ColorType.Black, UGUIResources.Font);
//
//             if (isGrouped && parent != null)
//             {
//                 var group = parent.GameObject.TryAddComponent<ToggleGroup>();
//                 group.allowSwitchOff = false;
//                 toggle.group = group;
//             }
//
//             RectSize = new Vector2(100, 20);
//             UpdateSize();
//
//             var spacing = RectSize.y * 0.1f;
//
//             var rectsizeObserver = gameObject.ObserveEveryValueChanged(_ => RectSize);
//             rectsizeObserver.Subscribe(_ => UpdateSize());
//
//             toggle.onValueChanged.AddListener(e => OnSelected());
//         }
//
//         /// <summary>
//         /// SetRectSizeしたとき変更できるように
//         /// </summary>
//         private void UpdateSize()
//         {
//             var spacing = RectSize.y * 0.1f;
//             if (!isWithBoxImage)
//             {
//                 Text.SetLocalPos(spacing * 1, spacing);
//                 Text.SetRectSize((int)(RectSize.x - (spacing * 2)), (int)(0.8f * RectSize.y));
//             }
//             else
//             {
//                 var imageSize = 0.8f * RectSize.y;
//                 boxImageObject.SetAnchorType(Utils.AnchorType.TopLeft);
//                 boxImageObject.SetLocalPos(spacing, spacing);
//                 boxImageObject.SetRectSize((int)imageSize, (int)imageSize);
//                 Text.SetLocalPos(spacing * 2 + imageSize, spacing);
//                 Text.SetRectSize((int)(RectSize.x - (spacing * 3 + imageSize)), (int)imageSize);
//             }
//
//         }
//
//         public EgToggle AddOnValueChangedTrue(Action action, bool isOn = true)
//         {
//             toggle.onValueChanged.AddListener(e =>
//             {
//                 if (isOn)
//                 {
//                     if (toggle.isOn) action.Invoke();
//                 }
//                 else
//                 {
//                     if (!toggle.isOn) action.Invoke();
//                 }
//
//             });
//             return this;
//         }
//
//         //public SToggle SetEnabledTextColor(Color color)
//         //{
//         //    return this;
//         //}
//
//         //public SToggle SetDisabledTextColor()
//         //{
//         //    return this;
//         //}
//
//         private void OnSelected()
//         {
//             if (toggle.isOn)
//             {
//                 Text.SetTextColor(EnabledTextColor);
//                 SetBackGroundColor(EnabledBackGroundImageColor);
//             }
//             else
//             {
//                 Text.SetTextColor(DisabledTextColor);
//                 SetBackGroundColor(DisabledBackGroundImageColor);
//             }
//         }
//
//         #region  RequiredMethods
//
//         public new EgToggle SetBackGroundColor(ColorType colorType, float alpha)
//         {
//             return base.SetBackGroundColor(colorType, alpha) as EgToggle;
//         }
//
//         public new EgToggle SetBackGroundColor(Color color)
//         {
//             return base.SetBackGroundColor(color) as EgToggle;
//         }
//
//         public new EgToggle SetParentSGameObject(EGGameObject parent)
//         {
//             return base.SetParentSGameObject(parent) as EgToggle;
//         }
//
//         public new EgToggle SetRectSizeByRatio(float ratioX, float ratioY)
//         {
//             base.SetRectSizeByRatio(ratioX, ratioY);
//             //UpdateSize();
//             return this;
//         }
//
//         public new EgToggle SetLocalPosByRatio(float posXratio, float posYratio)
//         {
//             base.SetLocalPosByRatio(posXratio, posYratio);
//             //UpdateSize();
//             return this;
//         }
//
//         #endregion
//     }
//
// }