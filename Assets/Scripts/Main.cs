using System;

using HC.UI;
using UnityEngine;
using UnityEngine.UI;
using static SGUI.Base.Utils;
using SGUI.Base;
using SGUI.GameObjects;
using static UnityEngine.UI.ScrollRect;
using static HC.UI.UICreator;

public class Main : MonoBehaviour
{

    private SImage image;
    void Start ()
    {
        var sc = new SCanvas("aaaaaaaaa");
        new CommandBattleRpg();
    }
}

public class MyToggle : SToggle
{
    public MyToggle(SGameObject parent, string name) : base(parent, isWithBoxImage: true, isGrouped: true, name: name)
    {
        SetAnchorType(AnchorType.MiddleCenter).SetLocalPos(0, 0);
        SetRectSize(400, 50);
        SetBackGroundImage("Images/Tab");
        EnabledBackGroundImageColor = Color.white;
        DisabledBackGroundImageColor = Color.gray;
        EnabledTextColor = Color.black;
        DisabledTextColor = Color.black;
        Text.SetTextAnchor(TextAnchor.MiddleCenter);
    }
}

public class MyImage : SImage
{
    public MyImage(SGameObject parent) : base(parent)
    {

    }
}