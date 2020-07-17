using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using DG.Tweening;
using SGUI;
using SGUI.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SGUI.SGameObjects
{
    public class SMessageWindow : SGameObject
    {

        private Queue < (string message, Action action) > messageQueue = new Queue < (string message, Action action) > ();
        private bool closesOnExit = true;

        public SText MessageText {get; private set;}

        private static Sequence textSeq = DOTween.Sequence ();

        public SMessageWindow (
            SGameObject parent,
            string name,
            Queue < (string message, Action action) > messageQueue,
            // bool createsNewCanvas = true,
            bool closesOnExit = true
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                // if (createsNewCanvas) {
                //     var newCanvas = new SCanvas($"{name}Canvas");
                //     return UIFactory.CreateMessageWindow (newCanvas.GameObject, name) as GameObject;
                // }
                // return UIFactory.CreateMessageWindow (parent.GameObject, name) as GameObject;
                return UIFactory.CreatePanel (parent.GameObject, name) as GameObject;
            })
        )
        {
            MessageText = new SText(this, colorType:ColorType.White);
            MessageText.SetTextConfig(36, ColorType.White, null);
            this.closesOnExit = closesOnExit;
            // CanvasStack.GotoNextState (parentCanvas, TransitionType.Overlay);
            SetEvent ();
            SetMessages (messageQueue);
            //ShowNextMessage ();
        }

        private void SetEvent ()
        {
            gameObject.AddComponent<ClickEventGetter> ();
            var trigger = gameObject.AddComponent<EventTrigger> ();
            var entry = new EventTrigger.Entry ();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener (e => ShowNextMessage ());
            trigger.triggers.Add (entry);
        }

        public void SetMessages (Queue < (string message, Action action) > messageQueue)
        {
            this.messageQueue = messageQueue;
            ShowNextMessage ();
        }

        // public void SetTextPos (float posX, float posY)
        // {
        //     var textObject = gameObject.transform.Find ($"{name}Text");
        //     textObject.Find
        // }

        public void AddMessage ((string message, Action action) message)
        {
            messageQueue.Enqueue (message);
        }

        public void ShowNextMessage ()
        {
            if (messageQueue.Count == 0)
            {
                if (closesOnExit)
                {
                    CanvasStack.Pop ();
                }
                else
                {
                    GameObject.Destroy (this.gameObject);
                }
                return;
            }
            var collier = gameObject.AddComponent<BoxCollider2D> ();
            //var text = gameObject.GetComponentInChildren<Text> ();
            var current = messageQueue.Dequeue ();
            SetText (current.message);
            if (current.action != null) current.action.Invoke ();
        }

        public SMessageWindow SetTextConfig (
            int fontSize, ColorType color, string fontName = null)
        {
            this.MessageText.SetTextConfig(fontSize, color, fontName);
            
            // var text = gameObject.GetComponentInChildren<Text> ();
            // text.fontSize = fontSize;
            // text.color = Utils.GetColor (color, 1f);
            return this;
        }

        public SMessageWindow SetText (string _text)
        {
            this.MessageText.SetText(_text);
            // var text = gameObject.GetComponentInChildren<Text> ();
            // text.text = _text;
            return this;
        }

        #region  RequiredMethods

        private void SetColliderSize ()
        {
            var collider = gameObject.GetComponent<BoxCollider2D> ();
            collider.size = new Vector2 (RectSize.x, RectSize.y);
            collider.offset = new Vector2 (RectSize.x / 2, -RectSize.y / 2);
        }

        public new SMessageWindow SetBackGroundColor (ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor (colorType, alpha) as SMessageWindow;
        }

        public new SMessageWindow SetBackGroundColor (Color color)
        {
            return base.SetBackGroundColor (color) as SMessageWindow;
        }

        public new SMessageWindow SetParentSGameObject (SGameObject parent)
        {
            return base.SetParentSGameObject (parent) as SMessageWindow;
        }

        public new SMessageWindow SetRectSizeByRatio (float ratioX, float ratioY)
        {
            base.SetRectSizeByRatio (ratioX, ratioY);
            SetColliderSize ();
            // var collider = gameObject.GetComponent<BoxCollider2D> ();
            // collider.size = new Vector2 (RectSize.x, RectSize.y);
            // collider.offset = new Vector2 (RectSize.x / 2, -RectSize.y / 2);
            return this as SMessageWindow;
        }

        public new SMessageWindow SetRectSize (int width, int height)
        {
            base.SetRectSize (width, height);
            SetColliderSize ();
            return this as SMessageWindow;
        }

        public new SMessageWindow SetLocalPosByRatio (float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio (posXratio, posYratio) as SMessageWindow;
        }

        #endregion

    }
}