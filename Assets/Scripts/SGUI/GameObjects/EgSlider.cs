// using System;
// using Assets.Scripts.SGUI.Base;
// using EGUI.Base;
// using UnityEngine;
// using UnityEngine.UI;
// using UniRx;
//
// namespace EGUI.GameObjects
// {
//     class EgSlider : EGGameObject
//     {
//
//
//         private Slider slider;
//
//         EgImage boxImageObject;
//
//         EgImage checkImageObject;
//
//         EGText textObject;
//
//         EgImage backgroundImage;
//
//         EGGameObject fillArea;
//         EgImage fill;
//         EgImage handle;
//         EGGameObject handleSlideArea;
//
//         private int aaa = 0;
//
//         public EgSlider(
//             EGGameObject parent,
//             string name,
//             bool isOn = true
//         ) : base(parent, name,
//             new Func<GameObject>(() =>
//            {
//                return UIFactory.CreateBaseRect(parent.GameObject, name);
//            })
//         )
//         {
//             //, "Background"
//             this.backgroundImage = new EgImage(this);
//             backgroundImage.SetImageSource(UGUIResources.Background);
//            
//             backgroundImage.SetRectSizeByRatio(1f, 1);
//             backgroundImage.SetAnchorType(Utils.AnchorType.HorizontalStretch);
//             //, "Fill Area"
//             fillArea = new EgSubCanvas(this);
//             fillArea.SetAnchorType(Utils.AnchorType.HorizontalStretch);
//
//             //, "Fill"
//             fill = new EgImage(fillArea);
//             fill.SetImageSource(UGUIResources.UISprite);
//             fill.SetRectSizeByRatio(1, 1f);
//             fill.SetHorizontalStretchAnchor();
//
//             //, "Handle Slide Area"
//             handleSlideArea = new EgSubCanvas(this);
//             handleSlideArea.SetFullStretchAnchor();
//
//             //, "Handle"
//             handle = new EgImage(handleSlideArea);
//             handle.SetImageSource(UGUIResources.Knob);
//
//             slider = gameObject.AddComponent<Slider>();
//             slider.targetGraphic = handle.Image;
//             slider.fillRect = fill.RectTransform;
//             slider.handleRect = handle.RectTransform;
//
//             var setter = gameObject.ObserveEveryValueChanged(_ => RectSize);
//             setter.Subscribe(_ => UpdateSize());
//
//             UpdateSize();
//         }
//         
//         private void UpdateSize()
//         {
//             handleSlideArea.RectTransform.offsetMax = new Vector2(-RectSize.y / 4, 0);
//             handleSlideArea.RectTransform.offsetMin = new Vector2(-RectSize.y / 4, 0);
//
//             // バーのサイズは親rectの半分になる
//             // 4を設定すると親と同じ大きさになる
//             backgroundImage.RectTransform.offsetMax = new Vector2(0, RectSize.y / 4);
//             backgroundImage.RectTransform.offsetMin = new Vector2(0, -RectSize.y / 4);
//
//             fill.RectTransform.offsetMax = new Vector2(0, RectSize.y / 4);
//             fill.RectTransform.offsetMin = new Vector2(0, -RectSize.y / 4);
//
//             fillArea.RectTransform.offsetMax = new Vector2(0, 0);
//             fillArea.RectTransform.offsetMin = new Vector2(0, 0);
//
//             handle.RectTransform.offsetMax = new Vector2(RectSize.y, 0);
//             handle.RectTransform.offsetMin = new Vector2(0,0);
//         }
//
//         #region  RequiredMethods
//
//         public new EgSlider SetBackGroundColor(ColorType colorType, float alpha)
//         {
//             return base.SetBackGroundColor(colorType, alpha) as EgSlider;
//         }
//
//         public new EgSlider SetBackGroundColor(Color color)
//         {
//             return base.SetBackGroundColor(color) as EgSlider;
//         }
//
//         public new EgSlider SetParentSGameObject(EGGameObject parent)
//         {
//             return base.SetParentSGameObject(parent) as EgSlider;
//         }
//
//         public new EgSlider SetRectSizeByRatio(float ratioX, float ratioY)
//         {
//             base.SetRectSizeByRatio(ratioX, ratioY);
//             return this;
//         }
//
//         public new EgSlider SetLocalPosByRatio(float posXratio, float posYratio)
//         {
//             base.SetLocalPosByRatio(posXratio, posYratio);
//             return this;
//         }
//
//         //public virtual SGameObject SetRectSize(int width, int height)
//         //{
//         //    return this;
//         //}
//
//         //public virtual SGameObject SetLocalPos(int posX, int posY)
//         //{
//         //    return this;
//         //}
//
//
//         #endregion
//     }
//
// }