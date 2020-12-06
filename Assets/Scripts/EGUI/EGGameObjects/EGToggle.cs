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

        /// <summary>
        /// Toggleオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="isGrouped">Toggleをグループ化するか</param>
        /// <param name="isWithCheckBoxImage">チェックボックスの画像を表示するか</param>
        public EGToggle
        (
            EGGameObject parent,
            bool isGrouped = false,
            bool isWithCheckBoxImage = true
        ) : this
        (
            parent.gameObject, isGrouped, isWithCheckBoxImage
        )
        {
        }

        /// <summary>
        /// Toggleオブジェクトのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="isGrouped">Toggleをグループ化するか</param>
        /// <param name="isWithCheckBoxImage">チェックボックスの画像を表示するか</param>
        /// <param name="name">オブジェクト名</param>
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
            SetSize(200, 40)
                .SetImage(UGUIDefaultResources.UISprite)
                .SetImageColor(DisabledBackGroundImageColor);
            ToggleComponent = gameObject.AddComponent<Toggle>();

            BoxImageObject = new EGGameObject(gameObject, name: "toggleBackGround")
                .SetImage(UGUIDefaultResources.UISprite)
                .SetImageColor(Color.white);

            LabelTextObject = new EGText(this.gameObject, "toggleLabel")
                .SetText("Toggle")
                .SetParagraph(alignment: TextAnchor.MiddleLeft)
                .SetSize(200, 40) as EGText;

            var checkBoxImageSize = rectTransform.sizeDelta.y - spacing * 2;
            BoxImageObject
                .SetAnchorType(AnchorType.MiddleLeft)
                .SetPosition(spacing, 0)
                .SetSize(checkBoxImageSize, checkBoxImageSize);

            var boxAspectFitter = BoxImageObject.gameObject.GetOrAddComponent<AspectRatioFitter>();
            boxAspectFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            boxAspectFitter.aspectRatio = 1;
            BoxImageObject.SetAnchorType(AnchorType.VerticalStretchWithLeftPivot);

            var textAspectFitter = LabelTextObject.gameObject.GetOrAddComponent<AspectRatioFitter>();
            textAspectFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            textAspectFitter.aspectRatio = 3;
            BoxImageObject.SetAnchorType(AnchorType.VerticalStretchWithLeftPivot);

            CheckImageObject = new EGGameObject(BoxImageObject.gameObject, name: "box_gray_name")
                .SetImage(UGUIDefaultResources.Checkmark)
                .SetRelativeSize(1, 1)
                .SetPosition(0, 0)
                .SetAnchorType(AnchorType.FullStretch);

            BoxImageObject.gameObject.SetActive(isWithCheckBoxImage);

            ToggleComponent.targetGraphic = BoxImageObject.gameObject.GetComponent<Image>();
            ToggleComponent.graphic = CheckImageObject.gameObject.GetComponent<Image>();

            BoxImageObject.SetSize(defaultHeight - offSet * 2, defaultHeight - offSet * 2);
            LabelTextObject.SetSize(defaultWidth * 2 - defaultHeight, defaultHeight - spacing);

            var layout = gameObject.AddComponent<HorizontalLayoutGroup>();
            layout.childControlWidth = false;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = true;
            layout.spacing = spacing;
            layout.padding = new RectOffset(offSet, offSet, offSet, offSet);

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
                LabelTextObject.SetTextColor(EnabledTextColor);
                gameObject.SetImageColor(EnabledBackGroundImageColor);
            }
            else
            {
                LabelTextObject.SetTextColor(DisabledTextColor);
                gameObject.SetImageColor(DisabledBackGroundImageColor);
            }
        }
    }
}