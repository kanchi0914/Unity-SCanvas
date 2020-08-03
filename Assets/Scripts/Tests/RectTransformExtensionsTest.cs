using System;
using EGUI.GameObjects;
using UnityEngine;
using static EGUI.Base.Utils.AnchorType;

public class RectTransformExtensionsTest : MonoBehaviour
{
    private void Start()
    {
        var canvas = new EGCanvas();
        var layout = new EgHorizontalLayoutView(canvas, true)
            .SetAnchorType(TopLeft)
            .SetLocalPosByRatio(0f, 0.8f)
            .SetRectSizeByRatio(1, 0.2f);

        //new SImage(layout);

        var parentBaseImage = new EgImage(canvas)
            .SetAnchorType(MiddleCenter)
            .SetRectSize(300, 300);

        var childImage = new EgImage(parentBaseImage)
            .SetAnchorType(TopLeft)
            .SetRectSize(100, 100)
            .SetLocalPos(40, 80);

        new EGButton(layout, "Vertical")
            .AddOnClick(() => childImage.SetAnchorType(VerticalStretch));
        new EGButton(layout, "MIDDLECENTER")
            .AddOnClick(() => childImage.SetAnchorType(MiddleCenter));
        new EGButton(layout, "FULL")
            .AddOnClick(() => childImage.SetAnchorType(FullStretch));
        new EGButton(layout, "HORIZONTAL")
            .AddOnClick(() => childImage.SetAnchorType(HorizontalStretch));
        new EGButton(layout, "TOPLEFT")
            .AddOnClick(() => childImage.SetAnchorType(TopLeft));
    }
}
