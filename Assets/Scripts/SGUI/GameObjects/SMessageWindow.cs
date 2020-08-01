using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using DG.Tweening;
using SGUI;
using SGUI.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SGUI.GameObjects
{
    public class SMessageWindow : SGameObject
    {

        public struct Script
        {
            public string message;
            public Action actionBefore;
            public Action actionAfter;
        }

        private Queue<(string message, Action action)> messageQueue = new Queue<(string message, Action action)>();
        private bool closesCanvas = true;

        private Action onSentEveryMessage;

        public SText MessageText { get; private set; }

        private static Sequence textSeq = DOTween.Sequence();

        public SMessageWindow(
            SGameObject parent,
            Queue<(string message, Action action)> messageQueue,
            Action onSentEveryMessage = null,
            bool closesCanvas = true,
            string name = "SMessageWindow"
        ) : base(parent, name,
            new Func<GameObject>(() =>
           {
                return UIFactory.CreatePanel(parent.GameObject, name) as GameObject;
           })
        )
        {
            MessageText = new SText(this, colorType: ColorType.White);
            MessageText.SetTextConfig(36, ColorType.White, UGUIResources.Font);
            MessageText.SetTextAnchor(TextAnchor.UpperLeft);
            MessageText.SetFullStretchAnchor();
            this.closesCanvas = closesCanvas;
            this.onSentEveryMessage = onSentEveryMessage;
            SetEvent();
            SetMessages(messageQueue);
        }

        private void SetEvent()
        {
            gameObject.AddComponent<ClickEventGetter>();
            var trigger = gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener(e => OnClicked());
            trigger.triggers.Add(entry);
        }

        public void SetMessages(Queue<(string message, Action action)> messageQueue)
        {
            this.messageQueue = messageQueue;
            SetNextMessage();
        }

        public void AddMessage((string message, Action action) message)
        {
            messageQueue.Enqueue(message);
        }

        public void OnClicked()
        {
            if (messageQueue.Count == 0)
            {
                if (onSentEveryMessage != null) onSentEveryMessage.Invoke();
                Dispose();
                return;
            }
            SetNextMessage();
        }

        public void SetNextMessage()
        {
            var collier = gameObject.TryAddComponent<BoxCollider2D>();
            var current = messageQueue.Dequeue();
            SetText(current.message);
            if (current.action != null) current.action.Invoke();
        }

        public SMessageWindow SetTextConfig(
            int fontSize, ColorType color, string fontName = null)
        {
            this.MessageText.SetTextConfig(fontSize, color, fontName);
            return this;
        }

        public SMessageWindow SetText(string _text)
        {
            this.MessageText.SetText(_text);
            return this;
        }

        #region  RequiredMethods

        private void SetColliderSize()
        {
            var collider = gameObject.TryAddComponent<BoxCollider2D>();
            collider.size = new Vector2(RectSize.x, RectSize.y);
            collider.offset = new Vector2(RectSize.x / 2, -RectSize.y / 2);
        }

        public new SMessageWindow SetBackGroundColor(ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor(colorType, alpha) as SMessageWindow;
        }

        public new SMessageWindow SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SMessageWindow;
        }

        public new SMessageWindow SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SMessageWindow;
        }

        public new SMessageWindow SetRectSizeByRatio(float ratioX, float ratioY)
        {
            base.SetRectSizeByRatio(ratioX, ratioY);
            SetColliderSize();
            // var collider = gameObject.GetComponent<BoxCollider2D> ();
            // collider.size = new Vector2 (RectSize.x, RectSize.y);
            // collider.offset = new Vector2 (RectSize.x / 2, -RectSize.y / 2);
            return this as SMessageWindow;
        }

        public new SMessageWindow SetRectSize(int width, int height)
        {
            base.SetRectSize(width, height);
            SetColliderSize();
            return this as SMessageWindow;
        }

        public new SMessageWindow SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as SMessageWindow;
        }

        #endregion

    }
}