using System;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Examples.RpgGame.Views
{
       public class CommandTextLabel : EGGameObject
        {
            private EGText textLabel;
            private EGGameObject arrow;

            public CommandTextLabel(EGGameObject parent, string text, Action onSelected, Action onPointerIn = null,
                Action onPointerExit = null) : base(parent)
            {
                arrow = new EGGameObject(this)
                    .SetAnchorType(AnchorType.MiddleLeft)
                    .SetRelativeSize(.2f, .2f);
                arrow.SetImage("Images/arrow");
                var fitter = arrow.gameObject.GetOrAddComponent<AspectRatioFitter>();
                fitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
                fitter.aspectRatio = 1;
                textLabel = new RpgText(this)
                    .SetParagraph(TextAnchor.MiddleLeft)
                    .SetAnchorType(AnchorType.MiddleRight)
                    .SetRelativeSize(.8f, 1) as EGText;

                AddEvent(EventTriggerType.PointerEnter, () =>
                {
                    arrow.SetActive(true);
                    onPointerIn?.Invoke();
                });
                AddEvent(EventTriggerType.PointerExit, () =>
                {
                    arrow.SetActive(false);
                    onPointerExit?.Invoke();
                });
                AddOnClick(onSelected);

                textLabel.SetText(text);
                arrow.SetActive(false);
            }

            public void Enable()
            {
                var trigger = gameObject.GetComponent<EventTrigger>();
                textLabel.SetTextColor(Color.white);
                arrow.SetImageColor(Color.white);
                trigger.enabled = true;
            }

            public void Disable()
            {
                var trigger = gameObject.GetComponent<EventTrigger>();
                textLabel.SetTextColor(Color.gray);
                arrow.SetImageColor(Color.gray);
                trigger.enabled = false;
            }
        }

}