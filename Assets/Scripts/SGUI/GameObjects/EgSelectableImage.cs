using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scripts.Extensions;
using DG.Tweening;
using EGUI.Base;
using UnityEngine.EventSystems;

namespace EGUI.GameObjects
{
    public class EgSelectableImage : EgImage
    {
        public EgSelectableImage(
            EGGameObject parent,
            string imageFilePath = null,
            float posRatioX = 0,
            float posRatioY = 0,
            float ratioX = 1,
            float ratioY = 1,
            bool isRaycastTaraget = true,
            bool keepsAspectRatio = false,
            string name = "SImage"
        ) : base(parent, imageFilePath, posRatioX, posRatioY, ratioX, ratioY, keepsAspectRatio, "SSelectableImage")
        {
            if (keepsAspectRatio)
            {
                var aspectFitter = gameObject.TryAddComponent<AspectRatioFitter>();
                aspectFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            }
        }

        public EgSelectableImage SetOnClick(Action action)
        {
            var trigger = gameObject.TryAddComponent<EventTrigger>();
            trigger.triggers = new List<EventTrigger.Entry>();
            AddOnClick(action);
            return this;
        }

        public EgSelectableImage AddOnClick(Action action)
        {
            var trigger = gameObject.TryAddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener(e => action.Invoke());
            trigger.triggers.Add(entry);
            return this;
        }
    }
}