﻿using System;
using Assets.Scripts.Examples.AdvGame.Objects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
 using UnityEngine.Networking;
 using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame
{
    public class AdvGameOpening
    {
        private EGCanvas canvas;
        private CanvasRenderer renderer = CanvasRenderer.Instance;
        
        public AdvGameOpening()
        {
            Init();
        }

        private void Init()
        {
            renderer.SetStartMenu();
        }
    }
}