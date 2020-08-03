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
    public class EgSelectableImage : EGGameObject
    {
        public Image Image { get; private set; }

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
        ) : base(parent, "SImage",
            new Func<GameObject>(() =>
            {
                return UIFactory.CreateImage(parent.GameObject, "SImage", imageFilePath);
            })
        )
        {
            SetRectSizeByRatio(ratioX, ratioY);
            SetLocalPosByRatio(posRatioX, posRatioY);
            Image = gameObject.GetComponent<Image>();
            if (keepsAspectRatio)
            {
                var aspectFitter = gameObject.TryAddComponent<AspectRatioFitter>();
                aspectFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            }

        }

        public void SetImageSource(Sprite source)
        {
            this.Image.sprite = source;
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



        #region  RequiredMethods

        public new EgSelectableImage SetBackGroundColor(ColorType colorType, float alpha = 1)
        {
            return base.SetBackGroundColor(colorType, alpha) as EgSelectableImage;
        }

        public new EgSelectableImage SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as EgSelectableImage;
        }

        public new EgSelectableImage SetParentSGameObject(EGGameObject parent)
        {
            return base.SetParentSGameObject(parent) as EgSelectableImage;
        }

        public new EgSelectableImage SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as EgSelectableImage;
        }

        public new EgSelectableImage SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as EgSelectableImage;
        }

        public new EgSelectableImage SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as EgSelectableImage;
        }

        #endregion
    }
}