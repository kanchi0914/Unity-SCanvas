using System;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;


public class RectTransformExtensionsTest
{

    private EGCanvas canvas;
    private RectInfo rectInfo = new RectInfo(0, 0, 100, 100);
    private List<GameObject> rects;

    public RectTransformExtensionsTest()
    {
        ResetButtons();
        var buttons = new EGCanvas("ButtonsCanvas");
        var layout = new EgHorizontalLayoutView(
            buttons.gameObject,
            isAutoAlignment: true,
            isAutoSizingWidth: true,
            isAutoSizingHeight: true
        ).gameObject.SetRectSizeByRatio(1, 0.1f);
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
            rects.ForEach(b => b.gameObject.SetTopCenterAnchor());
        });
        new EGButton(layout, "SetTr").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.gameObject.SetTopRightAnchor());
        });
        new EGButton(layout, "SetMl").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.gameObject.SetMiddleLeftAnchor());
        });
        new EGButton(layout, "SetMc").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.gameObject.SetMiddleCenterAnchor());
        });
        new EGButton(layout, "SetMr").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.gameObject.SetMiddleRightAnchor());
        });
        new EGButton(layout, "SetBl").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.gameObject.SetBottomLeftAnchor());
        });
        new EGButton(layout, "SetBc").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.gameObject.SetBottomCenterAnchor());
        });
        new EGButton(layout, "SetBr").SetOnOnClick(() =>
        {
            rects.ForEach(b => b.gameObject.SetBottomRightAnchor());
        });
    }

    public void ResetButtons()
    {
        canvas?.gameObject.DestroySelf();
        canvas = new EGCanvas("TestRects");
        
        var pareImage = new EGGameObject(canvas.gameObject)
            .gameObject
            .SetImageColor(Color.black)
            .SetRectSize(500, 500)
            .SetMiddleCenterAnchor()
            .SetLocalPos(0, 0);

        rects = new List<GameObject>()
        {
            new EGButton(pareImage, "tl")
                .gameObject
                .SetTopLeftAnchor()
                .SetPresetRect(rectInfo),
            
            new EGButton(pareImage, "tc")
                .gameObject
                .SetTopCenterAnchor().SetPresetRect(rectInfo),
            
            new EGButton(pareImage, "tr")
                .gameObject
                .SetTopRightAnchor().SetPresetRect(rectInfo),
            
            new EGButton(pareImage, "ml")
                .gameObject
                .SetMiddleLeftAnchor().SetPresetRect(rectInfo),
            
            new EGButton(pareImage, "mc")
                .gameObject
                .SetMiddleCenterAnchor().SetPresetRect(rectInfo),
            
            new EGButton(pareImage, "mr")
                .gameObject
                .SetMiddleRightAnchor().SetPresetRect(rectInfo),
            
            new EGButton(pareImage, "bl")
                .gameObject
                .SetBottomLeftAnchor().SetPresetRect(rectInfo),
            
            new EGButton(pareImage, "bc")
                .gameObject
                .SetBottomCenterAnchor().SetPresetRect(rectInfo),
            
            new EGButton(pareImage, "br")
                .gameObject
                .SetBottomRightAnchor().SetPresetRect(rectInfo)
        };

    }
}
