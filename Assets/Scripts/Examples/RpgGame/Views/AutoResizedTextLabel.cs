using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using EGUI.GameObjects;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.RpgGame.Views
{
    public class AutoResizedTextLabel : EGGameObject
    {
        private RpgText textObject;
        private bool initialized = false;
        

        public AutoResizedTextLabel(EGGameObject parent, string text) : base(parent, "AutoResizedTextLabel")
        {
            SetImage("Images/clear_box");
            var canvasGroup = gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            textObject = new RpgText(this).SetText(text).As<RpgText>();
            var fitter = textObject.gameObject.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            gameObject.ObserveEveryValueChanged(_ => textObject.RectSize.x)
                .Subscribe(_ => Resize());
            Observable.NextFrame().Subscribe(_ =>
            {
                canvasGroup.alpha = 1;
            });
        }

        private void Resize()
        {
            SetSize(textObject.RectSize.x + 10, textObject.RectSize.y + 10);
        }
    }
}