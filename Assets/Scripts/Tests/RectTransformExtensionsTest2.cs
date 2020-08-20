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
    private List<EGGameObject> objects;

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
            .SetRectSizeByRatio(1, 0.1f)
            .SetImageColor(Color.white)
            .gameObject;
        
        new EGButton(layout, "Reset").SetOnOnClick(() =>
        {
            ResetButtons();
        });
        new EGButton(layout, "SetTl").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetTopLeftAnchor() );
        });
        new EGButton(layout, "SetTc").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetTopCenterAnchor());
        });
        new EGButton(layout, "SetTr").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetTopRightAnchor());
        });
        new EGButton(layout, "SetMl").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetMiddleLeftAnchor());
        });
        new EGButton(layout, "SetMc").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetMiddleCenterAnchor());
        });
        new EGButton(layout, "SetMr").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetMiddleRightAnchor());
        });
        new EGButton(layout, "SetBl").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetBottomLeftAnchor());
        });
        new EGButton(layout, "SetBc").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetBottomCenterAnchor());
        });
        new EGButton(layout, "SetBr").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetBottomRightAnchor());
        });
        new EGButton(layout, "SetHs").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetHorizontalStretchAnchor());
        });
        new EGButton(layout, "SetHs_t").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetHorizontalStretchWithTopPivotAnchor());
        });
        new EGButton(layout, "SetHs_b").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetHorizontalStretchWithBottomPivotAnchor());
        });
        new EGButton(layout, "SetVs").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetVerticalStretchAnchor());
        });
        new EGButton(layout, "SetVs_l").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetVerticalStretchWithLeftPivotAnchor());
        });
        new EGButton(layout, "SetVs_r").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetVerticalStretchWithRightPivotAnchor());
        });
        new EGButton(layout, "SetFs").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetFullStretchAnchor());
        });
    }

    public void ResetButtons()
    {
        canvas?.gameObject.DestroySelf();
        canvas = new EGCanvas("test");

        var pareImage = new EGGameObject(canvas.gameObject)
            .SetImageColor(Color.black)
            .SetRectSize(300, 300)
            .SetMiddleCenterAnchor()
            .SetLocalPos(0, 0).gameObject;

            
        objects = new List<EGGameObject>()
        {
            new EGButton(pareImage, "hs_t")
                 .SetTopLeftAnchor().SetPresetRect(rectInfo)
                 .SetHorizontalStretchWithTopPivotAnchor(),

             new EGButton(pareImage, "hs_c")
                 .SetMiddleLeftAnchor().SetPresetRect(rectInfo)
                 .SetHorizontalStretchAnchor(),
            
             new EGButton(pareImage, "hs_b")
                 .SetBottomLeftAnchor().SetPresetRect(rectInfo)
                 .SetHorizontalStretchWithBottomPivotAnchor(),
            
             new EGButton(pareImage, "vs_l")
                 .SetMiddleLeftAnchor().SetPresetRect(rectInfo)
                 .SetVerticalStretchWithLeftPivotAnchor(),
             //
             new EGButton(pareImage, "vs_m")
                 .SetMiddleCenterAnchor().SetPresetRect(rectInfo)
                 .SetVerticalStretchAnchor(),
            
             new EGButton(pareImage, "vs_r")
                 .SetMiddleRightAnchor().SetPresetRect(rectInfo)
                 .SetVerticalStretchWithRightPivotAnchor(),
            
             new EGButton(pareImage, "fs")
                 .SetMiddleCenterAnchor().SetPresetRect(rectInfo)
                 .SetFullStretchAnchor()
        };

    }
}
