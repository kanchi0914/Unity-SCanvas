using System;

using HC.UI;
using UnityEngine;
using UnityEngine.UI;
using static EGUI.Base.Utils;
using EGUI.Base;
using EGUI.GameObjects;
using static UnityEngine.UI.ScrollRect;
using static HC.UI.UICreator;

public class Main : MonoBehaviour
{

    private EgImage image;
    void Start ()
    {
        var sc = new EGCanvas("aaaaaaaaa");
        new CommandBattleRpg();
    }
}

public class MyToggle : EgToggle
{
    public MyToggle(EGGameObject parent, string name) : base(parent, isWithBoxImage: true, isGrouped: true, name: name)
    {
        SetAnchorType(AnchorType.MiddleCenter).SetLocalPos(0, 0);
        SetRectSize(400, 50);
        SetBackGroundImage("Images/Tab");
        EnabledBackGroundImageColor = Color.white;
        DisabledBackGroundImageColor = Color.gray;
        EnabledTextColor = Color.black;
        DisabledTextColor = Color.black;
        Text.SetTextAlignment(TextAnchor.MiddleCenter);
    }
}

public class MyImage : EgImage
{
    public MyImage(EGGameObject parent) : base(parent)
    {

    }
}