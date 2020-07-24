using System;
using SGUI.SGameObjects;
using UnityEngine;

public class RectTransformExtensionsTest : MonoBehaviour
{
    private void Start()
    {
        var canvas = new SCanvas();
        var layout = new SHorizontalLayoutView(canvas, 5)
            .SetTopLeftAnchor()
            .SetLocalPosByRatio(0f, 0.8f)
            .SetRectSizeByRatio(1, 0.2f);

        //new SImage(layout);

        var parentBaseImage = new SImage(canvas)
            .SetMiddleCenterAnchor()
            .SetRectSize(300, 300);

        var childImage = new SImage(parentBaseImage)
            .SetTopLeftAnchor()
            .SetRectSize(100, 100)
            .SetLocalPos(40, 80);

        new SButton(layout, "Vertical")
            .AddOnClick(() => childImage.SetVerticalStretchAnchor());
        new SButton(layout, "MIDDLECENTER")
            .AddOnClick(() => childImage.SetMiddleCenterAnchor());
        new SButton(layout, "FULL")
            .AddOnClick(() => childImage.SetFullStretchAnchor());
        new SButton(layout, "HORIZONTAL")
            .AddOnClick(() => childImage.SetHorizontalStretchAnchor());
        new SButton(layout, "TOPLEFT")
            .AddOnClick(() => childImage.SetTopLeftAnchor());
    }
}
