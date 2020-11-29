// using System;
// using System.Collections.Generic;
// using Assets.Scripts.Examples.AdvGame;
// using Assets.Scripts.Extensions;
// using Assets.Scripts.SGUI.Base;
// using DG.Tweening;
// using EGUI.Base;
// using EGUI.Base;
// using UnityEngine;
// using static Assets.Scripts.Examples.AdvGame.GUIData;
//
// namespace EGUI.GameObjects
// {
//     public class EgMessageWindow : EGGameObject
//     {
//         public EGText MessageText { get; private set; }
//
//         public EGGameObject TalkerPanelImage;
//         public EGText TalkerText;
//
//         public EgMessageWindow(
//             EGCanvas canvas,
//             string name = "SMessageWindow"
//         ) : base(canvas, name)
//         {
//             SetAnchorType(RectTransformExtensions.AnchorType.BottomCenter, false)
//                 .SetPosition(0, 20)
//                 .SetSize(600, 150)
//                 .SetImageColor(Color.gray, 0.5f);
//
//             var menus = new EgHorizontalLayoutView(
//                     this,
//                     isAutoSizingHeight: true,
//                     isAutoSizingWidth: true
//                 )
//                 .SetTopRightAnchor()
//                 .SetSize(300, 20);
//             
//             var logText = new EGText(menus, "バックログ", true).SetTextPreset(DefaultText);
//             var saveText = new EGText(menus, "セーブ", true).SetTextPreset(DefaultText);
//             var loadText = new EGText(menus, "ロード", true).SetTextPreset(DefaultText);
//             var optionText = new EGText(menus, "オプション", true).SetTextPreset(DefaultText);
//
//             MessageText = new EGText(gameObject, "Test text")
//                 .SetParagraph(alignment: TextAnchor.UpperLeft)
//                 .SetText("This it a text!")
//                 .SetTextPreset(DefaultText)
//                 .SetAnchorType(RectTransformExtensions.AnchorType.TopCenter, false)
//                 .SetPosition(0, -20)
//                 .SetSize(550, 120)
//                 .SetOnClick(OnClicked) as EGText;
//             
//             TalkerPanelImage = new EGGameObject(this)
//                 .SetImageColor(Color.gray, 0.5f)
//                 .SetTopLeftAnchor()
//                 .SetPosition(0, 40)
//                 .SetSize(200, 40);
//             
//             TalkerText = new EGText(TalkerPanelImage.gameObject, "Text")
//                     .SetTextPreset(DefaultText)
//                     .SetRelativeSize(1,1)
//                 as EGText;
//             gameObject.GetImageComponent().raycastTarget = true;
//         }
//
//         protected virtual void OnClicked()
//         {
//             Debug.Log("dasdadsasdasdas");
//         }
//
//         public void SetTalkerText(string talker)
//         {
//             TalkerText.SetText(talker);
//         }
//
//         public void SetMessageText(string message)
//         {
//             MessageText.SetText(message);
//         }
//     }
// }