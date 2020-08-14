using System;
using System.Collections.Generic;
using EGUI.Base;
using EGUI.GameObjects.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static EGUI.Base.Utils;
using UniRx;
using static UnityEngine.UI.ScrollRect;

namespace EGUI.GameObjects
{
    class PreEgVerticalGridScrollView : EGGameObject, ILayoutObject
    {

        public float ItemHeight
        {
            get; private set;
        }

        public int ItemWidth
        {
            get
            {
                return (int)((contentAreaRectSize.x - (gridLayout.padding.left + gridLayout.padding.right)
                    - gridLayout.spacing.x * (columnSize - 1)) / columnSize);
            }
        }
        
        private GridLayoutGroup layoutComponent;

        private bool isItemAutoSizing = false;

        private int columnSize = 1;

        private int minContentAreaSize = 0;
        
        private float scrollbarWidth = 0;
        
        public int PaddingLeft => layoutComponent.padding.left;
        public int PaddingRight => layoutComponent.padding.right;
        public int PaddingTop => layoutComponent.padding.top;
        public int PaddingBottom => layoutComponent.padding.bottom;
        public float SpacingX => layoutComponent.spacing.x;
        public float SpacingY => layoutComponent.spacing.y;

        public GameObject ContentArea { get; private set; }

        public GridLayoutGroup gridLayout;

        private Vector2 contentAreaRectSize;

        public PreEgVerticalGridScrollView(
            EGGameObject parent,
            int itemHeight = 100,
            int columnSize = 2,
            float posRatioX = 0,
            float posRatioY = 0,
            float widthRatio = 1,
            float heightRatio = 1,
            string name = "SVerticalGridScrollView"
        ) : base(parent, 
            posRatioX,
            posRatioY,
            widthRatio,
            heightRatio,
            name,
            () => UIFactory.CreateVerticalGridLayoutScrollView(parent.GameObject, name))
        {
            scrollbarWidth = gameObject.transform.FindDeep("Scrollbar Vertical").GetComponent<RectTransform>().sizeDelta.x;
            ContentArea = this.GameObject.transform.FindDeep("Content");
            ContentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(-scrollbarWidth, RectSize.y);

            var csfitter = ContentArea.AddComponent<ContentSizeFitter>();
            csfitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            csfitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            gridLayout = ContentArea.GetComponent<GridLayoutGroup>();

            ItemHeight = itemHeight;
            this.columnSize = columnSize;

            gridLayout.childAlignment = TextAnchor.UpperLeft;
            SetScrollbarVisibility(ScrollbarVisibility.Permanent);
            SetMovementType(MovementType.Clamped);

            gameObject.ObserveEveryValueChanged(_ => RectSize).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gridLayout.spacing.x).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gridLayout.spacing.y).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gridLayout.padding).Subscribe(_ => UpdateCellSize());
            
            UpdateCellSize();
        }

        private void UpdateCellSize()
        {
            contentAreaRectSize = new Vector2(ApparentRectSize.x - scrollbarWidth, ApparentRectSize.y);
            gridLayout.cellSize = new Vector2(ItemWidth, ItemHeight);
        }

        public void AddItem(EGGameObject egGameObject)
        {
            egGameObject.GameObject.transform.SetParent(ContentArea.transform, false);
        }

        public PreEgVerticalGridScrollView SetItemHeight(float itemHeight)
        {
            ItemHeight = itemHeight;
            return this;
        }

        public PreEgVerticalGridScrollView SetScrollbarVisibility(ScrollbarVisibility scrollbarVisibility)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.verticalScrollbarVisibility = scrollbarVisibility;
            return this;
        }

        public PreEgVerticalGridScrollView SetMovementType(MovementType movementType)
        {
            var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
            scrollView.movementType = movementType;
            return this;
        }

        public PreEgVerticalGridScrollView SetPadding(int? left = null, int? right = null, int? top = null, int? bottom = null)
        {
            layoutComponent.padding.left = left ?? PaddingLeft;
            layoutComponent.padding.right = right ?? PaddingRight;
            layoutComponent.padding.top = top ?? PaddingTop;
            layoutComponent.padding.bottom = bottom ?? PaddingBottom;
            return this;
        }

        public PreEgVerticalGridScrollView SetSpacing(float? spacingX = null, float? spacingY = null)
        {
            layoutComponent.spacing = new Vector2(spacingX ?? SpacingX, spacingY ?? SpacingY);
            return this;
        }

        #region  RequiredMethods

        public new PreEgVerticalGridScrollView SetBackGroundColor(ColorType colorType, float alpha = 1)
        {
            return base.SetColor(colorType, alpha) as PreEgVerticalGridScrollView;
        }

        public new PreEgVerticalGridScrollView SetBackGroundColor(Color color)
        {
            return base.SetColor(color) as PreEgVerticalGridScrollView;
        }

        public new PreEgVerticalGridScrollView SetParentSGameObject(EGGameObject parent)
        {
            return base.SetParentSGameObject(parent) as PreEgVerticalGridScrollView;
        }

        public new PreEgVerticalGridScrollView SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as PreEgVerticalGridScrollView;
        }

        public new PreEgVerticalGridScrollView SetRectSizeByRatio(float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio(ratioX, ratioY) as PreEgVerticalGridScrollView;
        }

        public new PreEgVerticalGridScrollView SetLocalPos(float width, float height)
        {
            return base.SetLocalPos(width, height) as PreEgVerticalGridScrollView;
        }

        public new PreEgVerticalGridScrollView SetLocalPosByRatio(float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio(posXratio, posYratio) as PreEgVerticalGridScrollView;
        }

        public new PreEgVerticalGridScrollView SetAnchorType(Utils.AnchorType anchorType)
        {
            return base.SetAnchorType(anchorType) as PreEgVerticalGridScrollView;
        }

        #endregion
    }
}