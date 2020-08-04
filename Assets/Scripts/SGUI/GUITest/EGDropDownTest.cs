using System;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.SGUI.GUITest
{
    public class EGDropDownTest : MonoBehaviour
    {
        private void Start()
        {
            var canvas = new EGCanvas();
            var dropdown = new EGDropDown(canvas)
                .SetRectSize(200, 50)
                .SetMiddleCenterAnchor()
                .SetLocalPos(0, 0) as EGDropDown;
            dropdown.AddOption("", () => Debug.Log(""));
            dropdown.AddOption("", () => Debug.Log(""));
            dropdown.AddOption("", () => Debug.Log(""));
            dropdown.AddOption("", () => Debug.Log(""));

        }
    }
}