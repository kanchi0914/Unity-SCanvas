using System;
using Assets.Scripts.Extensions;
using UnityEngine;
using static EGUI.Base.Utils;

namespace EGUI.Base
{
    public static class RectTransformExtensions
    {
        
        public static Vector2 GetApparentRectSize(this RectTransform rectTransform)
        {
            var anchorMax = rectTransform.anchorMax;
            var anchorMin = rectTransform.anchorMin;
            var offsetMax = rectTransform.offsetMax;
            var offsetMin = rectTransform.offsetMin;
            float rectSizeX = 0;
            float rectSizeY = 0;
            RectTransform parent = rectTransform.transform.parent?.gameObject.TryAddComponent<RectTransform>();
            if ((anchorMax - anchorMin).y != 0)
            {
                if (parent == null) rectSizeY = Screen.height;
                else rectSizeY = parent.GetApparentRectSize().y - offsetMin.y + offsetMax.y;
            }
            else
            {
                rectSizeY = rectTransform.sizeDelta.y;
            }
            if ((anchorMax - anchorMin).x != 0)
            {
                if (parent == null) rectSizeX = Screen.width;
                else rectSizeX = parent.GetApparentRectSize().x - offsetMin.x + offsetMax.x;
            }
            else
            {
                rectSizeX = rectTransform.sizeDelta.x;
            }
            return new Vector2(rectSizeX, rectSizeY);
        }
        

        public static RectTransform SetOffset(this RectTransform rectTransform, float minX, float minY, float maxX, float maxY)
        {
            rectTransform.offsetMax = new Vector2(maxX, maxY);
            rectTransform.offsetMin = new Vector2(minX, minY);
            return rectTransform;
        }
        
        private static Vector2 GetParentRectSize(this RectTransform rectTransform)
        {
            Transform parent = rectTransform.transform.parent;
            return parent?.gameObject.TryAddComponent<RectTransform>().GetApparentRectSize()
                   ?? new Vector2(Screen.width, Screen.height);
            if (parent != null)
            {
                var rect = parent.gameObject.TryAddComponent<RectTransform>();
                return rect.GetApparentRectSize();
            }
            return new Vector2(Screen.width, Screen.height);
        }


