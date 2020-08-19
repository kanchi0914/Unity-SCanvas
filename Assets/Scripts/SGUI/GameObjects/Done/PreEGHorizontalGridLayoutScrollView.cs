// using System;
// using System.Collections.Generic;
// using EGUI.Base;
// using EGUI.GameObjects.Interfaces;
// using UniRx;
// using UnityEngine;
// using UnityEngine.UI;
// using static UnityEngine.UI.ScrollRect;
//
// namespace EGUI.GameObjects
// {
//     class PreEGHorizontalGridLayoutScrollView : EGGameObject, ILayoutObject
//     {
//         public int ItemHeight => (int) ((contentAreaRectSize.y - SpacingX * (rowSize + 1)) / rowSize);
//         public int ItemWidth => (int) ((contentAreaRectSize.x - SpacingY * (columnSize + 1)) / columnSize);
//
//         private bool isItemAutoSizing = false;
//
//         private int rowSize = 3;
//         private int columnSize = 3;
//
//         public int PaddingLeft => layoutComponent.padding.left;
//         public int PaddingRight => layoutComponent.padding.right;
//         public int PaddingTop => layoutComponent.padding.top;
//         public int PaddingBottom => layoutComponent.padding.bottom;
//         public float SpacingX => layoutComponent.spacing.x;
//         public float SpacingY => layoutComponent.spacing.y;
//
//         private int scrollbarHeight = 0;
//
//         private GridLayoutGroup layoutComponent;
//
//         public GameObject ContentArea { get; private set; }
//
//         private Vector2 contentAreaRectSize;
//
//         public PreEGHorizontalGridLayoutScrollView(
//             EGGameObject parent,
//             int rowSize = 3,
//             int columnSize = 3,
//             float posRatioX = 0,
//             float posRatioY = 0,
//             float widthRatio = 1,
//             float heightRatio = 1,
//             string name = "EGHorizontalGridLayoutScrollView"
//         ) : base
//         (
//             parent,
//             posRatioX,
//             posRatioY,
//             widthRatio,
//             heightRatio,
//             name,
//             () => UIFactory.CreateHorizontalGridLayoutScrollView(parent.GameObject, name)
//         )
//         {
//             this.rowSize = rowSize;
//             this.columnSize = columnSize;
//             this.isItemAutoSizing = true;
//             
//             scrollbarHeight = (int) gameObject.transform.FindDeep("Scrollbar Horizontal").GetComponent<RectTransform>()
//                 .sizeDelta.y;
//             var parentWidth = RectSize.x;
//             ContentArea = this.GameObject.transform.FindDeep("Content");
//             var rect = ContentArea.GetComponent<RectTransform>();
//
//             contentAreaRectSize = new Vector2(RectSize.x, RectSize.y - scrollbarHeight);
//
//             var csfitter = ContentArea.AddComponent<ContentSizeFitter>();
//             csfitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
//             csfitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
//
//             layoutComponent = gameObject.GetComponentInChildren<GridLayoutGroup>();
//             layoutComponent.constraint = GridLayoutGroup.Constraint.FixedRowCount;
//             layoutComponent.constraintCount = rowSize;
//
//             SetValueObserver();
//             UpdateCellSize();
//         }
//         
//         private void SetValueObserver()
//         {
//             gameObject.ObserveEveryValueChanged(_ => RectSize).Subscribe(_ => UpdateCellSize());
//             gameObject.ObserveEveryValueChanged(_ => layoutComponent.spacing).Subscribe(_ => UpdateCellSize());
//             gameObject.ObserveEveryValueChanged(_ => layoutComponent.padding.left).Subscribe(_ => UpdateCellSize());
//             gameObject.ObserveEveryValueChanged(_ => layoutComponent.padding.right).Subscribe(_ => UpdateCellSize());
//             gameObject.ObserveEveryValueChanged(_ => layoutComponent.padding.top).Subscribe(_ => UpdateCellSize());
//             gameObject.ObserveEveryValueChanged(_ => layoutComponent.padding.bottom).Subscribe(_ => UpdateCellSize());
//             gameObject.ObserveEveryValueChanged(_ => rectTransform.offsetMax).Subscribe(_ => UpdateCellSize());
//             gameObject.ObserveEveryValueChanged(_ => rectTransform.offsetMin).Subscribe(_ => UpdateCellSize());
//         }
//
//         
//         public void SetScrollbarVisibility(ScrollbarVisibility scrollbarVisibility)
//         {
//             var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
//             scrollView.horizontalScrollbarVisibility = scrollbarVisibility;
//         }
//
//         public void SetMovementType(MovementType movementType)
//         {
//             var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
//             scrollView.movementType = movementType;
//         }
//
//         public void UpdateCellSize()
//         {
//             layoutComponent.cellSize = new Vector2(ItemWidth, ItemHeight);
//         }
//
//         public void AddItem(EGGameObject egGameObject)
//         {
//             egGameObject.GameObject.transform.SetParent(ContentArea.transform, false);
//         }
//
//         public PreEGHorizontalGridLayoutScrollView SetPadding(int? left = null, int? right = null, int? top = null, int? bottom = null)
//         {
//             layoutComponent.padding.left = left ?? PaddingLeft;
//             layoutComponent.padding.right = right ?? PaddingRight;
//             layoutComponent.padding.top = top ?? PaddingTop;
//             layoutComponent.padding.bottom = bottom ?? PaddingBottom;
//             return this;
//         }
//
//         public PreEGHorizontalGridLayoutScrollView SetSpacing(float? spacingX = null, float? spacingY = null)
//         {
//             layoutComponent.spacing = new Vector2(spacingX ?? SpacingX, spacingY ?? SpacingY);
//             return this;
//         }
//     }
// }