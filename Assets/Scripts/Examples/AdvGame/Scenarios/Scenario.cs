using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Examples.AdvGame.Objects;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Scenario
    {
        //public List<List<Section>> Scripts = new List<List<Section>>(); 
        public Dictionary<string, List<Section>> Scripts =
            new Dictionary<string, List<Section>>();
        public List<(Character Character, EGGameObject egGameObject)> CharacterImages = new List<(Character, EGGameObject)>();
        private EGCanvas backGroundImageCanvas;
        private EGCanvas CharacterImageCanvas;
        private EgHorizontalLayoutView characterImagesLayoutView;
        private EGCanvas messageWindowCanvas;
        public AdvMessageWindow AdvMessageWindow;

        public EGGameObject BackgroundImage { get; set; }
        public Scenario ()
        {
            CanvasStack.ClearAll();
            Init();
        }
        
        private void Init(){
            backGroundImageCanvas = new EGCanvas("backGroundImageCanvas");
            CharacterImageCanvas = new EGCanvas("CharacterImageCanvas");
            characterImagesLayoutView = new EgHorizontalLayoutView(CharacterImageCanvas, isAutoAlignment:true)
                .SetRectSizeByRatio(1,1) as EgHorizontalLayoutView;
            characterImagesLayoutView.LayoutComponent.childAlignment = TextAnchor.MiddleCenter;
            messageWindowCanvas = new EGCanvas("messageWindowCanvas");
            AdvMessageWindow = new AdvMessageWindow(messageWindowCanvas);
        }

        public void Load(string scriptId = null, int sectionNumber = 0)
        {
            var script =  ((scriptId == null) ? Scripts.First().Value : Scripts[scriptId]);
            script.RemoveRange(0, sectionNumber);
            AdvMessageWindow.SetMessageAndActions(script);
            AdvMessageWindow.gameObject.GetImage().raycastTarget = true;
        }
        
        public void SetBackGroundImage(string imageFilePath)
        {
            BackgroundImage?.DestroySelf();;
            BackgroundImage = new EGGameObject(backGroundImageCanvas);
            Utils.SetBackgroundImage(BackgroundImage, imageFilePath);
        }
        
        public void AddCharacter(string name, string imageName)
        {
            var chara = GameData.Characters.Find(c => c.Name == name);
            var image = new EGGameObject(characterImagesLayoutView);
            image.SetImageSprite(chara.ImageMap[imageName]);
            CharacterImages.Add((chara, image));
            image.SetRectSizeByRatio(0.4f, 0.9f);
        }

        public void RemoveAllCharacters()
        {
            var names = new List<string>();
            CharacterImages.ForEach(i => names.Add(i.Character.Name));
            names.ForEach(RemoveCharacter);
        }

        public void RemoveCharacter(string name)
        {
            CharacterImages.ForEach(c =>
            {
                if (c.Item1.Name == name)
                {
                    c.Item2.DestroySelf();;
                }
            });
            CharacterImages.RemoveAll(c => c.Item1.Name == name);
        }

        public void SetCharacterImage(string name, string imageName)
        {
            var charaAndImage = CharacterImages.Find(c => c.Item1.Name == name);
            if (charaAndImage.egGameObject == null)
            {
                AddCharacter(name, imageName); 
                return;
            }
            charaAndImage.egGameObject.SetImageSprite(charaAndImage.Character.ImageMap[imageName]);
        }

    }
}