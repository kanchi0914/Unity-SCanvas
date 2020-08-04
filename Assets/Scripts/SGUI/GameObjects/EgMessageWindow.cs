// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Runtime.Versioning;
// using System.Text;
// using System.Threading.Tasks;
// using Assets.Scripts.Extensions;
// using Assets.Scripts.SGUI.Base;
// using DG.Tweening;
// using EGUI.Base;
// using EGUI;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
//
// namespace EGUI.GameObjects
// {
//     public class EgMessageWindow : EGGameObject
//     {
//
//         public struct Script
//         {
//             public string message;
//             public Action actionBefore;
//             public Action actionAfter;
//         }
//
//         private Queue<(string message, Action action)> messageQueue = new Queue<(string message, Action action)>();
//         private bool closesCanvas = true;
//
//         private Action onSentEveryMessage;
//
//         public EGText MessageText { get; private set; }
//
//         private static Sequence textSeq = DOTween.Sequence();
//
//         public EgMessageWindow(
//             EGGameObject parent,
//             Queue<(string message, Action action)> messageQueue,
//             Action onSentEveryMessage = null,
//             bool closesCanvas = true,
//             string name = "SMessageWindow"
//         ) : base(parent, name,
//             new Func<GameObject>(() =>
//            {
//                 return UIFactory.CreatePanel(parent.GameObject, name) as GameObject;
//            })
//         )
//         {
//             MessageText = new EGText(this, colorType: ColorType.White);
//             MessageText.SetTextConfig(36, ColorType.White, UGUIResources.Font);
//             MessageText.SetTextAlignment(TextAnchor.UpperLeft);
//             MessageText.SetFullStretchAnchor();
//             this.closesCanvas = closesCanvas;
//             this.onSentEveryMessage = onSentEveryMessage;
//             SetEvent();
//             SetMessages(messageQueue);
//         }
//
//         private void SetEvent()
//         {
//             gameObject.AddComponent<ClickEventGetter>();
//             var trigger = gameObject.AddComponent<EventTrigger>();
//             var entry = new EventTrigger.Entry();
//             entry.eventID = EventTriggerType.PointerClick;
//             entry.callback.AddListener(e => OnClicked());
//             trigger.triggers.Add(entry);
//         }
//
//         public void SetMessages(Queue<(string message, Action action)> messageQueue)
//         {
//             this.messageQueue = messageQueue;
//             SetNextMessage();
//         }
//
//         public void AddMessage((string message, Action action) message)
//         {
//             messageQueue.Enqueue(message);
//         }
//
//         public void OnClicked()
//         {
//             if (messageQueue.Count == 0)
//             {
//                 if (onSentEveryMessage != null) onSentEveryMessage.Invoke();
//                 Dispose();
//                 return;
//             }
//             SetNextMessage();
//         }
//
//         public void SetNextMessage()
//         {
//             var collier = gameObject.TryAddComponent<BoxCollider2D>();
//             var current = messageQueue.Dequeue();
//             SetText(current.message);
//             if (current.action != null) current.action.Invoke();
//         }
//
//         public EgMessageWindow SetTextConfig(
//             int fontSize, ColorType color, string fontName = null)
//         {
//             this.MessageText.SetTextConfig(fontSize, color, fontName);
//             return this;
//         }
//
//         public EgMessageWindow SetText(string _text)
//         {
//             this.MessageText.SetText(_text);
//             return this;
//         }
//
//         #region  RequiredMethods
//
//         private void SetColliderSize()
//         {
//             var collider = gameObject.TryAddComponent<BoxCollider2D>();
//             collider.size = new Vector2(RectSize.x, RectSize.y);
//             collider.offset = new Vector2(RectSize.x / 2, -RectSize.y / 2);
//         }
//
//         public new EgMessageWindow SetBackGroundColor(ColorType colorType, float alpha)
//         {
//             return base.SetBackGroundColor(colorType, alpha) as EgMessageWindow;
//         }
//
//         public new EgMessageWindow SetBackGroundColor(Color color)
//         {
//             return base.SetBackGroundColor(color) as EgMessageWindow;
//         }
//
//         public new EgMessageWindow SetParentSGameObject(EGGameObject parent)
//         {
//             return base.SetParentSGameObject(parent) as EgMessageWindow;
//         }
//
//         public new EgMessageWindow SetRectSizeByRatio(float ratioX, float ratioY)
//         {
//             base.SetRectSizeByRatio(ratioX, ratioY);
//             SetColliderSize();
//             // var collider = gameObject.GetComponent<BoxCollider2D> ();
//             // collider.size = new Vector2 (RectSize.x, RectSize.y);
//             // collider.offset = new Vector2 (RectSize.x / 2, -RectSize.y / 2);
//             return this as EgMessageWindow;
//         }
//
//         public new EgMessageWindow SetRectSize(int width, int height)
//         {
//             base.SetRectSize(width, height);
//             SetColliderSize();
//             return this as EgMessageWindow;
//         }
//
//         public new EgMessageWindow SetLocalPosByRatio(float posXratio, float posYratio)
//         {
//             return base.SetLocalPosByRatio(posXratio, posYratio) as EgMessageWindow;
//         }
//
//         #endregion
//
//     }
// }