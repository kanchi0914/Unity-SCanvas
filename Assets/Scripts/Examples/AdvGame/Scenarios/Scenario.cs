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
    /// <summary>
    /// シナリオのモデルクラス
    /// </summary>
    public class Scenario
    {
        public Dictionary<string, List<Section>> Scripts =
            new Dictionary<string, List<Section>>();
        
        // 現在再生中のスクリプト
        public List<Section> CurrentScript = new List<Section>();
        // 現在再生中のスクリプトID(セーブデータに必要)
        public string CurrentScriptId;
        // 現在のスクリプト中、何番目のセクションを表示しているか
        public int SectionNumber = -1;
        // ログ表示に必要な部分
        public List<Section> SentSections = new List<Section>();

        public string ScenarioName => GetType().Name;
        
        public List<(Character Character, EGGameObject egGameObject)> CharacterImages = new List<(Character, EGGameObject)>();
        
        // ビュークラス
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
            AdvMessageWindow = new AdvMessageWindow(messageWindowCanvas, this);
        }

        // メッセージ送り
        public void SendSection()
        {
            SectionNumber++;
            // スクリプト中の全セクションを表示し終わった
            if (SectionNumber >= CurrentScript.Count)
            {
                AdvMessageWindow.DestroySelf();
                return;
            }
            var section = CurrentScript[SectionNumber];
            SentSections.Add(section);
            AdvMessageWindow.SetTalkerText(section.Talker);
            AdvMessageWindow.SetMessageText(section.Text);
            section.Action?.Invoke();
        }

        public void LoadScript(string scriptId = null, int sectionNumber = -1)
        {
            SectionNumber = -1;
            if (scriptId == null) scriptId = Scripts.First().Key;
            CurrentScriptId = scriptId;
            CurrentScript = Scripts[scriptId];
            while (sectionNumber >= 0)
            {
                SendSection();
                sectionNumber--;
            }
            SendSection();
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