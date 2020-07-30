using System;
using SGUI.GameObjects;
using UnityEngine;
using static SGUI.Base.Utils.AnchorType;

public class RectTransformExtensionsTest : MonoBehaviour
{
    private void Start()
    {
        var canvas = new SCanvas();
        var layout = new SHorizontalLayoutView(canvas, true)
            .SetAnchorType(TopLeft)
            .SetLocalPosByRatio(0f, 0.8f)
            .SetRectSizeByRatio(1, 0.2f);

        //new SImage(layout);

        var parentBaseImage = new SImage(canvas)
            .SetAnchorType(MiddleCenter)
            .SetRectSize(300, 300);

        var childImage = new SImage(parentBaseImage)
            .SetAnchorType(TopLeft)
            .SetRectSize(100, 100)
            .SetLocalPos(40, 80);

        new SButton(layout, "Vertical")
            .AddOnClick(() => childImage.SetAnchorType(VerticalStretch));
        new SButton(layout, "MIDDLECENTER")
            .AddOnClick(() => childImage.SetAnchorType(MiddleCenter));
        new SButton(layout, "FULL")
            .AddOnClick(() => childImage.SetAnchorType(FullStretch));
        new SButton(layout, "HORIZONTAL")
            .AddOnClick(() => childImage.SetAnchorType(HorizontalStretch));
        new SButton(layout, "TOPLEFT")
            .AddOnClick(() => childImage.SetAnchorType(TopLeft));
    }
}