        public static RectTransform SetMiddleCenterAnchor(this RectTransform rectTransform)
        {
            var rectSize = rectTransform.sizeDelta;
            var parentRectSize = rectTransform.GetParentRectSize();

            var anchorMax = rectTransform.anchorMax;
            var anchorMin = rectTransform.anchorMin;

            var preRectSize = rectSize;
            var preOffsetMax = rectTransform.offsetMax;
            var preOffsetMin = rectTransform.offsetMin;
            var preAnchoredPos = rectTransform.anchoredPosition;
            
            var anchoredPosX = rectTransform.anchoredPosition.x;
            var anchoredPosY = rectTransform.anchoredPosition.y;
            float newAnchoredPosX = anchoredPosX;
            float newAnchoredPosY = anchoredPosY;

            if (anchorMin == new Vector2(0.5f, 0.5f) && anchorMax == new Vector2(0.5f, 0.5f))
                return rectTransform;

            
            // anchoredPosX, anchoredPosYだけそのままで、ほかは反転させる
            // Horizontal Stretch
            if (anchorMin == new Vector2(0, 0.5f) && anchorMax == new Vector2(1, 0.5f))
            {
                rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                var rectSizeX = parentRectSize.x - preOffsetMin.x + preOffsetMax.x;
                rectTransform.sizeDelta = new Vector2(rectSizeX, preRectSize.y);
            }
            // Horizontal Stretch with Top Pivot
            else if (anchorMin == new Vector2(0, 1) && anchorMax == new Vector2(1, 1))
            {
                rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                var rectSizeX = parentRectSize.x - preOffsetMin.x + preOffsetMax.x;
                rectTransform.sizeDelta = new Vector2(rectSizeX, preRectSize.y);
                newAnchoredPosX = anchoredPosX;
                newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
            }
            // Horizontal Stretch with Bottom Pivot
            else if (anchorMin == new Vector2(0, 0) && anchorMax == new Vector2(1, 0))
            {
                rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                var rectSizeX = parentRectSize.x - preOffsetMin.x + preOffsetMax.x;
                rectTransform.sizeDelta = new Vector2(rectSizeX, preRectSize.y);
                newAnchoredPosX = anchoredPosX;
                newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
            }
            // Vertical Stretch
            else if (anchorMin == new Vector2(0.5f, 0) && anchorMax == new Vector2(0.5f, 1))
            {
                rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
                rectTransform.sizeDelta = new Vector2(preRectSize.x, rectSizeY);
            }
            // Vertical Stretch with Left Pivot
            else if (anchorMin == new Vector2(0, 0) && anchorMax == new Vector2(0, 1))
            {
                rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
                rectTransform.sizeDelta = new Vector2(preRectSize.x, rectSizeY);
                newAnchoredPosX = -(parentRectSize.x / 2) + anchoredPosX + rectSize.x / 2;
                newAnchoredPosY = anchoredPosY;
            }
            // Vertical Stretch with Right Pivot
            else if (anchorMin == new Vector2(1, 0) && anchorMax == new Vector2(1, 1))
            {
                rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
                rectTransform.sizeDelta = new Vector2(preRectSize.x, rectSizeY);
                newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                newAnchoredPosY = anchoredPosY;
            }
            // Full Stretch
            else if (anchorMin == new Vector2(0, 0) && anchorMax == new Vector2(1, 1))
            {
                // rectTransform.SetAnchorAndPivot(Utils.AnchorType.MiddleCenter);
                // var rectSizeX = parentRectSize.x + preOffsetMax.x - preOffsetMin.x;
                // var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
                // rectTransform.anchoredPosition = preAnchoredPos;
                // rectTransform.sizeDelta = new Vector2(rectSizeX, rectSizeY);
                rectTransform.SetAnchorAndPivot(Utils.AnchorType.MiddleCenter);
                var rectSizeX = parentRectSize.x + preOffsetMax.x - preOffsetMin.x;
                var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
                rectTransform.anchoredPosition = preAnchoredPos;
                rectTransform.sizeDelta = new Vector2(rectSizeX, rectSizeY);
            }
            // Top Left
            else if (anchorMin == new Vector2(0, 1) && anchorMax == new Vector2(0, 1))
            {
                newAnchoredPosX = anchoredPosX - (parentRectSize.x / 2) + rectSize.x / 2;
                newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
            }
            // Top Center
            else if (anchorMin == new Vector2(0.5f, 1) && anchorMax == new Vector2(0.5f, 1))
            {
                newAnchoredPosX = anchoredPosX;
                newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
            }
            // Top Right
            else if (anchorMin == new Vector2(1, 1) && anchorMax == new Vector2(1, 1))
            {
                newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
            }
            // Middle Left
            else if (anchorMin == new Vector2(0, 0.5f) && anchorMax == new Vector2(0, 0.5f))
            {
                newAnchoredPosX = anchoredPosX - (parentRectSize.x / 2) + rectSize.x / 2;
                newAnchoredPosY = anchoredPosY;
            }
            else if (anchorMin == new Vector2(0.5f, 0.5f) && anchorMax == new Vector2(0.5f, 0.5f))
            {
                
            }
            // Middle Right
            else if (anchorMin == new Vector2(1, 0.5f) && anchorMax == new Vector2(1, 0.5f))
            {
                newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                newAnchoredPosY = anchoredPosY;
            }
            // Bottom Left
            else if (anchorMin == new Vector2(0, 0) && anchorMax == new Vector2(0, 0))
            {
                newAnchoredPosX = -(parentRectSize.x / 2) + anchoredPosX + rectSize.x / 2;
                newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
            }
            // Bottom Center
            else if (anchorMin == new Vector2(0.5f, 0) && anchorMax == new Vector2(0.5f, 0))
            {
                newAnchoredPosX = anchoredPosX;
                newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
            }
            // Bottom Right
            else if (anchorMin == new Vector2(1, 0) && anchorMax == new Vector2(1, 0))
            {
                newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
            }
            rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
            rectTransform.anchoredPosition = new Vector2(newAnchoredPosX, newAnchoredPosY);
            return rectTransform;
        }
        
