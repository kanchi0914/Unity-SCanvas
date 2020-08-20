// using System;
// using System.Collections.Generic;
// using Assets.Scripts.SGUI.Base;
// using DG.Tweening;
// using EGUI.Base;
// using UnityEngine;
//
// namespace EGUI.GameObjects
// {
//     public class EgMessageWindow : EgSelectableImage
//     {
//         public struct Section
//         {
//             public String Talker;
//             public String Text;
//             public Action Action;
//
//             public Section(string talker, string message, Action action = null)
//             {
//                 Talker = talker;
//                 Text = message;
//                 Action = action;
//             }
//         }
//
//         public List<Section> SentSections = new List<Section>();
//
//         public Queue<Section> MessageQueue = new Queue<Section>();
//         private bool closesCanvas = true;
//
//         public Action OnSentEveryMessage { get; protected set; }
//
//         public EGText MessageText { get; private set; }
//
//         private static Sequence textSeq = DOTween.Sequence();
//
//         public EGImage TalkerPanelImage;
//         public EGText TalkerText;
//
//         public EgMessageWindow(
//             EGGameObject parent,
//             EgSelectableImage clickTarget = null,
//             string name = "SMessageWindow"
//         ) : base(parent, null, name)
//         {
//             MessageText = new EGText(this)
//                 .SetTextConfig(36, Color.white, font: UGUIResources.Font)
//                 .SetTextAlignment(TextAnchor.UpperLeft)
//                 .SetFullStretchAnchor() as EGText;
//             if (clickTarget != null) clickTarget.AddOnClick(OnClicked);
//             else SetOnClick(OnClicked);
//             SetColor(ColorType.Gray, 0.5f);
//
//             TalkerPanelImage = new EGImage(this)
//                     .SetColor(Color.gray, 0.5f)
//                     .SetTopLeftAnchor()
//                     .SetLocalPos(0, -60)
//                     .SetRectSize(200, 60)
//                 as EGImage;
//
//             TalkerText = new EGText(TalkerPanelImage)
//                     .SetRectSizeByRatio(1, 1)
//                     .SetFullStretchAnchor()
//                 as EGText;
//             TalkerText.SetText("");
//         }
//
//         /// <summary>
//         /// 全てのメッセージを送り終わった後に呼ばれるActionを設定します。
//         /// </summary>
//         /// <param name="onSentEveryMessage"></param>
//         /// <returns></returns>
//         public EgMessageWindow SetOnSentEveryMessage(Action onSentEveryMessage)
//         {
//             this.OnSentEveryMessage = onSentEveryMessage;
//             return this;
//         }
//
//         /// <summary>
//         /// メッセージキューを新たに設定します。
//         /// </summary>
//         /// <param name="messageAndActions"></param>
//         public void SetMessageAndActions(List<Section> messageAndActions)
//         {
//             MessageQueue = new Queue<Section>(messageAndActions);
//             SetNextMessage();
//         }
//
//         /// <summary>
//         /// メッセージキューにメッセージを追加します。
//         /// </summary>
//         /// <param name="message"></param>
//         public void AddMessageAndAction(Section messageAndActions)
//         {
//             MessageQueue.Enqueue(messageAndActions);
//         }
//
//         /// <summary>
//         /// メッセージキューにメッセージのリストを追加します。
//         /// </summary>
//         /// <param name="message"></param>
//         public void AddMessageAndAction(List<Section> messageAndActions)
//         {
//             messageAndActions.ForEach(ma => MessageQueue.Enqueue(ma));
//         }
//
//         protected virtual void OnClicked()
//         {
//             if (MessageQueue.Count == 0)
//             {
//                 OnSentEveryMessage?.Invoke();
//                 DestroySelf();
//                 return;
//             }
//             SetNextMessage();
//         }
//
//         protected virtual void SetNextMessage()
//         {
//             var current = MessageQueue.Dequeue();
//             SentSections.Add(current);
//             MessageText.SetText(current.Text);
//             TalkerText.SetText(current.Talker);
//             current.Action?.Invoke();
//         }
//     }
// }