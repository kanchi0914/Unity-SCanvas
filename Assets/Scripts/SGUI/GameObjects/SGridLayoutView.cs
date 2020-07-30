using System;
using SGUI.Base;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

using SGUI.GameObjects.Interfaces;
using static SGUI.Base.Utils;

namespace SGUI.GameObjects
{
    public class SGridLayoutView : SGameObject, ILayoutObject
    {
        public float ItemHeight
        {
            get
            {
                var value = ((ApparentRectSize.y - (gridLayout.padding.top + gridLayout.padding.bottom)
                    - gridLayout.spacing.y * (rowCount - 1)) / rowCount);
                return value;
                
            }
        }

        public float ItemWidth
        {
            get
            {
                return ((ApparentRectSize.x - (gridLayout.padding.left + gridLayout.padding.right)
                    - gridLayout.spacing.x * (columnCount - 1)) / columnCount);
            }
        }

        private GridLayoutGroup gridLayout;

        private int columnCount = 1;
        private int rowCount = 1;


        public SGridLayoutView (
            SGameObject parent,
            int rowCount,
            int columnCount
        ) : this (parent, rowCount, columnCount, 0, 0, 1, 1) { }

        public SGridLayoutView (
            SGameObject parent,
            int rowCount,
            int columnCount,
            float posRatioX,
            float posRatioY,
            float ratioX,
            float ratioY
        ) : base (parent, "SGridLayoutView",
            new Func<GameObject> (() =>
            {
                return UIFactory.CreateGridLayoutView (parent.GameObject, "SVerticalListItems");
            })
        )
        {
            SetRectSizeByRatio (ratioX, ratioY);
            SetLocalPosByRatio (posRatioX, posRatioY);
            gridLayout = gameObject.GetComponent<GridLayoutGroup>();
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            SetValueObserver();
            UpdateCellSize();
        }

        private void SetValueObserver()
        {
            gameObject.ObserveEveryValueChanged(_ => RectSize).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gridLayout.spacing).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gridLayout.padding.left).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gridLayout.padding.right).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gridLayout.padding.top).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gridLayout.padding.bottom).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => rectTransform.offsetMax).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => rectTransform.offsetMin).Subscribe(_ => UpdateCellSize());
        }

        private void UpdateCellSize()
        {
            gridLayout.cellSize = new Vector2(ItemWidth, ItemHeight);
        }

        public void AddItem(SGameObject sGameObject)
        {
            sGameObject.GameObject.transform.SetParent(gameObject.transform, false);
        }

        #region SpedificMethods

        public SGridLayoutView SetPadding(int left, int right, int top, int bottom)
        {
            gridLayout.padding.left = left;
            gridLayout.padding.right = right;
            gridLayout.padding.top = top;
            gridLayout.padding.bottom = bottom;
            return this;
        }

        public SGridLayoutView SetSpacing(float spacingX, float spacingY)
        {
            gridLayout.spacing = new Vector2(spacingX, spacingY);
            return this;
        }

        #endregion


        #region  RequiredMethods

        public new SGridLayoutView SetBackGroundColor (ColorType colorType, float alpha)
        {
            return base.SetBackGroundColor (colorType, alpha) as SGridLayoutView;
        }

        public new SGridLayoutView SetBackGroundColor (Color color)
        {
            return base.SetBackGroundColor (color) as SGridLayoutView;
        }

        public new SGridLayoutView SetParentSGameObject (SGameObject parent)
        {
            return base.SetParentSGameObject (parent) as SGridLayoutView;
        }

        public new SGridLayoutView SetRectSize(float width, float height)
        {
            return base.SetRectSize(width, height) as SGridLayoutView;
        }

        public new SGridLayoutView SetRectSizeByRatio (float ratioX, float ratioY)
        {
            return base.SetRectSizeByRatio (ratioX, ratioY) as SGridLayoutView;
        }

        public new SGridLayoutView SetLocalPos(float width, float height)
        {
            return base.SetLocalPos(width, height) as SGridLayoutView;
        }

        public new SGridLayoutView SetLocalPosByRatio (float posXratio, float posYratio)
        {
            return base.SetLocalPosByRatio (posXratio, posYratio) as SGridLayoutView;
        }

        public new SGridLayoutView SetAnchorType(AnchorType anchorType)
        {
            return base.SetAnchorType(anchorType) as SGridLayoutView;
        }

        #endregion

    }
}