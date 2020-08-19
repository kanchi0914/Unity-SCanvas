using System;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame.Objects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Scenario0_Morning : Scenario
    {
        public Scenario0_Morning ()
        {
            InitScripts();
            SetBackGroundImage("Images/bg_outside_jutaku");
        }

        private void InitScripts()
        {
            var script1 = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("ヒロシ", "あー　今日もいい天気だ"),
                new EgMessageWindow.Section("ヒロシ", "朝ご飯をたべよう"),
                new EgMessageWindow.Section("ヒロシ", "パクパク　もぐもぐ"),
                new EgMessageWindow.Section("？？？", "やばいよ～　遅刻遅刻"),
                new EgMessageWindow.Section("ヒロシ", "ん？　この声は…"),
                new EgMessageWindow.Section("ヒロ子", "早くしないと学校間に合わないよ～～～～", 
                    () => AddCharactor("ヒロ子", "running")),
                new EgMessageWindow.Section("ヒロシ", "なんだ　誰かと思えばお前か…"),
                new EgMessageWindow.Section("ヒロ子", "あっヒロシ　おはよー　\n ていうか急がなきゃ遅刻しちゃうよ～", 
                    () => SetCharactorImage("ヒロ子", "normal")),
                new EgMessageWindow.Section("ヒロシ", "おいおい…　時計をよく見てみろよ", null),
                new EgMessageWindow.Section("ヒロ子", "えっ？　‥‥‥‥‥‥‥‥。", 
                    () => SetCharactorImage("ヒロ子", "sumaho")),
                new EgMessageWindow.Section("ヒロ子", "うわ～時間勘違いしてたよ～～ラッキー～～～", 
                    () => SetCharactorImage("ヒロ子", "joy")),
                new EgMessageWindow.Section("ヒロシ", "お前は本当にうっかりやだな～～　そんなんだと嫁にいけないぞ"),
                new EgMessageWindow.Section("ヒロ子", "えっ…　そ　それは困るなぁ…", 
                () => SetCharactorImage("ヒロ子", "tere")),
                new EgMessageWindow.Section("ヒロシ", "なに動揺してんだよ…　ほら早く学校行くぞ"),
                new EgMessageWindow.Section("", "", () => new Scenario1_School().Load("intro", 0))
            };
            Scripts.Add("intro", script1);
        }

    }
}