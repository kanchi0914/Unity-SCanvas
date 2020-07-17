using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SDOTextMessageWindow : SGameObject
    {
        private Queue < (string message, Action action) > messageQueue = new Queue < (string message, Action action) > ();

        private static Sequence textSeq = DOTween.Sequence ();
        public SDOTextMessageWindow (
            SGameObject parent,
            string name
        ) : base (parent, name,
            new Func<GameObject> (() =>
            {
                //return UIFactory.CreateMessageWindow (parent.GameObject, name);
                return UIFactory.CreatePanel (parent.GameObject, name);
            })
        )
        {
            gameObject.AddComponent<ClickEventGetter> ();
            var trigger = gameObject.AddComponent<EventTrigger> ();
            var entry = new EventTrigger.Entry ();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener (e => ShowNextMessage ());
            trigger.triggers.Add (entry);
        }

        // public SDOTextMessageWindow (SGameObject parent, string name)
        // {
        //     InitGameObject (parent, name);
        //     gameObject.AddComponent<ClickEventGetter> ();
        //     var trigger = gameObject.AddComponent<EventTrigger> ();
        //     var entry = new EventTrigger.Entry ();
        //     entry.eventID = EventTriggerType.PointerClick;
        //     entry.callback.AddListener (e => ShowNextMessage ());
        //     trigger.triggers.Add (entry);
        //     SetParentSGameObject (parent);
        // }

        public void SetMessages (Queue < (string message, Action action) > messageQueue)
        {
            this.messageQueue = messageQueue;
        }

        public void ShowNextMessage ()
        {
            if (messageQueue.Count == 0)
            {
                CanvasStack.Pop ();
                return;
            }
            var text = gameObject.GetComponentInChildren<Text> ();
            Debug.Log (textSeq);
            if (textSeq.IsPlaying ())
            {
                textSeq.Complete ();
            }
            else
            {
                text.text = "";
                textSeq = DOTween.Sequence ();
                var current = messageQueue.Dequeue ();
                textSeq.Append (text.DOText (
                    current.message,
                    current.message.Length * 0.02f
                ).SetEase (Ease.Linear));
            }
        }

        public SDOTextMessageWindow SetTextConfig (
            int fontSize, ColorType color, FontData font = null)
        {
            var text = gameObject.GetComponentInChildren<Text> ();
            text.fontSize = fontSize;
            text.color = Utils.GetColor (color, 1f);
            return this;
        }

        public SDOTextMessageWindow SetText (string _text)
        {
            var text = gameObject.GetComponentInChildren<Text> ();
            text.text = _text;
            return this;
        }

        public new SDOTextMessageWindow SetBackGroundColor (ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor (colorType, alpha) as SDOTextMessageWindow;
        }

        public new SDOTextMessageWindow SetBackGroundColor (Color color)
        {
            return base.SetBackGroundColor (color) as SDOTextMessageWindow;
        }

        public new SDOTextMessageWindow SetParentSGameObject (SGameObject parent)
        {
            return base.SetParentSGameObject (parent) as SDOTextMessageWindow;
        }

        public new SDOTextMessageWindow SetRectSizeByRatio (float ratioX, float ratioY)
        {
            base.SetRectSizeByRatio (ratioX, ratioY);
            var collider = gameObject.GetComponent<BoxCollider2D> ();
            collider.size = new Vector2 (RectSize.x, RectSize.y);
            collider.offset = new Vector2 (RectSize.x / 2, -RectSize.y / 2);
            return this as SDOTextMessageWindow;
        }

        public new SDOTextMessageWindow SetLocalPosByRatio (float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio (posXratio, posYratio) as SDOTextMessageWindow;
        }

    }
}