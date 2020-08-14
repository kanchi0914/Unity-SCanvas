using System;
using System.Collections.Generic;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using static EGUI.Base.Utils.AnchorType;

public class RectTransformExtensionsTest
{

    private EGCanvas canvas;
    private RectInfo rectInfo = new RectInfo(0, 0, 100, 100);
    private List<EGGameObject> rects;

    public void TestSetAnchor()
    {
        ResetButtons();
        var buttons = new EGCanvas();
        var layout = new EgHorizontalLayoutView(
            buttons,
            isAutoAlignment: true,
            isAutoSizingWidth: true,
            isAutoSizingHeight: true
        ).SetRectSizeByRatio(1, 0.1f);
        new EGButton(layout, "Reset").SetOnOnClick(() =>
        {
            ResetButtons();
        });
        new EGButton(layout, "SetTl").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetTopLeftAnchor() );
        });
        new EGButton(layout, "SetTc").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetTopCenterAnchor());
        });
        new EGButton(layout, "SetTr").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetTopRightAnchor());
        });
        new EGButton(layout, "SetMl").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetMiddleLeftAnchor());
        });
        new EGButton(layout, "SetMc").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetMiddleCenterAnchor());
        });
        new EGButton(layout, "SetMr").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetMiddleRightAnchor());
        });
        new EGButton(layout, "SetBl").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetBottomLeftAnchor());
        });
        new EGButton(layout, "SetBc").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetBottomCenterAnchor());
        });
        new EGButton(layout, "SetBr").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetBottomRightAnchor());
        });
    }

    public void ResetButtons()
    {
        canvas?.Dispose();
        canvas = new EGCanvas("test");
        
        var pareImage = new EgImage(canvas)
            .SetColor(Color.black)
            .SetRectSize(500, 500)
            .SetMiddleCenterAnchor()
            .SetLocalPos(0, 0);

        var tl = new EGButton(pareImage, "tl")
            .SetTopLeftAnchor().SetPresetRect(rectInfo);
        
        var tc = new EGButton(pareImage, "tc")
            .SetTopCenterAnchor().SetPresetRect(rectInfo);
        
        var tr = new EGButton(pareImage, "tr")
            .SetTopRightAnchor().SetPresetRect(rectInfo);
        
        var ml = new EGButton(pareImage, "ml")
            .SetMiddleLeftAnchor().SetPresetRect(rectInfo);
        
        var mc = new EGButton(pareImage, "mc")
            .SetMiddleCenterAnchor().SetPresetRect(rectInfo);
        
        var mr = new EGButton(pareImage, "mr")
            .SetMiddleRightAnchor().SetPresetRect(rectInfo);
        
        var bl = new EGButton(pareImage, "bl")
            .SetBottomLeftAnchor().SetPresetRect(rectInfo);
        
        var bc = new EGButton(pareImage, "bc")
            .SetBottomCenterAnchor().SetPresetRect(rectInfo);
        
        var br = new EGButton(pareImage, "br")
            .SetBottomRightAnchor().SetPresetRect(rectInfo);
        rects = new List<EGGameObject>(){tl, tc, tr, ml, mc, mr, bl, bl, bc, br};
    }
}
