using System;
using Assets.Scripts.SGUI.Base;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Assets.Scripts.Extensions;
using EGUI.Base;

namespace EGUI.GameObjects
{
    public class EGToggle : EGGameObject
    {
        private int defaultWidth = 200;
        private int defaultHeight = 40;
        private int offSet = 5;
        private int spacing = 10;
        
        /// <summary>
        /// Toggleコンポーネントへの参照
        /// </summary>
        public Toggle ToggleComponent { get; private set; }

        public EGGameObject BoxImageObject { get; private set; }

        public EGGameObject CheckImageObject { get; private set; }
        
        public EGText LabelTextObject { get; private set; }

        public Color EnabledTextColor { get; set; } = Color.black;
        public Color DisabledTextColor { get; set; } = Color.black;
        
        public Color EnabledBackGroundImageColor { get; set; } = Color.clear;
        
        public Color DisabledBackGroundImageColor { get; set; } = Color.clear;
        
        public bool IsOn
        {
            get => ToggleComponent.isOn;
            set => ToggleComponent.isOn = value;
        }
        
        public EGToggle(
            GameObject parent = null, 
            bool isGrouped = false,
            bool isWithCheckBoxImage = true,
            string name = "EgToggle"
        ) : base(
            parent,
            name
        )
        {
            SetRectSize(200, 40)
                .SetImageSprite(UGUIResources.UISprite)
                .SetImageColor(DisabledBackGroundImageColor);
            ToggleComponent = gameObject.AddComponent<Toggle>();
            
            BoxImageObject = new EGGameObject(gameObject, name: "toggleBackGround")
                .SetImageSprite(UGUIResources.UISprite)
                .SetImageColor(Color.white);

            LabelTextObject = new EGText(this.gameObject, "Toggle", "toggleLabel")
                .SetParagraph(alignment: TextAnchor.MiddleLeft)
                .SetRectSize(200, 40) as EGText;
            
            var checkBoxImageSize = rectTransform.sizeDelta.y - spacing * 2;
            BoxImageObject
                .SetMiddleLeftAnchor()
                .SetLocalPos(spacing, 0)
                .SetRectSize(checkBoxImageSize, checkBoxImageSize);
            
            var boxAspectFitter = BoxImageObject.gameObject.GetOrAddComponent<AspectRatioFitter>();
            boxAspectFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            boxAspectFitter.aspectRatio = 1; 
            BoxImageObject.SetVerticalStretchWithLeftPivotAnchor();
            
            var textAspectFitter = LabelTextObject.gameObject.GetOrAddComponent<AspectRatioFitter>();
            textAspectFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            textAspectFitter.aspectRatio = 3; 
            BoxImageObject.SetVerticalStretchWithLeftPivotAnchor();

            CheckImageObject = new EGGameObject(BoxImageObject.gameObject, name: "box_gray_name")
                .SetImageSprite(UGUIResources.Checkmark)
                .SetRectSizeByRatio(1, 1)
                .SetLocalPos(0, 0)
                .SetFullStretchAnchor();

            BoxImageObject.gameObject.SetActive(isWithCheckBoxImage);

            ToggleComponent.targetGraphic = BoxImageObject.gameObject.GetComponent<Image>();
            ToggleComponent.graphic = CheckImageObject.gameObject.GetComponent<Image>();

            BoxImageObject.SetRectSize(defaultHeight - offSet * 2, defaultHeight - offSet * 2);
            LabelTextObject.SetRectSize(defaultWidth * 2 - defaultHeight, defaultHeight - spacing);
            
            var layout = gameObject.AddComponent<HorizontalLayoutGroup>();
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;
            layout.spacing = spacing;
            layout.padding = new RectOffset(offSet,offSet,offSet,offSet);

            if (isGrouped && parent != null)
            {
                var group = parent.gameObject.GetOrAddComponent<ToggleGroup>();
                group.allowSwitchOff = false;
                ToggleComponent.group = group;
            }

            // this.isWithCheckBox = isWithCheckBox;
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
                LabelTextObject.SetColor(EnabledTextColor);
                gameObject.SetImageColor(EnabledBackGroundImageColor);
            }
            else
            {
                LabelTextObject.SetColor(DisabledTextColor);
                gameObject.SetImageColor(DisabledBackGroundImageColor);
            }
        }


    }

}