        public static RectTransform SetTopLeftAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.TopLeft);
        }
        
        public static RectTransform SetTopCenterAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.TopCenter);
        }
        
        public static RectTransform SetTopRightAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.TopRight);
        }
        
        public static RectTransform SetMiddleLeftAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.MiddleLeft);
        }
        
        public static RectTransform SetMiddleRightAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.MiddleRight);
        }
        
        public static RectTransform SetBottomLeftAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.BottomLeft);
        }
                
        public static RectTransform SetBottomCenterAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.BottomCenter);
        }
                
        public static RectTransform SetBottomRightAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.BottomRight);
        }
        
        
        private static RectTransform SetNewAnchor(this RectTransform rectTransform, AnchorType anchorType)
        {
            rectTransform.SetMiddleCenterAnchor();
            var rectSize = rectTransform.sizeDelta;
            var parentRectSize = rectTransform.GetParentRectSize();
            var anchoredPosX = rectTransform.anchoredPosition.x;
            var anchoredPosY = rectTransform.anchoredPosition.y;
            float newAnchoredPosX = anchoredPosX;
            float newAnchoredPosY = anchoredPosY;
            switch (anchorType)
            {
                case AnchorType.TopLeft:
                {
                    newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                    newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
                    break;
                }
                case AnchorType.TopCenter:
                {
                    newAnchoredPosX = anchoredPosX;
                    newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
                    break;
                }
                case AnchorType.TopRight:
                {
                    newAnchoredPosX = -(parentRectSize.x / 2) + anchoredPosX + rectSize.x / 2;
                    newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
                    break;
                }
                case AnchorType.MiddleLeft:
                {
                    newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                    newAnchoredPosY = anchoredPosY;
                    break;
                }
                case AnchorType.MiddleRight:
                {
                    newAnchoredPosX = -(parentRectSize.x / 2) + anchoredPosX + rectSize.x / 2;
                    newAnchoredPosY = anchoredPosY;
                    break;
                }
                case AnchorType.BottomLeft:
                {
                    newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                    newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
                    break;
                }
                case AnchorType.BottomCenter:
                {
                    newAnchoredPosX = anchoredPosX;
                    newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
                    break;
                }
                case AnchorType.BottomRight:
                {
                    newAnchoredPosX = -(parentRectSize.x / 2) + anchoredPosX + rectSize.x / 2;
                    newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
                    break;
                }
                case AnchorType.VerticalStretchWithLeftPivot:
                {
                    newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                    newAnchoredPosY = anchoredPosY;
                    var merginTop = parentRectSize.y / 2 - rectSize.y / 2 - anchoredPosY;
                    var merginBottom = parentRectSize.y / 2 - rectSize.y / 2 + anchoredPosY;
                    rectTransform.sizeDelta = new Vector2(rectSize.x, -(merginTop + merginBottom));
                    break;;
                }
                case AnchorType.VerticalStretchWithRightPivot:
                {
                    newAnchoredPosX = -(parentRectSize.x / 2) + anchoredPosX + rectSize.x / 2;
                    newAnchoredPosY = anchoredPosY;
                    var merginTop = parentRectSize.y / 2 - rectSize.y / 2 - anchoredPosY;
                    var merginBottom = parentRectSize.y / 2 - rectSize.y / 2 + anchoredPosY;
                    rectTransform.sizeDelta = new Vector2(rectSize.x, -(merginTop + merginBottom));
                    break;;
                }
                case AnchorType.VerticalStretch:
                {
                    var merginTop = parentRectSize.y / 2 - rectSize.y / 2 - anchoredPosY;
                    var merginBottom = parentRectSize.y / 2 - rectSize.y / 2 + anchoredPosY;
                    rectTransform.sizeDelta = new Vector2(rectSize.x, -(merginTop + merginBottom));
                    break;;
                }
                case AnchorType.HorizontalStretch:
                {
                    var merginLeft = parentRectSize.x / 2 - rectSize.x / 2 + anchoredPosX;
                    var right = parentRectSize.x / 2 - rectSize.x / 2 - anchoredPosX;
                    rectTransform.sizeDelta = new Vector2(-(merginLeft + right), rectSize.y);
                    break;;
                }
                case AnchorType.HorizontalStretchWithTopPivot:
                {
                    newAnchoredPosX = anchoredPosX;
                    newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
                    var merginLeft = parentRectSize.x / 2 - rectSize.x / 2 + anchoredPosX;
                    var right = parentRectSize.x / 2 - rectSize.x / 2 - anchoredPosX;
                    rectTransform.sizeDelta = new Vector2(-(merginLeft + right), rectSize.y);
                    break;;
                }
                case AnchorType.HorizontalStretchWithBottomPivot:
                {
                    newAnchoredPosX = anchoredPosX;
                    newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
                    var merginLeft = parentRectSize.x / 2 - rectSize.x / 2 + anchoredPosX;
                    var right = parentRectSize.x / 2 - rectSize.x / 2 - anchoredPosX;
                    rectTransform.sizeDelta = new Vector2(-(merginLeft + right), rectSize.y);
                    break;;
                }
                case AnchorType.FullStretch:
                {
                    var marginLeft = anchoredPosX;
                    var marginRight = parentRectSize.x - rectSize.x - marginLeft;
                    var marginTop = -anchoredPosY;
                    var marginBottom = parentRectSize.y - rectSize.y - marginTop;
                    rectTransform.offsetMax = new Vector2(-marginRight, -marginTop);
                    rectTransform.offsetMin = new Vector2(marginLeft, marginBottom);
                    break;
                }
            }
            rectTransform.SetAnchorAndPivot(anchorType);
            rectTransform.anchoredPosition = new Vector2(newAnchoredPosX, newAnchoredPosY);
            return rectTransform;
        }

        # region HorizontalStretch

        public static RectTransform SetHorizontalStretchAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.HorizontalStretch);
        }
        
        public static RectTransform SetHorizontalStretchWithTopPivotAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.HorizontalStretchWithTopPivot);
        }
        
        public static RectTransform SetHorizontalStretchWithBottomPivotAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.HorizontalStretchWithBottomPivot);
        }

        # endregion
        
        # region VerticalStretch

        public static RectTransform SetVerticalStretchAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.VerticalStretch);
        }
        
        public static RectTransform SetVerticalStretchWithLeftPivotAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.VerticalStretchWithLeftPivot);
        }
        
        public static RectTransform SetVerticalStretchWithRightPivotAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.VerticalStretchWithRightPivot); }

        # endregion

        public static RectTransform SetFullStretchAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetNewAnchor(AnchorType.FullStretch);
        }

        private static RectTransform SetAnchorAndPivot(this RectTransform rectTransform, AnchorType anchorType)
        {
            switch (anchorType)
            {
                case AnchorType.MiddleCenter:
                {
                    rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    rectTransform.pivot = new Vector3(0.5f, 0.5f);
                    break;
                }
                case AnchorType.TopLeft:
                    {
                        rectTransform.anchorMin = new Vector2(0, 1);
                        rectTransform.anchorMax = new Vector2(0, 1);
                        rectTransform.pivot = new Vector3(0, 1);
                        break;
                    }
                case AnchorType.TopCenter:
                {
                    rectTransform.anchorMin = new Vector2(0.5f, 1);
                    rectTransform.anchorMax = new Vector2(0.5f, 1);
                    rectTransform.pivot = new Vector3(0.5f, 1);
                    break;
                }
                case AnchorType.TopRight:
                {
                    rectTransform.anchorMin = new Vector2(1, 1);
                    rectTransform.anchorMax = new Vector2(1, 1);
                    rectTransform.pivot = new Vector3(1f, 1);
                    break;
                }
                case AnchorType.MiddleLeft:
                {
                    rectTransform.anchorMin = new Vector2(0, 0.5f);
                    rectTransform.anchorMax = new Vector2(0, 0.5f);
                    rectTransform.pivot = new Vector3(0, 0.5f);
                    break;
                }
                case AnchorType.MiddleRight:
                {
                    rectTransform.anchorMin = new Vector2(1, 0.5f);
                    rectTransform.anchorMax = new Vector2(1, 0.5f);
                    rectTransform.pivot = new Vector3(1, 0.5f);
                    break;
                }
                case AnchorType.BottomLeft:
                {
                    rectTransform.anchorMin = new Vector2(0, 0);
                    rectTransform.anchorMax = new Vector2(0, 0);
                    rectTransform.pivot = new Vector3(0, 0);
                    break;
                }
                case AnchorType.BottomCenter:
                {
                    rectTransform.anchorMin = new Vector2(0.5f, 0);
                    rectTransform.anchorMax = new Vector2(0.5f, 0);
                    rectTransform.pivot = new Vector3(0.5f, 0);
                    break;
                }
                case AnchorType.BottomRight:
                {
                    rectTransform.anchorMin = new Vector2(1, 0);
                    rectTransform.anchorMax = new Vector2(1, 0);
                    rectTransform.pivot = new Vector3(1, 0);
                    break;
                }
                case AnchorType.FullStretch:
                    {
                        rectTransform.anchorMin = new Vector2(0, 0);
                        rectTransform.anchorMax = new Vector2(1, 1);
                        rectTransform.pivot = new Vector3(0.5f, 0.5f);
                        break;
                    }
                case AnchorType.HorizontalStretch:
                    {
                        rectTransform.anchorMin = new Vector2(0, 0.5f);
                        rectTransform.anchorMax = new Vector2(1, 0.5f);
                        rectTransform.pivot = new Vector2(0.5f, 0.5f);
                        break;
                    }
                case AnchorType.HorizontalStretchWithTopPivot:
                {
                    rectTransform.anchorMin = new Vector2(0, 1);
                    rectTransform.anchorMax = new Vector2(1f, 1);
                    rectTransform.pivot = new Vector2(0.5f, 1f);
                    break;
                }
                case AnchorType.HorizontalStretchWithBottomPivot:
                {
                    rectTransform.anchorMin = new Vector2(0, 0);
                    rectTransform.anchorMax = new Vector2(1f, 0);
                    rectTransform.pivot = new Vector2(0.5f, 0f);
                    break;
                }
                case AnchorType.VerticalStretch:
                    {
                        rectTransform.anchorMin = new Vector2(0.5f, 0f);
                        rectTransform.anchorMax = new Vector2(0.5f, 1f);
                        rectTransform.pivot = new Vector2(0.5f, 0.5f);
                        break;
                    }
                case AnchorType.VerticalStretchWithLeftPivot:
                {
                    rectTransform.anchorMin = new Vector2(0.0f, 0f);
                    rectTransform.anchorMax = new Vector2(0.0f, 1f);
                    rectTransform.pivot = new Vector2(0f, 0.5f);
                    break;
                }
                case AnchorType.VerticalStretchWithRightPivot:
                {
                    rectTransform.anchorMin = new Vector2(1.0f, 0f);
                    rectTransform.anchorMax = new Vector2(1.0f, 1f);
                    rectTransform.pivot = new Vector2(1f, 0.5f);
                    break;
                }
                default: throw new Exception("Not to be Implemented!");
            }
            return rectTransform;
        }
    }
}