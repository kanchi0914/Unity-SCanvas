using Assets.Scripts.Extensions;
using EGUI.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EGUI.GameObjects
{
    public class EGGridLayoutView : EGGameObject
    {
        private int columnCount = 1;
        private int rowCount = 1;

        /// <summary>
        /// 行数から動的に計算されるアイテムの高さ
        /// </summary>
        public float ItemHeight
        {
            get
            {
                if (ConstantItemHeight > 0) return ConstantItemHeight;
                var value = (gameObject.GetRectTransform().GetApparentRectSize().y
                             - (LayoutComponent.padding.top + LayoutComponent.padding.bottom)
                             - LayoutComponent.spacing.y * (rowCount - 1)) / rowCount;
                return value;
            }
        }

        /// <summary>
        /// 列数から動的に決定されるアイテムの幅
        /// </summary>
        public float ItemWidth
        {
            get
            {
                if (ConstantItemWidth > 0) return ConstantItemWidth;
                var value = (gameObject.GetRectTransform().GetApparentRectSize().x
                             - (LayoutComponent.padding.left + LayoutComponent.padding.right)
                             - LayoutComponent.spacing.x * (columnCount - 1)) / columnCount;
                return value;
            }
        }

        /// <summary>
        /// アイテムの固定高さ
        /// </summary>
        public int ConstantItemWidth { get; private set; } = -1;

        /// <summary>
        /// アイテムの固定幅
        /// </summary>
        public int ConstantItemHeight { get; private set; } = -1;

        /// <summary>
        /// GridLayoutGroupコンポーネントへの参照
        /// </summary>
        public GridLayoutGroup LayoutComponent;
        
        /// <summary>
        /// GridLayoutGroupを持つGameObjectのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクトをラップするEGGameObject</param>
        /// <param name="rowCount">行数</param>
        /// <param name="columnCount">列数</param>
        /// <param name="constantItemWidth">アイテムの固定幅</param>
        /// <param name="constantItemHeight">アイテムの固定高さ</param>
        public EGGridLayoutView
        (
            EGGameObject parent,
            int rowCount = 3,
            int columnCount = 3,
            int constantItemWidth = -1,
            int constantItemHeight = -1
        ) : this
        (
            parent.gameObject,
            rowCount,columnCount,constantItemWidth,constantItemHeight
        ){}
        
        /// <summary>
        /// GridLayoutGroupを持つGameObjectのラッパークラス
        /// </summary>
        /// <param name="parent">親オブジェクト</param>
        /// <param name="rowCount">行数</param>
        /// <param name="columnCount">列数</param>
        /// <param name="constantItemWidth">アイテムの固定幅</param>
        /// <param name="constantItemHeight">アイテムの固定高さ</param>
        /// <param name="name">オブジェクト名</param>
        public EGGridLayoutView(
            GameObject parent,
            int rowCount = 3,
            int columnCount = 3,
            int constantItemWidth = -1,
            int constantItemHeight = -1,
            string name = "SGridLayoutView"
        ) : base
        (
            parent,
            name
        )
        {
            LayoutComponent = gameObject.GetOrAddComponent<GridLayoutGroup>();
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            if (constantItemWidth > 0) ConstantItemWidth = constantItemWidth;
            if (constantItemHeight > 0) ConstantItemHeight = constantItemHeight;
            SetValueObserver();
            UpdateCellSize();
        }

        private void SetValueObserver()
        {
            gameObject.ObserveEveryValueChanged(_ => gameObject.GetRectTransform().sizeDelta)
                .Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => LayoutComponent.spacing).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => LayoutComponent.padding.left).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => LayoutComponent.padding.right).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => LayoutComponent.padding.top).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => LayoutComponent.padding.bottom).Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gameObject.GetRectTransform().offsetMax)
                .Subscribe(_ => UpdateCellSize());
            gameObject.ObserveEveryValueChanged(_ => gameObject.GetRectTransform().offsetMin)
                .Subscribe(_ => UpdateCellSize());
        }

        private EGGridLayoutView UpdateCellSize()
        {
            LayoutComponent.cellSize = new Vector2(ItemWidth, ItemHeight);
            return this;
        }
        
        /// <summary>
        /// 子オブジェクトの配置方法を設定する
        /// </summary>
        /// <param name="childAlignment"></param>
        /// <returns></returns>
        public EGGridLayoutView SetChildAlignment(
            TextAnchor childAlignment
        )
        {
            LayoutComponent.childAlignment = childAlignment;
            return this;
        }

        /// <summary>
        /// GridLayoutGroupコンポーネントのpadding, spacingを設定する
        /// </summary>
        /// <param name="left">padding.left</param>
        /// <param name="right">padding.right</param>
        /// <param name="top">padding.top</param>
        /// <param name="bottom">padding.bottom</param>
        /// <param name="spacingX">spacing.x</param>
        /// <param name="spacingY">spacing.y</param>
        public EGGridLayoutView SetPaddingAndSpacing(int? left = null, int? right = null, int? top = null, int? bottom = null,
            float? spacingX = null, float? spacingY = null)
        {
            LayoutComponent.padding.left = left ?? LayoutComponent.padding.left;
            LayoutComponent.padding.right = right ?? LayoutComponent.padding.right;
            LayoutComponent.padding.top = top ?? LayoutComponent.padding.top;
            LayoutComponent.padding.bottom = bottom ?? LayoutComponent.padding.bottom;
            LayoutComponent.spacing = new Vector2(spacingX ?? LayoutComponent.spacing.x,
                spacingY ?? LayoutComponent.spacing.y);
            return this;
        }

        /// <summary>
        /// GridLayoutGroupコンポーネントのpadding, spacingにまとめて同じ値を設定する
        /// </summary>
        /// <param name="left">設定する値</param>
        public EGGridLayoutView SetPaddingAndSpacing(int num)
        {
            SetPaddingAndSpacing(num, num, num, num, num, num);
            return this;
        }
    }
}