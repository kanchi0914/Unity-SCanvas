using System;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame.GameObjects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Scenario0_Morning : Scenario
    {
        public Scenario0_Morning ()
        {
            SetBackGroundImage("bg_outside_jutaku");
        }

        protected override void InitScripts()
        {
            var script1 = new List<Section>()
            {
                new Section("ヒロシ", "あー　今日もいい天気だ"),
                new Section("ヒロシ", "朝ご飯をたべよう"),
                new Section("ヒロシ", "パクパク　もぐもぐ"),
                new Section("？？？", "やばいよ～　遅刻遅刻"),
                new Section("ヒロシ", "ん？　この声は…"),
                new Section("ヒロ子", "早くしないと学校間に合わないよ～～～～", 
                    () => AddCharacter("ヒロ子", "running")),
                new Section("ヒロシ", "なんだ　誰かと思えばお前か…"),
                new Section("ヒロ子", "あっヒロシくん　おはよー　\n ていうか急がなきゃ遅刻しちゃうよ～", 
                    () => SetCharacterImage("ヒロ子", "normal")),
                new Section("ヒロシ", "おいおい…　時計をよく見てみろよ", null),
                new Section("ヒロ子", "えっ？　‥‥‥‥‥‥‥‥。", 
                    () => SetCharacterImage("ヒロ子", "sumaho")),
                new Section("ヒロ子", "うわ～時間勘違いしてたよ～～ラッキー～～～", 
                    () => SetCharacterImage("ヒロ子", "joy")),
                new Section("ヒロシ", "お前は本当にうっかりやだな～～　そんなんだと嫁にいけないぞ"),
                new Section("ヒロ子", "えっ…　そ　それは困るなぁ…", 
                () => SetCharacterImage("ヒロ子", "tere")),
                new Section("ヒロシ", "なに動揺してんだよ…　ほら早く学校行くぞ"),
                new Section("", "", () => new Scenario1_School().LoadScript())
            };
            Scripts.Add("intro", script1);
        }

    }
}