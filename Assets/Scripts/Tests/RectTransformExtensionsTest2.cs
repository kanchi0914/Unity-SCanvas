using System;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

public class RectTransformExtensionsTest2
{

    private EGCanvas canvas;
    private RectInfo rectInfo = new RectInfo(0, 0, 50, 50);
    private List<GameObject> rects;

    public RectTransformExtensionsTest2()
    {
        ResetButtons();
        var buttons = new EGCanvas("");
        var layout = new EgHorizontalLayoutView(
                buttons.gameObject,
                isAutoAlignment: true,
                isAutoSizingWidth: true,
                isAutoSizingHeight: true
            )
            .gameObject
            .SetRectSizeByRatio(1, 0.1f)
            .SetImageColor(Color.white);
        
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
        canvas?.gameObject.DestroySelf();
        canvas = new EGCanvas("test");
        
        var pareImage = new EGGameObject(canvas.gameObject)
            .gameObject
            .SetImageColor(Color.black)
            .SetRectSize(300, 300)
            .SetMiddleCenterAnchor()
            .SetLocalPos(0, 0);

            
        rects = new List<GameObject>()
        {

             new EGButton(pareImage, "hs_t")
                 .gameObject
                 .SetTopLeftAnchor().SetPresetRect(rectInfo)
                 .SetHorizontalStretchWithTopPivotAnchor(),

             new EGButton(pareImage, "hs_c")
                 .gameObject
                 .SetMiddleLeftAnchor().SetPresetRect(rectInfo)
                 .SetHorizontalStretchAnchor(),
            
             new EGButton(pareImage, "hs_b")
                 .gameObject
                 .SetBottomLeftAnchor().SetPresetRect(rectInfo)
                 .SetHorizontalStretchWithBottomPivotAnchor(),
            
             new EGButton(pareImage, "vs_l")
                 .gameObject
                 .SetMiddleLeftAnchor().SetPresetRect(rectInfo)
                 .SetVerticalStretchWithLeftPivotAnchor(),
             //
             new EGButton(pareImage, "vs_m")
                 .gameObject
                 .SetMiddleCenterAnchor().SetPresetRect(rectInfo)
                 .SetVerticalStretchAnchor(),
            
             new EGButton(pareImage, "vs_r")
                 .gameObject
                 .SetMiddleRightAnchor().SetPresetRect(rectInfo)
                 .SetVerticalStretchWithRightPivotAnchor(),
            
             new EGButton(pareImage, "fs")
                 .gameObject
                 .SetMiddleCenterAnchor().SetPresetRect(rectInfo)
                 .SetFullStretchAnchor()
        };

    }
}
