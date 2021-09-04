using System;
using System.Collections;
using System.Collections.Generic;
using EGUI.Base;
using EGUI.Demo;
using EGUI.GameObjects;
using EGUI.GameObjects.Demos;
using UnityEngine;

public class Demo : MonoBehaviour
{
    private void Update()
    {
        Debug.Log("bbbbbb");
    }

    void Start()
    {
        // new ButtonDemo();
        // new EGDropDown()
        //     .AddOption("text1", null)
        //     .AddOption("text2", null)
        //     .AddOption("text3", null)
        //     .SetSize(200,50);
        
        var aaa = new EGText().SetText("bbbbb").SetAnchorType(AnchorType.MiddleCenter);

        // var par = new EGGameObject().SetSize(400, 100);
        //
        // new EGToggle(par).SetAnchorType(AnchorType.TopLeft).SetRelativeSize(.33f, 1f);
        // new EGToggle(par).SetAnchorType(AnchorType.TopLeft).SetRelativePosition(.33f, 0).SetRelativeSize(.33f, 1f);
        // new EGToggle(par).SetAnchorType(AnchorType.TopLeft).SetRelativePosition(.66f, 0).SetRelativeSize(.33f, 1f);
        
        // new EGToggle(isWithCheckBoxImage:false)
        //     .SetImageColor(Color.white)
        //     .SetSize(200, 50);
        //
        // new EGToggle(isWithCheckBoxImage:false)
        //     .SetImageColor(Color.white)
        //     .SetPosition(200, 0)
        //     .SetSize(200, 50);

        //new ButtonAndWindow();

        // new EgSlider().SetSize(200, 40);
    }
}
