using System;
using System.Collections.Generic;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;


public class RectTransformExtensionsTest
{
    private EGCanvas canvas;
    private List<EGGameObject> rects;
    private float buttonWidth = 50;
    private float buttonHeight = 50;

    public RectTransformExtensionsTest()
    {
        ResetButtons();
        var buttons = new EGCanvas("ButtonsCanvas");
        var layout = new EgHorizontalLayoutView(
                buttons.gameObject,
                isAutoAlignment: true,
                isAutoSizingWidth: true,
                isAutoSizingHeight: true
            )
            .SetAnchorType(AnchorType.BottomCenter)
            .SetRelativeSize(1, 0.1f)
            .gameObject;
        new EGButton(layout, "Reset").AddOnClick(() => { ResetButtons(); });
        new EGButton(layout, "SetTl").AddOnClick(() =>
        {
            rects.ForEach(b => b.SetAnchorType(AnchorType.TopLeft, true));
        });
        new EGButton(layout, "SetTc").AddOnClick(() =>
        {
            rects.ForEach(b => b.SetAnchorType(AnchorType.TopCenter, true));
        });
        new EGButton(layout, "SetTr").AddOnClick(() =>
        {
            rects.ForEach(b => b.SetAnchorType(AnchorType.TopRight, true));
        });
        new EGButton(layout, "SetMl").AddOnClick(() =>
        {
            rects.ForEach(b => b.SetAnchorType(AnchorType.MiddleLeft, true));
        });
        new EGButton(layout, "SetMc").AddOnClick(() =>
        {
            rects.ForEach(b => b.SetAnchorType(AnchorType.MiddleCenter, true));
        });
        new EGButton(layout, "SetMr").AddOnClick(() =>
        {
            rects.ForEach(b => b.SetAnchorType(AnchorType.MiddleRight, true));
        });
        new EGButton(layout, "SetBl").AddOnClick(() =>
        {
            rects.ForEach(b => b.SetAnchorType(AnchorType.BottomLeft, true));
        });
        new EGButton(layout, "SetBc").AddOnClick(() =>
        {
            rects.ForEach(b => b.SetAnchorType(AnchorType.BottomCenter, true));
        });
        new EGButton(layout, "SetBr").AddOnClick(() =>
        {
            rects.ForEach(b => b.SetAnchorType(AnchorType.BottomRight, true));
        });
    }

    public void ResetButtons()
    {
        canvas?.gameObject.DestroySelf();
        canvas = new EGCanvas("TestRects");

        var pareImage = new EGGameObject(canvas.gameObject)
            .SetImageColor(Color.black)
            .SetSize(300, 300)
            .SetAnchorType(AnchorType.MiddleCenter)
            .SetPosition(0, 0).gameObject;

        rects = new List<EGGameObject>()
        {
            new EGButton(pareImage, "tl")
                .SetAnchorType(AnchorType.TopLeft).SetSize(buttonWidth, buttonHeight),

            new EGButton(pareImage, "tc")
                .SetAnchorType(AnchorType.TopCenter).SetSize(buttonWidth, buttonHeight),

            new EGButton(pareImage, "tr")
                .SetAnchorType(AnchorType.TopRight).SetSize(buttonWidth, buttonHeight),

            new EGButton(pareImage, "ml")
                .SetAnchorType(AnchorType.MiddleLeft).SetSize(buttonWidth, buttonHeight),

            new EGButton(pareImage, "mc")
                .SetAnchorType(AnchorType.MiddleCenter).SetSize(buttonWidth, buttonHeight),

            new EGButton(pareImage, "mr")
                .SetAnchorType(AnchorType.MiddleRight).SetSize(buttonWidth, buttonHeight),

            new EGButton(pareImage, "bl")
                .SetAnchorType(AnchorType.BottomLeft).SetSize(buttonWidth, buttonHeight),

            new EGButton(pareImage, "bc")
                .SetAnchorType(AnchorType.BottomCenter).SetSize(buttonWidth, buttonHeight),

            new EGButton(pareImage, "br")
                .SetAnchorType(AnchorType.BottomRight).SetSize(buttonWidth, buttonHeight)
        };
    }
}