using System;
using Assets.Scripts.SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Assets.Scripts.Extensions;
using EGUI.Base;

namespace EGUI.GameObjects
{
    public class EgToggle : EGGameObject
    {
        public Toggle ToggleComponent { get; private set; }

        public EGGameObject BoxImageObject { get; private set; }

        public EGGameObject CheckImageObject { get; private set; }
        
        public EGText Text { get; private set; }

        public Color EnabledTextColor { get; set; } = Color.black;
        public Color DisabledTextColor { get; set; } = Color.black;
        
        public Color EnabledBackGroundImageColor { get; set; } = Color.clear;
        
        public Color DisabledBackGroundImageColor { get; set; } = Color.clear;

        private bool isWithCheckBox;

        public EgToggle(
            GameObject parent,
            bool isGrouped = false,
            bool isWithCheckBox = true,
            string name = "EgToggle"
        ) : base(
            parent,
            name
        )
        {
            gameObject.SetRectSize(200, 40);
            ToggleComponent = gameObject.AddComponent<Toggle>();
            gameObject.SetImageSprite(UGUIResources.UISprite);
            gameObject.SetImageColor(DisabledBackGroundImageColor);
            
            BoxImageObject = new EGGameObject(gameObject, name: "toggleBackGround");
            BoxImageObject.gameObject
                .SetImageSprite(UGUIResources.UISprite)
                .SetImageColor(Color.white);
            
            Text = new EGText(this.gameObject, "Toggle", "toggleLabel");
            Text.gameObject.SetMiddleLeftAnchor();
            
            var spacing = 10;
            var checkBoxImageSize = rectTransform.sizeDelta.y - spacing * 2;

            BoxImageObject.gameObject
                .SetMiddleLeftAnchor()
                .SetLocalPos(spacing, 0)
                .SetRectSize(checkBoxImageSize, checkBoxImageSize);
            
            var boxAspectFitter = BoxImageObject.gameObject.GetOrAddComponent<AspectRatioFitter>();
            boxAspectFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            boxAspectFitter.aspectRatio = 1; 
            BoxImageObject.gameObject.SetVerticalStretchWithLeftPivotAnchor();

            CheckImageObject = new EGGameObject(BoxImageObject.gameObject, name: "box_gray_name");
            CheckImageObject.gameObject
                .SetImageSprite(UGUIResources.Checkmark)
                .SetRectSizeByRatio(1, 1)
                .SetLocalPos(0, 0)
                .SetFullStretchAnchor();

            if (isWithCheckBox)
            {
                Text.gameObject.SetLocalPos(checkBoxImageSize + spacing * 2, 0);
                float textWidth = (200 - (spacing * 3 + checkBoxImageSize));
                Text.gameObject.SetRectSize(textWidth, rectTransform.sizeDelta.y);
            }
            else
            {
                Text.gameObject.SetLocalPos( spacing, 0);
                float textWidth = (200 - (spacing * 2));
                Text.gameObject.SetRectSize(textWidth, rectTransform.sizeDelta.y);
            }
            Text.gameObject.SetFullStretchAnchor();
            
            BoxImageObject.gameObject.SetActive(isWithCheckBox);

            ToggleComponent.targetGraphic = BoxImageObject.gameObject.GetComponent<Image>();
            ToggleComponent.graphic = CheckImageObject.gameObject.GetComponent<Image>();

            if (isGrouped && parent != null)
            {
                var group = parent.gameObject.GetOrAddComponent<ToggleGroup>();
                group.allowSwitchOff = false;
                ToggleComponent.group = group;
            }

            this.isWithCheckBox = isWithCheckBox;
            ToggleComponent.onValueChanged.AddListener(e => OnSelected());
        }
        
        public void SetOnValueChanged(Action action, bool isOn = true)
        {
            ToggleComponent.onValueChanged.RemoveAllListeners();
            AddOnValueChanged(action, isOn);
        }

        public void AddOnValueChanged(Action action, bool isOn = true)
        {
            ToggleComponent.onValueChanged.AddListener(e =>
            {
                if (isOn)
                {
                    if (ToggleComponent.isOn) action.Invoke();
                }
                else
                {
                    if (!ToggleComponent.isOn) action.Invoke();
                }
            });
        }
        
        private void OnSelected()
        {
            if (ToggleComponent.isOn)
            {
                Text.SetTextColor(EnabledTextColor);
                gameObject.SetImageColor(EnabledBackGroundImageColor);
            }
            else
            {
                Text.SetTextColor(DisabledTextColor);
                gameObject.SetImageColor(DisabledBackGroundImageColor);
            }
        }

    }

}