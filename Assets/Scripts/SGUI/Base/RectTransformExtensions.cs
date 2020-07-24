using Assets.Scripts.Extensions;
using UnityEngine;
using static SGUI.Base.Utils;

namespace SGUI.Base
{
    public static class RectTransformExtensions
    {
        public static RectTransform SetOffset(this RectTransform rectTransform, float minX, float minY, float maxX, float maxY)
        {
            rectTransform.offsetMax = new Vector2(maxX, maxY);
            rectTransform.offsetMin = new Vector2(minX, minY);
            return rectTransform;
        }

        public static RectTransform SetTopLeftAnchor(this RectTransform rectTransform)
        {
            rectTransform.SetMiddleCenterAnchor();
            var rectSize = rectTransform.sizeDelta;
            var parentRectSize = rectTransform.GetParentRectSize();
            var merginCenterX = rectTransform.anchoredPosition.x;
            var merginCenterY = rectTransform.anchoredPosition.y;
            var anchoredPosX = (parentRectSize.x / 2) + merginCenterX - rectSize.x / 2;
            var anchoredPosY = -(parentRectSize.y / 2) + merginCenterY + rectSize.y / 2;
            rectTransform.SetAnchorAndPivot(AnchorType.TopLeft);
            rectTransform.anchoredPosition = new Vector2(anchoredPosX, anchoredPosY);
            return rectTransform;
        }

        public static RectTransform SetMiddleCenterAnchor(this RectTransform rectTransform)
        {
            var pivot = rectTransform.pivot;

            var rectSize = rectTransform.sizeDelta;
            var parentRectSize = rectTransform.GetParentRectSize();

            var anchorMax = rectTransform.anchorMax;
            var anchorMin = rectTransform.anchorMin;

            var preRectSize = rectSize;
            var preOffsetMax = rectTransform.offsetMax;
            var preOffsetMin = rectTransform.offsetMin;
            var preAnchoredPos = rectTransform.anchoredPosition;

            if (anchorMin == new Vector2(0.5f, 0.5f) && anchorMax == new Vector2(0.5f, 0.5f))
                return rectTransform;

            // Horizontal Stretch
            if (anchorMin == new Vector2(0, 0.5f) && anchorMax == new Vector2(1, 0.5f))
            {
                rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                var rectSizeX = parentRectSize.x - preOffsetMin.x + preOffsetMax.x;
                rectTransform.sizeDelta = new Vector2(rectSizeX, preRectSize.y);
            }
            // Vertical Stretch
            else if (anchorMin == new Vector2(0.5f, 0) && anchorMax == new Vector2(0.5f, 1))
            {
                rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
                rectTransform.sizeDelta = new Vector2(preRectSize.x, rectSizeY);
            }
            // Full Stretch
            else if (anchorMin == new Vector2(0, 0) && anchorMax == new Vector2(1, 1))
            {
                rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                var rectSizeX = parentRectSize.x + preOffsetMax.x - preOffsetMin.x;
                var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
                rectTransform.anchoredPosition = preAnchoredPos;
                rectTransform.sizeDelta = new Vector2(rectSizeX, rectSizeY);
            }
            // Top Left
            else if (anchorMin == new Vector2(0, 1) && anchorMax == new Vector2(0, 1))
            {
                var merginLeft = rectTransform.anchoredPosition.x;
                var merginTop = -rectTransform.anchoredPosition.y;
                var anchoredPosX = merginLeft - (parentRectSize.x / 2) + rectSize.x / 2;
                var anchoredPosY = (parentRectSize.y / 2) - merginTop - rectSize.y / 2;
                rectTransform.SetAnchorAndPivot(AnchorType.MiddleCenter);
                rectTransform.anchoredPosition = new Vector2(anchoredPosX, anchoredPosY);
            }
            return rectTransform;
        }

        private static Vector2 GetParentRectSize(this RectTransform rectTransform)
        {
            Transform parent = rectTransform.transform.parent;
            Vector2 parentRectSize;
            if (parent != null)
            {
                var rect = parent.gameObject.TryAddComponent<RectTransform>();
                parentRectSize = rect.sizeDelta;
            }
            else
            {
                parentRectSize = new Vector2(Screen.width, Screen.height);
            }
            return parentRectSize;
        }

