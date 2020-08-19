using System;
using System.Collections.Generic;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using static EGUI.Base.Utils.AnchorType;

public class RectTransformExtensionsTest2
{

    private EGCanvas canvas;
    private RectInfo rectInfo = new RectInfo(0, 0, 50, 50);
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
            )
            .SetRectSizeByRatio(1, 0.1f)
            .SetImageColor(Color.white);

       //  new EgImage(buttons).SetRectSize(100, 100);
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
        new EGButton(layout, "SetHs").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetHorizontalStretchAnchor());
        });
        new EGButton(layout, "SetHs_t").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetHorizontalStretchWithTopPivotAnchor());
        });
        new EGButton(layout, "SetHs_b").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetHorizontalStretchWithBottomPivotAnchor());
        });
        new EGButton(layout, "SetVs").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetVerticalStretchAnchor());
        });
        new EGButton(layout, "SetVs_l").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetVerticalStretchWithLeftPivotAnchor());
        });
        new EGButton(layout, "SetVs_r").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetVerticalStretchWithRightPivotAnchor());
        });
        new EGButton(layout, "SetFs").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.SetFullStretchAnchor());
        });
    }

    public void ResetButtons()
    {
        canvas?.DestroySelf();
        canvas = new EGCanvas("test");
        
        var pareImage = new EGImage(canvas)
            .SetColor(Color.black)
            .SetRectSize(300, 300)
            .SetMiddleCenterAnchor()
            .SetLocalPos(0, 0);
        //
        var hs_t = new EGButton(pareImage, "hs_l")
            .SetTopLeftAnchor().SetPresetRect(rectInfo)
            .SetHorizontalStretchWithTopPivotAnchor();
        
        var hs_c = new EGButton(pareImage, "hs_c")
            .SetMiddleLeftAnchor().SetPresetRect(rectInfo)
            .SetHorizontalStretchAnchor();
        
        var hs_b = new EGButton(pareImage, "hs_r")
            .SetBottomLeftAnchor().SetPresetRect(rectInfo)
            .SetHorizontalStretchWithBottomPivotAnchor();
        
        var vs_l = new EGButton(pareImage, "vs_l")
            .SetMiddleLeftAnchor().SetPresetRect(rectInfo)
            .SetVerticalStretchWithLeftPivotAnchor();
        //
         var vs_m = new EGButton(pareImage, "vs_m")
             .SetMiddleCenterAnchor().SetPresetRect(rectInfo)
             .SetVerticalStretchAnchor();
        
         var vs_b = new EGButton(pareImage, "vs_r")
             .SetMiddleRightAnchor().SetPresetRect(rectInfo)
            .SetVerticalStretchWithRightPivotAnchor();
        
         var fs = new EGButton(pareImage, "fs")
             .SetMiddleCenterAnchor().SetPresetRect(rectInfo)
             .SetFullStretchAnchor();

        rects = new List<EGGameObject>(){hs_t, hs_c, hs_b, fs, vs_l, vs_m, vs_b};
    }
}
