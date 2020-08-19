using System;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame.Objects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Scenario
    {
        //public List<List<EgMessageWindow.Section>> Scripts = new List<List<EgMessageWindow.Section>>(); 
        public Dictionary<string, List<EgMessageWindow.Section>> Scripts =
            new Dictionary<string, List<EgMessageWindow.Section>>();
        public List<(Charactor, EGImage)> CharactorImages = new List<(Charactor, EGImage)>();
        private EGCanvas backGroundImageCanvas;
        private EGCanvas charactorImageCanvas;
        private EgHorizontalLayoutView images;
        private EGCanvas messageWindowCanvas;
        public AdvMessageWindow AdvMessageWindow;

        public EGImage BackgroundImage { get; set; }
        public Scenario ()
        {
            CanvasStack.ClearAll();
            Init();
        }
        
        private void Init(){
            backGroundImageCanvas = new EGCanvas("backGroundImageCanvas");
            charactorImageCanvas = new EGCanvas("charactorImageCanvas");
            images = new EgHorizontalLayoutView(charactorImageCanvas, isAutoAlignment:true)
                .SetRectSizeByRatio(1,1) as EgHorizontalLayoutView;
            images.LayoutComponent.childAlignment = TextAnchor.MiddleCenter;
            messageWindowCanvas = new EGCanvas("messageWindowCanvas");
            AdvMessageWindow = new AdvMessageWindow(messageWindowCanvas);
        }

        public void Load(string scriptId, int sectionNumber)
        {
            var script = Scripts[scriptId];
            script.RemoveRange(0, sectionNumber);
            AdvMessageWindow.SetMessageAndActions(script);
            AdvMessageWindow.Image.raycastTarget = true;
        }
        
        public void SetBackGroundImage(string imageFilePath)
        {
            BackgroundImage?.DestroySelf();;
            BackgroundImage = new EGImage(backGroundImageCanvas);
            Utils.SetBackgroundImage(BackgroundImage, imageFilePath);
        }
        
        public void AddCharactor(string name, string imageName)
        {
            var chara = GameData.Charactors.Find(c => c.Name == name);
            var image = new EGImage(images);
            image.SetImageSource(chara.ImageMap[imageName]);
            CharactorImages.Add((chara, image));
            image.SetRectSizeByRatio(0.4f, 0.9f);
        }

        public void RemoveCharactor(string name)
        {
            CharactorImages.ForEach(c =>
            {
                if (c.Item1.Name == name)
                {
                    c.Item2.DestroySelf();;
                }
            });
            CharactorImages.RemoveAll(c => c.Item1.Name == name);
        }

        public void SetCharactorImage(string name, string imageName)
        {
            var charaAndImage = CharactorImages.Find(c => c.Item1.Name == name);
            charaAndImage.Item2.SetImageSource(charaAndImage.Item1.ImageMap[imageName]);
        }

    }
}