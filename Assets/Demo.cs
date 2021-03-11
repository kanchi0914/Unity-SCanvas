using System.Collections;
using System.Collections.Generic;
using EGUI.Demo;
using EGUI.GameObjects;
using EGUI.GameObjects.Demos;
using UnityEngine;

public class Demo : MonoBehaviour
{
    void Start()
    {
        // new ButtonDemo();
        // new EGDropDown()
        //     .AddOption("text1", null)
        //     .AddOption("text2", null)
        //     .AddOption("text3", null)
        //     .SetSize(200,50);

        new EgSlider().SetSize(200, 40);
    }
}
