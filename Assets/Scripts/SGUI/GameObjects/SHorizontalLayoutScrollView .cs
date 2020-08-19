// using System;
// using System.Collections.Generic;
// using Assets.Scripts.Extensions;
// using EGUI.Base;
// using EGUI.GameObjects.Interfaces;
// using UnityEngine;
// using UnityEngine.UI;
// using static EGUI.Base.Utils;
// using static UnityEngine.UI.ScrollRect;
//
// namespace EGUI.GameObjects
// {
//     class PreEGHorizontalLayoutScrollView : EGGameObject, ILayoutObject
//     {
//         private int constantItemWidth = 0;
//         private readonly HorizontalLayoutGroup layoutComponent;
//
//         public int PaddingLeft => layoutComponent.padding.left;
//         public int PaddingRight => layoutComponent.padding.right;
//         public int PaddingTop => layoutComponent.padding.top;
//         public int PaddingBottom => layoutComponent.padding.bottom;
//         public float Spacing  => layoutComponent.spacing;
//         
//         public GameObject ContentArea { get; private set; }
//
//         public PreEGHorizontalLayoutScrollView(
//             EGGameObject parent,
//             int constantItemWidth = 0,
//             bool isAutoSizingHeight = true,
//             string name = "SHorizontalLayoutScrollView"
//         ) : base
//         (
//             parent,
//             name,
//             () => UIFactory.CreateHorizontalScrollView(parent.GameObject, name)
//         )
//         {
//             ContentArea = this.GameObject.transform.FindDeep("Content");
//             layoutComponent = ContentArea.GetComponent<HorizontalLayoutGroup>();
//
//             layoutComponent.childControlWidth = (constantItemWidth > 0);
//             layoutComponent.childControlHeight = isAutoSizingHeight;
//             layoutComponent.childScaleHeight = false;
//             layoutComponent.childScaleWidth = false;
//             layoutComponent.childForceExpandHeight = isAutoSizingHeight;
//             layoutComponent.childForceExpandWidth = false;
//
//             var scrollbarSize = (int)gameObject.transform.FindDeep("Scrollbar Horizontal")
//                 .GetComponent<RectTransform>().sizeDelta.y;
//             ContentArea = GameObject.transform.FindDeep("Content");
//             var contentAreaRect = ContentArea.GetComponent<RectTransform>();
//             contentAreaRect.sizeDelta = new Vector2(0, RectSize.y - scrollbarSize);
//
//             var csfitter = ContentArea.AddComponent<ContentSizeFitter>();
//             csfitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
//             csfitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
//             
//             this.constantItemWidth = constantItemWidth;
//         }
//         
//         
//
//         public void AddItem(EGGameObject egGameObject)
//         {
//             egGameObject.GameObject.transform.SetParent(ContentArea.transform, false);
//             var layout = egGameObject.GameObject.TryAddComponent<LayoutElement>();
//             if (constantItemWidth > 0)
//             {
//                 layout.minWidth = constantItemWidth;
//             }
//             else
//             {
//                 layout.minWidth = egGameObject.RectSize.x;
//             }
//             //SetLayout();
//         }
//         
//         public PreEGHorizontalLayoutScrollView SetPadding(int? left = null, int? right = null, int? top = null, int? bottom = null)
//         {
//             layoutComponent.padding.left = left ?? PaddingLeft;
//             layoutComponent.padding.right = right ?? PaddingRight;
//             layoutComponent.padding.top = top ?? PaddingTop;
//             layoutComponent.padding.bottom = bottom ?? PaddingBottom;
//             SetPadding();
//             return this;
//         }
//
//         public PreEGHorizontalLayoutScrollView SetSpacing(float spacing)
//         {
//             layoutComponent.spacing = spacing;
//             return this;
//         }
//
//         public PreEGHorizontalLayoutScrollView SetScrollbarVisibility(ScrollbarVisibility scrollbarVisibility)
//         {
//             var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
//             scrollView.horizontalScrollbarVisibility = scrollbarVisibility;
//             return this;
//         }
//
//         public PreEGHorizontalLayoutScrollView SetMovementType(MovementType movementType)
//         {
//             var scrollView = gameObject.GetComponentInChildren<ScrollRect>();
//             scrollView.movementType = movementType;
//             return this;
//         }
//     }
// }