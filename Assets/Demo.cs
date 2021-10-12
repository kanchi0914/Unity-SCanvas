using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame;
using EGUI.Base;
using EGUI.Demo;
using EGUI.GameObjects;
using EGUI.GameObjects.Demos;
using Examples.RpgGame;
using UnityEngine;

public class Demo : MonoBehaviour
{

    void Start()
    {
        // var aaaa = new EGGameObject().SetImageColor(Color.black);
        // new ButtonAndWindow();
        //new RpgMain();
        new CommandBattle();
        // new AdvGameMain();
    }
}
