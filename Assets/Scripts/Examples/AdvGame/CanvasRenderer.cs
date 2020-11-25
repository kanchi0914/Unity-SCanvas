using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Examples.AdvGame.Objects;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Examples.AdvGame
{
    /// <summary>
    /// モデルとビューの仲介をするシングルトンクラス
    /// </summary>
    public sealed class CanvasRenderer
    {
        // 現在表示しているシナリオ
        public Scenario Scenario { get; private set; }

        // ビュークラス
        private EGCanvas backGroundImageCanvas;
        private EGCanvas CharacterImageCanvas;
        private EgHorizontalLayoutView characterImagesLayoutView;
        private EGCanvas messageWindowCanvas;
        public AdvMessageWindow AdvMessageWindow;
        public EGGameObject BackgroundImage { get; set; }

        public List<(Character Character, EGGameObject egGameObject)> CharacterImages =
            new List<(Character, EGGameObject)>();

        private static CanvasRenderer instance = new CanvasRenderer();

        public static CanvasRenderer Instance
        {
            get => instance;
        }

        private CanvasRenderer()
        {
        }

        public void SetStartMenu()
        {
            CanvasStack.ClearAll();
            var canvas = new EGCanvas("");
            var backGroundImage = new EGGameObject(canvas);
            Utils.SetBackgroundImage(backGroundImage, "Images/school");

            var titleText = new EGText(canvas, "たのしいアドベンチャーゲーム")
                    .SetCharacter(font: GUIData.GenjuGothicBold, fontSize: 48)
                    .SetMiddleCenterAnchor()
                    .SetRectSizeByRatio(0.8f, 0.2f)
                    .SetLocalPosByRatio(0, -0.25f)
                as EGText;
            var menus = new EGVerticalLayoutView(canvas, isAutoSizingWidth: true)
                .SetMiddleCenterAnchor()
                .SetRectSizeByRatio(0.40f, 0.4f)
                .SetLocalPosByRatio(0, 0.25f);
            var newGameText = new EGText(menus, "ニューゲーム").SetTextPreset(GUIData.TopMenuText)
                .SetPointerEnteredTextColor(Color.white);
            var loadGameText = new EGText(menus, "コンティニュー").SetTextPreset(GUIData.TopMenuText)
                .SetPointerEnteredTextColor(Color.white);
            var optionGameText = new EGText(menus, "オプション").SetTextPreset(GUIData.TopMenuText)
                .SetPointerEnteredTextColor(Color.white);
            newGameText.AddOnClick(() =>
            {
                CanvasStack.ClearAll();
                new Scenario0_Morning().LoadScript();
            });
            loadGameText.AddOnClick(() => new LoadMenu());
            optionGameText.AddOnClick(() => new OptionMenu());
        }

        public void SetNewScenario(Scenario scenario)
        {
            Scenario = scenario;
            Init(Scenario);
        }

        private void Init(Scenario scenario)
        {
            CanvasStack.ClearAll();
            Scenario = scenario;
            CharacterImages = new List<(Character, EGGameObject)>();
            backGroundImageCanvas = new EGCanvas("BackGroundImageCanvas");
            CharacterImageCanvas = new EGCanvas("CharacterImageCanvas");
            characterImagesLayoutView = new EgHorizontalLayoutView(CharacterImageCanvas, isAutoAlignment: true)
                .SetRectSizeByRatio(1, 1) as EgHorizontalLayoutView;
            characterImagesLayoutView.SetChildAliments(TextAnchor.MiddleCenter);
            messageWindowCanvas = new EGCanvas("messageWindowCanvas");
            AdvMessageWindow = new AdvMessageWindow(messageWindowCanvas);
        }

        public void CloseMessageWindow()
        {
            AdvMessageWindow.DestroySelf();
        }


        public void ShowOptionsWindow(List<Option> options)
        {
            var optionWindow = new OptionsWindow(AdvMessageWindow);
            options.ForEach(it => { optionWindow.AddOption(it); });
        }

        public void ShowOptionsWindow(params Option[] options)
        {
            var optionWindow = new OptionsWindow(AdvMessageWindow);
            options.ToList().ForEach(it => { optionWindow.AddOption(it); });
        }

        public void AddOption(Option option)
        {
        }

        public void SetTalkerText(string text)
        {
            AdvMessageWindow.SetTalkerText(text);
        }

        public void SetMessageText(string text)
        {
            AdvMessageWindow.SetMessageText(text);
        }

        public void Load(SaveData saveData, string sceneId, int sectionNumber)
        {
            Type type = GetTypeByClassName(saveData.ScenarioName);
            Scenario scenario = Activator.CreateInstance(type) as Scenario;
            GameData.SelectedOptions = new HashSet<string>();
            scenario.LoadScript(sceneId, sectionNumber);
        }
        
        public static Type GetTypeByClassName(string className)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Name == className)
                    {
                        return type;
                    }
                }
            }
            return null;
        }

        public void SetBackgroundImage(string imageName)
        {
            BackgroundImage?.DestroySelf();
            BackgroundImage = new EGGameObject(backGroundImageCanvas);
            var image = BackgroundImage;
            image.SetImageSprite(BackgroundImageData.Images[imageName]);
            image.gameObject.GetImageComponent().SetNativeSize();
            image.SetMiddleCenterAnchor().SetLocalPos(0, 0);
            var asfitter = image.gameObject.AddComponent<AspectRatioFitter>();
            asfitter.aspectRatio = image.RectSize.x / image.RectSize.y;
            asfitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
        }

        //
        public void AddCharacter(string name, string imageName)
        {
            var chara = CharactorData.Characters.Find(c => c.Name == name);
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
                    c.Item2.DestroySelf();
                    ;
                }
            });
            CharacterImages.RemoveAll(c => c.Item1.Name == name);
        }

        public void SetCharacterImage(string name, string imageName)
        {
            var charaAndImage = CharacterImages.Find(c => c.Item1.Name == name);
            Debug.Log(CharacterImages.Count);
            Debug.Log(charaAndImage.Character);
            Debug.Log(charaAndImage.egGameObject);
            if (charaAndImage.egGameObject == null)
            {
                AddCharacter(name, imageName);
                return;
            }
            charaAndImage.egGameObject.SetImageSprite(charaAndImage.Character.ImageMap[imageName]);
        }
    }
}