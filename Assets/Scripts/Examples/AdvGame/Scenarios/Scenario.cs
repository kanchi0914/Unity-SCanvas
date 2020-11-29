using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Examples.AdvGame.GameObjects;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;

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
            Init();
            InitScripts();
        }
        
        private void Init(){
            CanvasStack.ClearAll();
            backGroundImageCanvas = new EGCanvas("backGroundImageCanvas");
            CharacterImageCanvas = new EGCanvas("CharacterImageCanvas");
            characterImagesLayoutView = new EgHorizontalLayoutView(CharacterImageCanvas, isAutoAlignment:true)
                .SetRelativeSize(1,1) as EgHorizontalLayoutView;
            characterImagesLayoutView.LayoutComponent.childAlignment = TextAnchor.MiddleCenter;
            messageWindowCanvas = new EGCanvas("messageWindowCanvas");
            AdvMessageWindow = new AdvMessageWindow(messageWindowCanvas, this);
        }

        protected virtual void InitScripts()
        {
            
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
        
        public void SetBackGroundImage(string imageName)
        {
            BackgroundImage?.DestroySelf();
            BackgroundImage = new EGGameObject(backGroundImageCanvas);
            var image = BackgroundImageData.Images[imageName];
            BackgroundImage.SetImage(image);
            Utils.SetImageAsBackground(BackgroundImage);
        }
        
        public void AddCharacter(string name, string imageName)
        {
            var chara = CharacterData.Characters.Find(c => c.Name == name);
            var image = new EGGameObject(characterImagesLayoutView);
            var source = chara.ImageMap[imageName];
            image.SetImage(source);
            SetImageAspectRatio(image, source);
            CharacterImages.Add((chara, image));
        }
        
        public void SetCharacterImage(string name, string imageName)
        {
            var charaAndImage = CharacterImages.Find(c => c.Item1.Name == name);
            if (charaAndImage.egGameObject == null)
            {
                AddCharacter(name, imageName); 
                return;
            }
            var image = charaAndImage.Character.ImageMap[imageName];
            SetImageAspectRatio(charaAndImage.egGameObject, image);
            charaAndImage.egGameObject.SetImage(charaAndImage.Character.ImageMap[imageName]);
        }

        private void SetImageAspectRatio(EGGameObject imageObject, Sprite source)
        {
            var ratio = source.bounds.size.x / source.bounds.size.y;
            Debug.Log($"{source.name}, x{source.bounds.size.x}, y{source.bounds.size.y}");
            var fitter = imageObject.gameObject.GetOrAddComponent<AspectRatioFitter>();
            fitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            fitter.aspectRatio = ratio;
            imageObject.SetRelativeSize(.0f, .9f, false);
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



    }
}