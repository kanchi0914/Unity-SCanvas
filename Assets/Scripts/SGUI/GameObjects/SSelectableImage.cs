using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scripts.Extensions;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace SGUI.GameObjects
{
    public class SSelectableImage : SGameObject
    {
        public Image Image { get; private set; }

        public SSelectableImage(
            SGameObject parent,
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

        public SSelectableImage SetOnClick(Action action)
        {
            var trigger = gameObject.TryAddComponent<EventTrigger>();
            trigger.triggers = new List<EventTrigger.Entry>();
            AddOnClick(action);
            return this;
        }

        public SSelectableImage AddOnClick(Action action)
        {
            var trigger = gameObject.TryAddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener(e => action.Invoke());
            trigger.triggers.Add(entry);
            return this;
        }



        #region  RequiredMethods

        public new SSelectableImage SetBackGroundColor(ColorType colorType, float alpha = 1)
        {
            return base.SetBackGroundColor(colorType, alpha) as SSelectableImage;
        }

        public new SSelectableImage SetBackGroundColor(Color color)
        {
            return base.SetBackGroundColor(color) as SSelectableImage;
        }

        public new SSelectableImage SetParentSGameObject(SGameObject parent)
        {
            return base.SetParentSGameObject(parent) as SSelectableImage;
        }

        public new SSelectableImage SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as SSelectableImage;
        }

        public new SSelectableImage SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as SSelectableImage;
        }

        public new SSelectableImage SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as SSelectableImage;
        }

        #endregion
    }
}