using System;
using Assets.Scripts.Extensions;
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
        var canvas = new EGCanvas("test");
        new EGScrollBar(canvas)
            .SetRectSize(200, 20).SetMiddleCenterAnchor().SetLocalPos(0,0);
    }
    
    public void Init()
    {
        var canvas = new EGCanvas("test");
        var root = new EgImage(canvas)
            .SetRectSize(300, 20)
            .SetMiddleCenterAnchor()
            .SetLocalPos(0, 0) as EgImage;

        var dropdownComponent = root.GameObject.TryAddComponent<Dropdown>();
        
        var label = new EGText(root, name: "label");
        var arrow = new EgImage(root, name: "arrow"); 
        var template = new EgVerticalLayoutScrollView(root, name:"template");
        
        
        //dropdownの設定
        dropdownComponent.targetGraphic = root.Image;
        dropdownComponent.template = template.RectTransform;
        dropdownComponent.captionText = label.TextComponent;

        new EGScrollBar(canvas);

        // var template = new EGUIObject(this, name: "Template");
        // var viewport = new EGUIObject(this, name: "Viewport");
        // var content = new EGUIObject(this, name: "Content");
        // var item = new EGUIObject(this, name: "Item Background");
        // var itemBackground = new EGUIObject(this, name: "Item Checkmark");
        // var itemLabel = new EGUIObject(this, name: "Item Label");
        //     


    }
}