        public static RectTransform SetHorizontalStretchAnchor(this RectTransform rectTransform)
        {
            rectTransform.SetMiddleCenterAnchor();
            var rectSize = rectTransform.sizeDelta;
            var anchoredPos = rectTransform.anchoredPosition;
            var parentRectSize = rectTransform.GetParentRectSize();

            var merginLeft = parentRectSize.x / 2 - rectSize.x / 2 +
                rectTransform.anchoredPosition.x;
            var right = parentRectSize.x / 2 - rectSize.x / 2 -
                rectTransform.anchoredPosition.x;

            rectTransform.SetAnchorAndPivot(AnchorType.HorizontalStretch);
            rectTransform.offsetMax = new Vector2(-right, rectSize.y / 2);
            rectTransform.offsetMin = new Vector2(merginLeft, -rectSize.y / 2);
            rectTransform.anchoredPosition = new Vector2(anchoredPos.x, anchoredPos.y);
            return rectTransform;
        }

        public static RectTransform SetVerticalStretchAnchor(this RectTransform rectTransform)
        {
            rectTransform.SetMiddleCenterAnchor();
            var rectSize = rectTransform.sizeDelta;
            var anchoredPos = rectTransform.anchoredPosition;
            var parentRectSize = rectTransform.GetParentRectSize();

            var merginTop = parentRectSize.y / 2 - rectSize.y / 2 -
                rectTransform.anchoredPosition.y;
            var merginBottom = parentRectSize.y / 2 - rectSize.y / 2 +
                rectTransform.anchoredPosition.y;

            rectTransform.SetAnchorAndPivot(AnchorType.VerticalStretch);
            rectTransform.offsetMax = new Vector2(rectSize.x / 2, -merginTop);
            rectTransform.offsetMin = new Vector2(-rectSize.x / 2, merginBottom);
            rectTransform.anchoredPosition = new Vector2(anchoredPos.x, anchoredPos.y);
            return rectTransform;
        }

        public static RectTransform SetStretchLeftAnchor(this RectTransform rectTransform)
        {
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);
            return rectTransform;
        }

        public static RectTransform SetMiddleLeftAnchor(this RectTransform rectTransform)
        {
            rectTransform.anchorMin = new Vector2(0f, 0.5f);
            rectTransform.anchorMax = new Vector2(0f, 0.5f);
            rectTransform.pivot = new Vector2(0f, 0.5f);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);
            return rectTransform;
        }

        public static RectTransform SetFullStretchAnchor(this RectTransform rectTransform)
        {
            rectTransform.SetMiddleCenterAnchor();
            var rectSize = rectTransform.sizeDelta;
            var preAnchoredPos = rectTransform.anchoredPosition;
            var parentRectSize = rectTransform.GetParentRectSize();

            var merginLeft = rectTransform.anchoredPosition.x;
            var merginRight = parentRectSize.x - rectSize.x - merginLeft;
            var merginTop = -rectTransform.anchoredPosition.y;
            var merginBottom = parentRectSize.y - rectSize.y - merginTop;
            rectTransform.SetAnchorAndPivot(AnchorType.FullStretch);
            rectTransform.offsetMax = new Vector2(-merginRight, -merginTop);
            rectTransform.offsetMin = new Vector2(merginLeft, merginBottom);
            rectTransform.anchoredPosition = new Vector2(preAnchoredPos.x, preAnchoredPos.y);
            return rectTransform;
        }

        private static void SetAnchorAndPivot(this RectTransform rectTransform, AnchorType anchorType)
        {
            switch (anchorType)
            {
                case AnchorType.TopLeft:
                    {
                        rectTransform.anchorMin = new Vector2(0f, 1f);
                        rectTransform.anchorMax = new Vector2(0f, 1f);
                        rectTransform.pivot = new Vector3(0.0f, 1.0f);
                        break;
                    }
                case AnchorType.FullStretch:
                    {
                        rectTransform.anchorMin = new Vector2(0, 0);
                        rectTransform.anchorMax = new Vector2(1, 1);
                        rectTransform.pivot = new Vector3(0.5f, 0.5f);
                        break;
                    }
                case AnchorType.MiddleCenter:
                    {
                        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                        rectTransform.pivot = new Vector3(0.5f, 0.5f);
                        break;
                    }
                case AnchorType.HorizontalStretch:
                    {
                        rectTransform.anchorMin = new Vector2(0.0f, 0.5f);
                        rectTransform.anchorMax = new Vector2(1f, 0.5f);
                        rectTransform.pivot = new Vector2(0.5f, 0.5f);
                        break;
                    }
                case AnchorType.VerticalStretch:
                    {
                        rectTransform.anchorMin = new Vector2(0.5f, 0f);
                        rectTransform.anchorMax = new Vector2(0.5f, 1f);
                        rectTransform.pivot = new Vector2(0.5f, 0.5f);
                        break;
                    }
            }

        }
    }
}