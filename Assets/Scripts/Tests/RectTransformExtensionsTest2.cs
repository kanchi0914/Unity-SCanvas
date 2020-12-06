﻿using System;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

public class RectTransformExtensionsTest2
{

    private EGCanvas canvas;
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
            .SetAnchorType(AnchorType.BottomCenter)
            .SetRelativeSize(1, 0.1f)
            .SetImageColor(Color.white)
            .gameObject;
        
        new EGButton(layout, "Reset").SetOnOnClick(() =>
        {
            ResetButtons();
        });
        new EGButton(layout, "SetTl").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.TopLeft, true));
        });
        new EGButton(layout, "SetTc").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.TopCenter, true));
        });
        new EGButton(layout, "SetTr").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.TopRight, true));
        });
        new EGButton(layout, "SetMl").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.MiddleLeft, true));
        });
        new EGButton(layout, "SetMc").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.MiddleCenter, true));
        });
        new EGButton(layout, "SetMr").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.MiddleRight, true));
        });
        new EGButton(layout, "SetBl").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.BottomLeft, true));
        });
        new EGButton(layout, "SetBc").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.BottomCenter, true));
        });
        new EGButton(layout, "SetBr").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.BottomRight, true));
        });
        new EGButton(layout, "SetHs").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.HorizontalStretch,true));
        });
        new EGButton(layout, "SetHs_t").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.HorizontalStretchWithTopPivot, true));
        });
        new EGButton(layout, "SetHs_b").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.HorizontalStretchWithBottomPivot, true));
        });
        new EGButton(layout, "SetVs").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.VerticalStretch, true));
        });
        new EGButton(layout, "SetVs_l").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.VerticalStretchWithLeftPivot, true));
        });
        new EGButton(layout, "SetVs_r").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.VerticalStretchWithRightPivot, true));
        });
        new EGButton(layout, "SetFs").SetOnOnClick(() =>
        {
            objects.ForEach(b => b.SetAnchorType(AnchorType.FullStretch, true));
        });
    }

    public void ResetButtons()
    {
        canvas?.gameObject.DestroySelf();
        canvas = new EGCanvas("test");

        var pareImage = new EGGameObject(canvas.gameObject)
            .SetImageColor(Color.black)
            .SetSize(300, 300)
            .SetAnchorType(AnchorType.MiddleCenter)
            .gameObject;

            
        objects = new List<EGGameObject>()
        {
            new EGButton(pareImage, "hs_t")
                .SetAnchorType(AnchorType.TopLeft)
                .SetSize(50,50)
                .SetAnchorType(AnchorType.HorizontalStretchWithTopPivot, true),

             new EGButton(pareImage, "hs_c")
                 .SetAnchorType(AnchorType.MiddleLeft)
                 .SetSize(50,50)
                 .SetAnchorType(AnchorType.HorizontalStretch, true),
            
             new EGButton(pareImage, "hs_b")
                 .SetAnchorType(AnchorType.BottomLeft)
                 .SetSize(50,50)
                 .SetAnchorType(AnchorType.HorizontalStretchWithBottomPivot, true),
            
             new EGButton(pareImage, "vs_l")
                 .SetAnchorType(AnchorType.MiddleLeft)
                 .SetSize(50,50)
                 .SetAnchorType(AnchorType.VerticalStretchWithLeftPivot, true),
             
             new EGButton(pareImage, "vs_m")
                 .SetAnchorType(AnchorType.MiddleCenter)
                 .SetSize(50,50)
                 .SetAnchorType(AnchorType.VerticalStretch, true),
            
             new EGButton(pareImage, "vs_r")
                 .SetAnchorType(AnchorType.MiddleRight)
                 .SetSize(50,50)
                 .SetAnchorType(AnchorType.VerticalStretchWithRightPivot, true),
            
             new EGButton(pareImage, "fs")
                 .SetAnchorType(AnchorType.MiddleCenter)
                 .SetSize(50,50)
                 .SetAnchorType(AnchorType.FullStretch, true)
        };

    }
}
