public static class RectTransformExtensions
{
    public static GameObject SetOffset (this RectTransform rectTransform, float minX, float minY, float maxX, float maxY)
    {
        rectTransform.offsetMax = new Vector2 (maxX, maxY);
        rectTransform.offsetMin = new Vector2 (minX, minY);
        return rectTransform;
    }

    public SGameObject SetTopLeftAnchor ()
    {
        SetMiddleCenterAnchor ();
        var parentRectSize = GetParentRectSize ();
        var merginCenterX = rectTransform.anchoredPosition.x;
        var merginCenterY = rectTransform.anchoredPosition.y;
        var anchoredPosX = (parentRectSize.x / 2) + merginCenterX - RectSize.x / 2;
        var anchoredPosY = -(parentRectSize.y / 2) + merginCenterY + RectSize.y / 2;
        SetAnchorAndPivot (AnchorType.TopLeft);
        rectTransform.anchoredPosition = new Vector2 (anchoredPosX, anchoredPosY);
        anchorType = AnchorType.TopLeft;
        return this;
    }

    public SGameObject SetMiddleCenterAnchor ()
    {
        var pivot = rectTransform.pivot;

        var parentRectSize = GetParentRectSize ();

        var anchorMax = rectTransform.anchorMax;
        var anchorMin = rectTransform.anchorMin;

        var preRectSize = RectSize;
        var preOffsetMax = rectTransform.offsetMax;
        var preOffsetMin = rectTransform.offsetMin;
        var preAnchoredPos = rectTransform.anchoredPosition;

        if (anchorType == AnchorType.MiddleCenter) return this;

        if (anchorType == AnchorType.HorizontalStretch)
        {
            SetAnchorAndPivot (AnchorType.MiddleCenter);
            var rectSizeX = parentRectSize.x - preOffsetMin.x + preOffsetMax.x;
            rectTransform.sizeDelta = new Vector2 (rectSizeX, preRectSize.y);
        }
        else if (anchorType == AnchorType.VerticalStretch)
        {
            SetAnchorAndPivot (AnchorType.MiddleCenter);
            var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
            rectTransform.sizeDelta = new Vector2 (preRectSize.x, rectSizeY);
        }
        else if (anchorType == AnchorType.FullStretch)
        {
            SetAnchorAndPivot (AnchorType.MiddleCenter);
            var rectSizeX = parentRectSize.x + preOffsetMax.x - preOffsetMin.x;
            var rectSizeY = parentRectSize.y - preOffsetMin.y + preOffsetMax.y;
            rectTransform.anchoredPosition = preAnchoredPos;
            rectTransform.sizeDelta = new Vector2 (rectSizeX, rectSizeY);
        }
        else if (anchorType == AnchorType.TopLeft)
        {
            var merginLeft = rectTransform.anchoredPosition.x;
            var merginTop = -rectTransform.anchoredPosition.y;
            var anchoredPosX = merginLeft - (parentRectSize.x / 2) + RectSize.x / 2;
            var anchoredPosY = (parentRectSize.y / 2) - merginTop - RectSize.y / 2;
            SetAnchorAndPivot (AnchorType.MiddleCenter);
            rectTransform.anchoredPosition = new Vector2 (anchoredPosX, anchoredPosY);
        }
        anchorType = AnchorType.MiddleCenter;
        return this;
    }

    private Vector2 GetParentRectSize ()
    {
        GameObject parent = parentSGameObject.GameObject;
        Vector2 parentRectSize;
        if (parent != null)
        {
            parentRectSize = parentSGameObject.RectSize;
        }
        else
        {
            parentRectSize = new Vector2 (Screen.width, Screen.height);
        }
        return parentRectSize;
    }

    public SGameObject SetHorizontalStretchAnchor ()
    {
        SetMiddleCenterAnchor ();
        var rectSize = RectSize;
        var anchoredPos = rectTransform.anchoredPosition;
        var parentRectSize = GetParentRectSize ();

        var merginLeft = parentRectSize.x / 2 - RectSize.x / 2 +
            rectTransform.anchoredPosition.x;
        var right = parentRectSize.x / 2 - RectSize.x / 2 -
            rectTransform.anchoredPosition.x;

        SetAnchorAndPivot (AnchorType.HorizontalStretch);
        rectTransform.offsetMax = new Vector2 (-right, rectSize.y / 2);
        rectTransform.offsetMin = new Vector2 (merginLeft, -rectSize.y / 2);
        rectTransform.anchoredPosition = new Vector2 (anchoredPos.x, anchoredPos.y);
        anchorType = AnchorType.HorizontalStretch;
        return this;
    }

