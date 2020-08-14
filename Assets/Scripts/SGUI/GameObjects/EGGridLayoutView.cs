using System;
using EGUI.Base;
using EGUI.GameObjects.Interfaces;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using static EGUI.Base.Utils;

namespace EGUI.GameObjects
{
    public class EGGridLayoutView : EGGameObject, ILayoutObject
    {
        public float ItemHeight
        {
            get
            {
                var value = (ApparentRectSize.y - (layoutComponent.padding.top + layoutComponent.padding.bottom)
                    - layoutComponent.spacing.y * (rowCount - 1)) / rowCount;
                return value;
            }
        }

        public float ItemWidth
        {
            get
            {
                if (ConstantItemWidth >  0) return ConstantItemWidth;
                var value = (ApparentRectSize.x - (layoutComponent.padding.left + layoutComponent.padding.right)
                                                - layoutComponent.spacing.x * (columnCount - 1)) / columnCount;
                return value;
            }
        }

        public int ConstantItemWidth { get; private set; } = -1;
        
        public int ConstantItemHeight { get; private set; } = -1;

        public int PaddingLeft => layoutComponent.padding.left;
        public int PaddingRight => layoutComponent.padding.right;
        public int PaddingTop => layoutComponent.padding.top;
        public int PaddingBottom => layoutComponent.padding.bottom;
        public float SpacingX  => layoutComponent.spacing.x;
        public float SpacingY  => layoutComponent.spacing.y;

        private GridLayoutGroup layoutComponent;

        private int columnCount = 1;
        private int rowCount = 1;

        public EGGridLayoutView (
            EGGameObject parent,
            RectTransform.Axis axis ,
            int rowCount = 3,
            int columnCount = 3,
            int constantItemWidth = -1,
            int constantItemHeight = -1,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "SGridLayoutView"
        ) : base
        (
            parent,
            posRatioX,
            posRatioY,
            widthRatio,
            heightRatio,
            name,
            () => UIFactory.CreateGridLayoutView (parent.GameObject, name)
        )
        {
            layoutComponent = gameObject.GetComponent<GridLayoutGroup>();
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            if (constantItemWidth > 0) ConstantItemWidth = constantItemWidth;
            if (constantItemHeight > 0) ConstantItemHeight = constantItemHeight;
            SetValueObserver();
            UpdateCellSize();
        }

        private void SetValueObserver()
        {
            gameObject.ObserveEveryValueChanged(_ => RectSize).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => layoutComponent.spacing).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => layoutComponent.padding.left).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => layoutComponent.padding.right).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => layoutComponent.padding.top).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => layoutComponent.padding.bottom).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => rectTransform.offsetMax).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => rectTransform.offsetMin).Subscribe(_ => UpdateCellSize());
        }

        private void UpdateCellSize()
        {
            layoutComponent.cellSize = new Vector2(ItemWidth, ItemHeight);
        }

        public void AddItem(EGGameObject egGameObject)
        {
            egGameObject.GameObject.transform.SetParent(gameObject.transform, false);
        }

        public EGGridLayoutView SetPadding(int? left = null, int? right = null, int? top = null, int? bottom = null)
        {
            layoutComponent.padding.left = left ?? PaddingLeft;
            layoutComponent.padding.right = right ?? PaddingRight;
            layoutComponent.padding.top = top ?? PaddingTop;
            layoutComponent.padding.bottom = bottom ?? PaddingBottom;
            return this;
        }

        public EGGridLayoutView SetSpacing(float? spacingX = null, float? spacingY = null)
        {
            layoutComponent.spacing = new Vector2(spacingX ?? SpacingX, spacingY ?? SpacingY);
            return this;
        }

    }
}