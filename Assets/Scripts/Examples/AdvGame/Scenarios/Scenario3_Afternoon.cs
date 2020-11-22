using System;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame.Objects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Scenario4_Afternoon : Scenario
    {
        public Scenario4_Afternoon()
        {
            InitScripts();
            SetBackGroundImage("Images/bg_school_room_yuyake");
        }

        private void InitScripts()
        {
            var intro = new List<Section>()
            {
                new Section("", "　～放課後～"),
                new Section("ヒロ子", "ねーもういいでしょ　ヒロシくん\n犯人教えてよ", () => SetCharacterImage("ヒロ子", "normal")),
                new Section("ヒロシ", "わかったよ　それじゃ言うぞ\n犯人は‥‥",                     () =>
                {
                    var optionWindow = new OptionsWindow(AdvMessageWindow);
                    optionWindow.AddOption("ヒロ子だ", () => Load("choose_hiroko"));
                    optionWindow.AddOption("俺だ", () => Load("choose_myself"));
                    optionWindow.AddOption("斎藤さん自身だ", () => Load("choose_saito"));
                    optionWindow.AddOption("猫だ", () => Load("choose_cat",0));
                }),
            };
            Scripts.Add("intro", intro);
            
            var choose_hiroko = new List<Section>()
            {
                new Section("ヒロシ", "ヒロ子だ"),
                new Section("ヒロ子", "えっ！？", () => SetCharacterImage("ヒロ子", "running")),
                new Section("ヒロシ", "お前は昔から　がめついところがあるだろ\n金目のものを見て　無意識のうちに　手が出てしまったんだろうな‥‥"),
                new Section("ヒロ子", "そんな‥‥　まったく覚えがないけど　ヒロシくんが言うならそうなのかも‥‥",
                    () => SetCharacterImage("ヒロ子", "depressed")),
                new Section("ヒロ子", "警察に行って　自首してくるよ‥‥\n今までありがとう　ヒロシくん",
                    () => SetCharacterImage("ヒロ子", "depressed")),
                new Section("", "その後　ヒロ子は退学処分となり　俺はやりきれない気持ちのまま　残りの学生生活を過ごすのだった‥‥"),
                new Section("", "～BAD END～"),
                new Section("", "", () =>
                {
                    CanvasStack.ClearAll();
                    new AdvGameOpening();
                }),
            };
            Scripts.Add("choose_hiroko", choose_hiroko);
            
            var choose_myself = new List<Section>()
            {
                new Section("ヒロシ", "俺だ"),
                new Section("ヒロ子", "えっ！？", () => SetCharacterImage("ヒロ子", "running")),
                new Section("ヒロシ", "悪いなヒロ子　金目のものを見て　つい魔が差してしまったんだ"),
                new Section("ヒロ子", "そんな‥‥　ヒロシくん　信じてたのに‥‥",
                    () => SetCharacterImage("ヒロ子", "depressed")),
                new Section("ヒロシ", "警察に行って自首してくるよ\n今までありがとな　ヒロ子"),
                new Section("", "その後　俺は学校を退学し　失った青春は二度と帰ってこないのであった‥‥"),
                new Section("", "～BAD END～"),
                new Section("", "", () =>
                {
                    CanvasStack.ClearAll();
                    new AdvGameOpening();
                }),
            };
            Scripts.Add("choose_myself", choose_myself);
            
            var choose_saito = new List<Section>()
            {
                new Section("ヒロシ", "斎藤さん自身だ"),
                new Section("ヒロ子", "えっ！？", () => SetCharacterImage("ヒロ子", "running")),
                new Section("ヒロシ", "学校で物が盗まれる事件　知ってるだろ？\nあれの犯人は斎藤さんで　容疑者から自分を外すためにやったんだ"),
                new Section("ヒロ子", "たしかに　筋は通ってるけど‥‥\nでも証拠とかは‥‥",
                    () => SetCharacterImage("ヒロ子", "normal")),
                new Section("ヒロシ", "特にない　だが俺の直感がそう言っているんだ"),
                new Section("ヒロ子", "わかんないけど　ヒロシくんが言うならそうなのかも‥‥\nじゃあ斎藤さんのところへ行こうよ",
                    () => SetCharacterImage("ヒロ子", "running")),
                new Section("", "俺たちは斎藤さんに会いに行った\n自白するように促すと　斎藤さんはマジ切れしたあと泣き出した\nどうやら違ったらしい‥‥"),
                new Section("", "その後　俺は学校中の人間から白い目で見られ　耐えきれず転校することになった\nヒロ子とはその後　会っていない‥‥"),
                new Section("", "～BAD END～"),
                new Section("", "", () =>
                {
                    CanvasStack.ClearAll();
                    new AdvGameOpening();
                }),
            };
            Scripts.Add("choose_saito", choose_saito);
            
            var choose_cat = new List<Section>()
            {
                new Section("ヒロシ", "猫だ"),
                new Section("ヒロ子", "えっ！？", () => SetCharacterImage("ヒロ子", "running")),
                new Section("ヒロシ", "状況を整理して考えてみよう"),
                new Section("ヒロシ", "まず　犯人が財布を盗んだ後　どこから逃げたか考えてみよう\n斎藤さんは入り口側にいたし　入口側から逃げたとは考えづらい"),
                new Section("ヒロシ", "となると　窓から逃げたことが考えられる　斎藤さんの席は窓際だったし　窓は開いていたからな\n" +
                                   "だが　教室は3階だし　人間じゃそれは不可能だ"),
                new Section("ヒロ子", "ふむふむ", () => SetCharacterImage("ヒロ子", "normal")),
                new Section("ヒロシ", "ここで　BBSで見た猫の事を思い出そう\n逃げた猫‥‥ミケは光っているものが好きだったし　斎藤さんの財布は派手なものだったらしいし\n" +
                                   "ということは‥‥"),
                new Section("ヒロ子", "猫が財布を取って　窓から逃げたんだ！",
                    () => SetCharacterImage("ヒロ子", "smile")),
                new Section("ヒロシ", "ああ　ミケは大きい猫らしいし　財布をくわえて逃げることは難しくないはずだ\nどうだ　筋が通ってるだろ？"),
                new Section("ヒロ子", "すごいよ～ヒロシくん　さすが名探偵\nでもどうやって　ミケを捕まえようか？",
                    () => SetCharacterImage("ヒロ子", "smile")),
                new Section("ヒロシ", "それなら心配はいらない\nヒロ子　小銭持ってるか？"),
                new Section("ヒロ子", "えっ？あるにはあるけど‥‥",
                    () => SetCharacterImage("ヒロ子", "normal")),
                new Section("", "", () =>
                {
                    new Scenario4_AfternoonDote();
                }),
            };
            Scripts.Add("choose_cat", choose_cat);

        }
    }
}