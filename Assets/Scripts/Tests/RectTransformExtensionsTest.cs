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
    private List<EGGameObject> rects;

    public RectTransformExtensionsTest()
    {
        ResetButtons();
        var buttons = new EGCanvas("ButtonsCanvas");
        var layout = new EgHorizontalLayoutView(
                buttons.gameObject,
                isAutoAlignment: true,
                isAutoSizingWidth: true,
                isAutoSizingHeight: true
            ).SetRectSizeByRatio(1, 0.1f)
            .gameObject;
        new EGButton(layout, "Reset").SetOnOnClick(() => { ResetButtons(); });
        new EGButton(layout, "SetTl").SetOnOnClick(() => { rects.ForEach(b => b.SetTopLeftAnchor()); });
        new EGButton(layout, "SetTc").SetOnOnClick(() => { rects.ForEach(b => b.SetTopCenterAnchor()); });
        new EGButton(layout, "SetTr").SetOnOnClick(() => { rects.ForEach(b => b.SetTopRightAnchor()); });
        new EGButton(layout, "SetMl").SetOnOnClick(() => { rects.ForEach(b => b.SetMiddleLeftAnchor()); });
        new EGButton(layout, "SetMc").SetOnOnClick(() => { rects.ForEach(b => b.SetMiddleCenterAnchor()); });
        new EGButton(layout, "SetMr").SetOnOnClick(() => { rects.ForEach(b => b.SetMiddleRightAnchor()); });
        new EGButton(layout, "SetBl").SetOnOnClick(() => { rects.ForEach(b => b.SetBottomLeftAnchor()); });
        new EGButton(layout, "SetBc").SetOnOnClick(() => { rects.ForEach(b => b.SetBottomCenterAnchor()); });
        new EGButton(layout, "SetBr").SetOnOnClick(() => { rects.ForEach(b => b.SetBottomRightAnchor()); });
    }

    public void ResetButtons()
    {
        canvas?.gameObject.DestroySelf();
        canvas = new EGCanvas("TestRects");

        var pareImage = new EGGameObject(canvas.gameObject)
            .SetImageColor(Color.black)
            .SetRectSize(500, 500)
            .SetMiddleCenterAnchor()
            .SetLocalPos(0, 0).gameObject;

        rects = new List<EGGameObject>()
        {
            new EGButton(pareImage, "tl")
                .SetTopLeftAnchor()
                .SetPresetRect(rectInfo),

            new EGButton(pareImage, "tc")
                .SetTopCenterAnchor().SetPresetRect(rectInfo),

            new EGButton(pareImage, "tr")
                .SetTopRightAnchor().SetPresetRect(rectInfo),

            new EGButton(pareImage, "ml")
                .SetMiddleLeftAnchor().SetPresetRect(rectInfo),

            new EGButton(pareImage, "mc")
                .SetMiddleCenterAnchor().SetPresetRect(rectInfo),

            new EGButton(pareImage, "mr")
                .SetMiddleRightAnchor().SetPresetRect(rectInfo),

            new EGButton(pareImage, "bl")
                .SetBottomLeftAnchor().SetPresetRect(rectInfo),

            new EGButton(pareImage, "bc")
                .SetBottomCenterAnchor().SetPresetRect(rectInfo),

            new EGButton(pareImage, "br")
                .SetBottomRightAnchor().SetPresetRect(rectInfo)
        };
    }
}