    public SGameObject SetVerticalStretchAnchor ()
    {
        SetMiddleCenterAnchor ();
        var rectSize = RectSize;
        var anchoredPos = rectTransform.anchoredPosition;
        var parentRectSize = GetParentRectSize ();

        var merginTop = parentRectSize.y / 2 - RectSize.y / 2 -
            rectTransform.anchoredPosition.y;
        var merginBottom = parentRectSize.y / 2 - RectSize.y / 2 +
            rectTransform.anchoredPosition.y;

        SetAnchorAndPivot (AnchorType.VerticalStretch);
        rectTransform.offsetMax = new Vector2 (rectSize.x / 2, -merginTop);
        rectTransform.offsetMin = new Vector2 (-rectSize.x / 2, merginBottom);
        rectTransform.anchoredPosition = new Vector2 (anchoredPos.x, anchoredPos.y);
        anchorType = AnchorType.VerticalStretch;
        return this;
    }

    public SGameObject SetStretchLeftAnchor ()
    {
        rectTransform.anchorMin = new Vector2 (0, 0);
        rectTransform.anchorMax = new Vector2 (0, 1);
        rectTransform.pivot = new Vector2 (0.5f, 0.5f);
        rectTransform.offsetMax = new Vector2 (0, 0);
        rectTransform.offsetMin = new Vector2 (0, 0);
        return this;
    }

    public SGameObject SetMiddleLeftAnchor ()
    {
        rectTransform.anchorMin = new Vector2 (0f, 0.5f);
        rectTransform.anchorMax = new Vector2 (0f, 0.5f);
        rectTransform.pivot = new Vector2 (0f, 0.5f);
        rectTransform.offsetMax = new Vector2 (0, 0);
        rectTransform.offsetMin = new Vector2 (0, 0);
        return this;
    }

    public SGameObject SetFullStretchAnchor ()
    {
        if (anchorType == AnchorType.FullStretch) return this;
        SetMiddleCenterAnchor ();
        var rectSize = RectSize;
        var preAnchoredPos = rectTransform.anchoredPosition;
        var parentRectSize = GetParentRectSize ();

        var merginLeft = rectTransform.anchoredPosition.x;
        var merginRight = parentRectSize.x - RectSize.x - merginLeft;
        var merginTop = -rectTransform.anchoredPosition.y;
        var merginBottom = parentRectSize.y - RectSize.y - merginTop;

        SetAnchorAndPivot (AnchorType.FullStretch);
        rectTransform.offsetMax = new Vector2 (-merginRight, -merginTop);
        rectTransform.offsetMin = new Vector2 (merginLeft, merginBottom);
        rectTransform.anchoredPosition = new Vector2 (preAnchoredPos.x, preAnchoredPos.y);
        anchorType = AnchorType.FullStretch;
        return this;
    }

    private void SetAnchorAndPivot (AnchorType anchorType)
    {
        switch (anchorType)
        {
            case AnchorType.TopLeft:
                {
                    rectTransform.anchorMin = new Vector2 (0f, 1f);
                    rectTransform.anchorMax = new Vector2 (0f, 1f);
                    rectTransform.pivot = new Vector3 (0.0f, 1.0f);
                    break;
                }
            case AnchorType.FullStretch:
                {
                    rectTransform.anchorMin = new Vector2 (0, 0);
                    rectTransform.anchorMax = new Vector2 (1, 1);
                    rectTransform.pivot = new Vector3 (0.5f, 0.5f);
                    break;
                }
            case AnchorType.MiddleCenter:
                {
                    rectTransform.anchorMin = new Vector2 (0.5f, 0.5f);
                    rectTransform.anchorMax = new Vector2 (0.5f, 0.5f);
                    rectTransform.pivot = new Vector3 (0.5f, 0.5f);
                    break;
                }
            case AnchorType.HorizontalStretch:
                {
                    rectTransform.anchorMin = new Vector2 (0.0f, 0.5f);
                    rectTransform.anchorMax = new Vector2 (1f, 0.5f);
                    rectTransform.pivot = new Vector2 (0.5f, 0.5f);
                    break;
                }
            case AnchorType.VerticalStretch:
                {
                    rectTransform.anchorMin = new Vector2 (0.5f, 0f);
                    rectTransform.anchorMax = new Vector2 (0.5f, 1f);
                    rectTransform.pivot = new Vector2 (0.5f, 0.5f);
                    break;
                }
        }

    }
}