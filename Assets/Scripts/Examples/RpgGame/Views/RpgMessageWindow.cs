using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using Assets.Scripts.SGUI.Base;
using DG.Tweening;
using EGUI.Base;
using EGUI.GameObjects;
using Examples.RpgGame.Views;
using UnityEngine;


namespace EGUI.Examples
{
    public struct Script
    {
        public string message;
        public Action actionBefore;
        public Action actionAfter;
    }

    public class RpgMessageWindow : EGGameObject
    {
        private Queue<(string message, Action action)> messageQueue = new Queue<(string message, Action action)>();
        private bool closesCanvas = true;

        private Action onSentEveryMessage;

        public EGText MessageText { get; private set; }

        private static Sequence textSeq = DOTween.Sequence();

        public RpgMessageWindow(
            EGGameObject parent,
            Queue<(string message, Action action)> messageQueue,
            Action onSentEveryMessage = null,
            string name = "RpgMessageWindow"
        ) : base(parent, name)
        {
            SetImage("Images/clear_box").SetSize(480, 100)
                .SetAnchorType(AnchorType.BottomCenter).SetPosition(0, 30);
            MessageText = new RpgText(this)
                .SetParagraph(TextAnchor.UpperLeft)
                .SetRelativeSize(.95f, .95f).As<RpgText>();
            this.onSentEveryMessage = onSentEveryMessage;
            AddOnClick(OnClicked);
            SetMessages(messageQueue);
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

        private void OnClicked()
        {
            if (messageQueue.Count == 0)
            {
                if (onSentEveryMessage != null) onSentEveryMessage.Invoke();
                DestroySelf();
                return;
            }
            SetNextMessage();
        }

        private void SetNextMessage()
        {
            var current = messageQueue.Dequeue();
            MessageText.SetText(current.message);
            if (current.action != null) current.action.Invoke();
        }
    }
}