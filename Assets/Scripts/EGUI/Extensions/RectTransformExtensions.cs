using System;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace EGUI.Base
{
    public enum AnchorType
    {
        HorizontalStretchWithTopPivot,
        HorizontalStretch,
        HorizontalStretchWithBottomPivot,
        VerticalStretchWithLeftPivot,
        VerticalStretch,
        VerticalStretchWithRightPivot,
        FullStretch,
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
    }
    
    public static class RectTransformExtensions
    { 
        public static void SetRectSizeByRatio(this RectTransform rectTransform,  float ratioX, float ratioY)
        {
            rectTransform.SetSize(rectTransform.GetParentRectSize().x * ratioX,
                rectTransform.GetParentRectSize().y * ratioY);
        }
        
        public static RectTransform SetSize(this RectTransform rectTransform, float width, float height)
        {
            var anchorType = rectTransform.GetAnchorType();
            var pivot = rectTransform.pivot;
            if (anchorType == AnchorType.FullStretch
                || anchorType == AnchorType.HorizontalStretch
                || anchorType == AnchorType.HorizontalStretchWithBottomPivot
                || anchorType == AnchorType.HorizontalStretchWithTopPivot
                || anchorType == AnchorType.VerticalStretch
                || anchorType == AnchorType.VerticalStretchWithLeftPivot
                || anchorType == AnchorType.VerticalStretchWithRightPivot
            )
            {
                rectTransform.SetMiddleCenterAnchor(keepsPosition:true);
                rectTransform.sizeDelta = new Vector2(width, height);
                rectTransform.SetAnchorType(anchorType);
            }
            else
            {
                rectTransform.sizeDelta = new Vector2(width, height);
            }
            rectTransform.pivot = pivot;
            return rectTransform;
        }
        
        public static void SetRelativeAnchoredPos(this RectTransform rectTransform, float widthRatio, float heightRatio)
        {
            var posX = widthRatio * rectTransform.GetParentRectSize().x;
            var posY = -(heightRatio * rectTransform.GetParentRectSize().y);
            rectTransform.SetAnchoredPos(posX, posY);
        }

        public static Vector2 GetApparentRectSize(this RectTransform rectTransform)
        {
            var anchorMax = rectTransform.anchorMax;
            var anchorMin = rectTransform.anchorMin;
            var offsetMax = rectTransform.offsetMax;
            var offsetMin = rectTransform.offsetMin;
            float rectSizeX = 0;
            float rectSizeY = 0;
            RectTransform parent = rectTransform.transform.parent?.gameObject.GetOrAddComponent<RectTransform>();
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

        public static AnchorType GetAnchorType(this RectTransform rectTransform)
        {
            var anchorMax = rectTransform.anchorMax;
            var anchorMin = rectTransform.anchorMin;
            if (anchorMin == new Vector2(0, 0.5f) && anchorMax == new Vector2(1, 0.5f))
            {
                return AnchorType.HorizontalStretch;
            }
            if (anchorMin == new Vector2(0, 1) && anchorMax == new Vector2(1, 1))
            {
                return AnchorType.HorizontalStretchWithTopPivot;
            }
            if (anchorMin == new Vector2(0, 0) && anchorMax == new Vector2(1, 0))
            {
                return AnchorType.HorizontalStretchWithBottomPivot;
            }
            if (anchorMin == new Vector2(0.5f, 0) && anchorMax == new Vector2(0.5f, 1))
            {
                return AnchorType.VerticalStretch;
            }
            if (anchorMin == new Vector2(0, 0) && anchorMax == new Vector2(0, 1))
            {
                return AnchorType.VerticalStretchWithLeftPivot;
            }
            if (anchorMin == new Vector2(1, 0) && anchorMax == new Vector2(1, 1))
            {
                return AnchorType.VerticalStretchWithRightPivot;
            }
            if (anchorMin == new Vector2(0, 0) && anchorMax == new Vector2(1, 1))
            {
                return AnchorType.FullStretch;
            }
            if (anchorMin == new Vector2(0, 1) && anchorMax == new Vector2(0, 1))
            {
                return AnchorType.TopLeft;
            }
            if (anchorMin == new Vector2(0.5f, 1) && anchorMax == new Vector2(0.5f, 1))
            {
                return AnchorType.TopCenter;
            }
            if (anchorMin == new Vector2(1, 1) && anchorMax == new Vector2(1, 1))
            {
                return AnchorType.TopRight;
            }
            if (anchorMin == new Vector2(0, 0.5f) && anchorMax == new Vector2(0, 0.5f))
            {
                return AnchorType.MiddleLeft;
            }
            if (anchorMin == new Vector2(0.5f, 0.5f) && anchorMax == new Vector2(0.5f, 0.5f))
            {
                return AnchorType.MiddleCenter;
            }
            if (anchorMin == new Vector2(1, 0.5f) && anchorMax == new Vector2(1, 0.5f))
            {
                return AnchorType.MiddleRight;
            }
            if (anchorMin == new Vector2(0, 0) && anchorMax == new Vector2(0, 0))
            {
                return AnchorType.BottomLeft;
            }
            if (anchorMin == new Vector2(0.5f, 0) && anchorMax == new Vector2(0.5f, 0))
            {
                return AnchorType.BottomCenter;
            }
            if (anchorMin == new Vector2(1, 0) && anchorMax == new Vector2(1, 0))
            {
                return AnchorType.BottomRight;
            }

            return AnchorType.MiddleCenter;
        }
        
        public static RectTransform SetOffset(this RectTransform rectTransform, float minX, float minY, float maxX,
            float maxY)
        {
            rectTransform.offsetMax = new Vector2(maxX, maxY);
            rectTransform.offsetMin = new Vector2(minX, minY);
            return rectTransform;
        }
        
        public static RectTransform SetPresetRect(this RectTransform rectTransform, RectInfo rectInfo)
        {
            rectTransform.SetPosAndSize(rectInfo.PosX, rectInfo.PosY, rectInfo.Width, rectInfo.Height);
            return rectTransform;
        }
        
        public static RectTransform SetPosAndSize(this RectTransform rectTransform, float posX, float posY, float width, float height)
        {
            rectTransform.SetAnchoredPos(posX, posY);
            rectTransform.SetSize(width, height);
            return rectTransform;
        }
        
        public static void SetPivot(this RectTransform rectTransform, float x, float y)
        {
            var newPivot = new Vector2(x, y);
            rectTransform.pivot = newPivot;
        }

        public static Vector2 GetParentRectSize(this RectTransform rectTransform)
        {
            Transform parent = rectTransform.transform.parent;
            return parent?.gameObject.GetOrAddComponent<RectTransform>().GetApparentRectSize()
                   ?? new Vector2(Screen.width, Screen.height);
        }

        public static RectTransform SetMiddleCenterAnchor(this RectTransform rectTransform, bool keepsPosition = true)
        {
            var rectSize = rectTransform.sizeDelta;
            var parentRectSize = rectTransform.GetParentRectSize();
            var preRectSize = rectSize;
            var preOffsetMax = rectTransform.offsetMax;
            var preOffsetMin = rectTransform.offsetMin;
            var preAnchoredPos = rectTransform.anchoredPosition;
            var anchoredPosX = rectTransform.anchoredPosition.x;
            var anchoredPosY = rectTransform.anchoredPosition.y;
            float newAnchoredPosX = anchoredPosX;
            float newAnchoredPosY = anchoredPosY;

            switch (rectTransform.GetAnchorType())
            {
                case AnchorType.HorizontalStretch:
                {
                    rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                    var rectSizeX = parentRectSize.x - preOffsetMin.x + preOffsetMax.x;
                    rectTransform.sizeDelta = new Vector2(rectSizeX, preRectSize.y);
                    break;
                }
                case AnchorType.HorizontalStretchWithTopPivot:
                {
                    rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                    var rectSizeX = parentRectSize.x - preOffsetMin.x + preOffsetMax.x;
                    rectTransform.sizeDelta = new Vector2(rectSizeX, preRectSize.y);
                    newAnchoredPosX = anchoredPosX;
                    newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
                    break;
                }
                case AnchorType.HorizontalStretchWithBottomPivot:
                {
                    rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                    var rectSizeX = parentRectSize.x - preOffsetMin.x + preOffsetMax.x;
                    rectTransform.sizeDelta = new Vector2(rectSizeX, preRectSize.y);
                    newAnchoredPosX = anchoredPosX;
                    newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
                    break;
                }
                case AnchorType.VerticalStretch:
                {
                    rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                    var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
                    rectTransform.sizeDelta = new Vector2(preRectSize.x, rectSizeY);
                    break;
                }
                case AnchorType.VerticalStretchWithLeftPivot:
                {
                    rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                    var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
                    rectTransform.sizeDelta = new Vector2(preRectSize.x, rectSizeY);
                    newAnchoredPosX = -(parentRectSize.x / 2) + anchoredPosX + rectSize.x / 2;
                    newAnchoredPosY = anchoredPosY;
                    break;
                }
                case AnchorType.VerticalStretchWithRightPivot:
                {
                    rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                    var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
                    rectTransform.sizeDelta = new Vector2(preRectSize.x, rectSizeY);
                    newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                    newAnchoredPosY = anchoredPosY;
                    break;
                }
                case AnchorType.FullStretch:
                {
                    rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                    var rectSizeX = parentRectSize.x + preOffsetMax.x - preOffsetMin.x;
                    var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
                    rectTransform.anchoredPosition = preAnchoredPos;
                    rectTransform.sizeDelta = new Vector2(rectSizeX, rectSizeY);
                    break;
                }
                case AnchorType.TopLeft:
                {
                    newAnchoredPosX = anchoredPosX - (parentRectSize.x / 2) + rectSize.x / 2;
                    newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
                    break;
                }
                case AnchorType.TopCenter:
                {
                    newAnchoredPosX = anchoredPosX;
                    newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
                    break;
                }
                case AnchorType.TopRight:
                {
                    newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                    newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
                    break;
                }
                case AnchorType.MiddleLeft:
                {
                    newAnchoredPosX = anchoredPosX - (parentRectSize.x / 2) + rectSize.x / 2;
                    newAnchoredPosY = anchoredPosY;
                    break;
                }
                case AnchorType.MiddleCenter:
                {
                    break;
                }
                case AnchorType.MiddleRight:
                {
                    newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                    newAnchoredPosY = anchoredPosY;
                    break;
                }
                case AnchorType.BottomLeft:
                {
                    newAnchoredPosX = -(parentRectSize.x / 2) + anchoredPosX + rectSize.x / 2;
                    newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
                    break;
                }
                case AnchorType.BottomCenter:
                {
                    newAnchoredPosX = anchoredPosX;
                    newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
                    break;
                }
                case AnchorType.BottomRight:
                {
                    newAnchoredPosX = (parentRectSize.x / 2) + anchoredPosX - rectSize.x / 2;
                    newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
                    break;
                }
            }
            rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
            if (keepsPosition) rectTransform.anchoredPosition = new Vector2(newAnchoredPosX, newAnchoredPosY);
            return rectTransform;
        }

        public static RectTransform SetTopLeftAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.TopLeft);
        }

        public static RectTransform SetTopCenterAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.TopCenter);
        }

        public static RectTransform SetTopRightAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.TopRight);
        }

        public static RectTransform SetMiddleLeftAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.MiddleLeft);
        }

        public static RectTransform SetMiddleRightAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.MiddleRight);
        }

        public static RectTransform SetBottomLeftAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.BottomLeft);
        }

        public static RectTransform SetBottomCenterAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.BottomCenter);
        }

        public static RectTransform SetBottomRightAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.BottomRight);
        }
        
        public static RectTransform SetHorizontalStretchAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.HorizontalStretch);
        }

        public static RectTransform SetHorizontalStretchWithTopPivotAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.HorizontalStretchWithTopPivot);
        }

        public static RectTransform SetHorizontalStretchWithBottomPivotAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.HorizontalStretchWithBottomPivot);
        }
        
        public static RectTransform SetVerticalStretchAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.VerticalStretch);
        }

        public static RectTransform SetVerticalStretchWithLeftPivotAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.VerticalStretchWithLeftPivot);
        }

        public static RectTransform SetVerticalStretchWithRightPivotAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.VerticalStretchWithRightPivot);
        }

        public static RectTransform SetFullStretchAnchor(this RectTransform rectTransform)
        {
            return rectTransform.SetAnchorType(AnchorType.FullStretch);
        }


        public static RectTransform SetAnchorType(this RectTransform rectTransform, AnchorType anchorType, bool keepsPosition = false)
        {
            rectTransform.SetMiddleCenterAnchor(keepsPosition);
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
                    break;
                }
                case AnchorType.VerticalStretchWithRightPivot:
                {
                    newAnchoredPosX = -(parentRectSize.x / 2) + anchoredPosX + rectSize.x / 2;
                    newAnchoredPosY = anchoredPosY;
                    var merginTop = parentRectSize.y / 2 - rectSize.y / 2 - anchoredPosY;
                    var merginBottom = parentRectSize.y / 2 - rectSize.y / 2 + anchoredPosY;
                    rectTransform.sizeDelta = new Vector2(rectSize.x, -(merginTop + merginBottom));
                    break;
                }
                case AnchorType.VerticalStretch:
                {
                    var merginTop = parentRectSize.y / 2 - rectSize.y / 2 - anchoredPosY;
                    var merginBottom = parentRectSize.y / 2 - rectSize.y / 2 + anchoredPosY;
                    rectTransform.sizeDelta = new Vector2(rectSize.x, -(merginTop + merginBottom));
                    break;
                }
                case AnchorType.HorizontalStretch:
                {
                    var merginLeft = parentRectSize.x / 2 - rectSize.x / 2 + anchoredPosX;
                    var right = parentRectSize.x / 2 - rectSize.x / 2 - anchoredPosX;
                    rectTransform.sizeDelta = new Vector2(-(merginLeft + right), rectSize.y);
                    break;
                }
                case AnchorType.HorizontalStretchWithTopPivot:
                {
                    newAnchoredPosX = anchoredPosX;
                    newAnchoredPosY = -(parentRectSize.y / 2) + anchoredPosY + rectSize.y / 2;
                    var merginLeft = parentRectSize.x / 2 - rectSize.x / 2 + anchoredPosX;
                    var right = parentRectSize.x / 2 - rectSize.x / 2 - anchoredPosX;
                    rectTransform.sizeDelta = new Vector2(-(merginLeft + right), rectSize.y);
                    break;
                }
                case AnchorType.HorizontalStretchWithBottomPivot:
                {
                    newAnchoredPosX = anchoredPosX;
                    newAnchoredPosY = (parentRectSize.y / 2) + anchoredPosY - rectSize.y / 2;
                    var merginLeft = parentRectSize.x / 2 - rectSize.x / 2 + anchoredPosX;
                    var right = parentRectSize.x / 2 - rectSize.x / 2 - anchoredPosX;
                    rectTransform.sizeDelta = new Vector2(-(merginLeft + right), rectSize.y);
                    break;
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
            if (keepsPosition) rectTransform.anchoredPosition = new Vector2(newAnchoredPosX, newAnchoredPosY);
            return rectTransform;
        }
        
        public static void SetAnchoredPos(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.anchoredPosition = new Vector3(x, y, 0);
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