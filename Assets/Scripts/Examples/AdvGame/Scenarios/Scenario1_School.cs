using System;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame.Objects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Scenario1_School : Scenario
    {
        public Scenario1_School()
        {
            InitScripts();
            SetBackGroundImage("Images/bg_school_rouka");
        }

        private void InitScripts()
        {
            var script1 = new List<Section>()
            {
                new Section("生徒達", "がやがや…　がやがや…"),
                new Section("ヒロシ", "ん？　なんか騒がしいな…"),
                new Section("ヒロ子", "ほんとだね～どうしたんだろ　\nちょっと聞いてみよっか…",
                    () => AddCharacter("ヒロ子", "normal")),
                new Section("ヒロ子", "あの～すみません　なにかあったんですか？"),
                new Section("男子生徒", "いや実はね　2組の斎藤さんの財布が盗まれたらしいよ\n" +
                                                    "少し目を離した隙になくなってたらしい \nかわいそうに…",
                    () => AddCharacter("男子生徒", "normal")),
                new Section("ヒロ子", "えぇ～～そりゃ大変だ",
                    () => SetCharacterImage("ヒロ子", "running")),
                new Section("ヒロシ", "やれやれ　物騒な事件もあったもんだ"),
                new Section("ヒロ子", "よし！こうしちゃいられない　ヒロシくん",
                    () =>
                    {
                        RemoveCharacter("男子生徒");
                        SetCharacterImage("ヒロ子", "joy");
                    }),
                new Section("ヒロシ", "なんだ？"),
                new Section("ヒロ子", "こーなったら犯人捜しだよ～　人のもの盗むなんて絶対許せない " +
                                                   "じっちゃんの名にかけて解決してやろ～よ　私たちが",
                    () => SetCharacterImage("ヒロ子", "normal")),
                new Section("ヒロシ", "(どうしようかな…？)",
                    () =>
                    {
                        var optionWindow = new OptionsWindow(AdvMessageWindow);
                        optionWindow.AddOption("ヒロ子に協力する", () => Load("selected_cooperate",0));
                        optionWindow.AddOption("ヒロ子に協力しない", () => Load("selected_dont_cooperate",0));
                    })
            };
            Scripts.Add("intro", script1);
            // 「協力する」
            var script2 = new List<Section>()
            {
                new Section("ヒロシ", "しょうがないな　付き合ってやるよ" +
                                                   "\nおまえは昔から　困ってる人を見たら放っておけないもんな"),
                new Section("ヒロ子", "やった！そうこなっくちゃ～～～",
                    () => SetCharacterImage("ヒロ子", "joy")),
                new Section("ヒロ子", "そうと決まればさっそく情報収集だね" +
                                                   "\nあとは任せたよ　探偵さん",
                    () => SetCharacterImage("ヒロ子", "normal")),
                new Section("ヒロシ", "いや　俺が主体になって動くのかよ…" +
                                                   "\nおまえが言い出したくせに‥‥"),
                new Section("", "", () =>
                {
                    Load("options_where_to_go",0);
                }),
            };
            Scripts.Add("selected_cooperate", script2);
            // 「協力しない」
            var script3 = new List<Section>()
            {
                new Section("ヒロシ", "いや…俺はいいかな"),
                new Section("ヒロ子", "えっ",
                    () => SetCharacterImage("ヒロ子", "normal")),
                new Section("ヒロシ", "俺だって忙しいんだよ　受験もそう遠くないし \n" +
                                                   "気持ちはわかるけど　他人にばかりかまってられないよ"),
                new Section("ヒロ子", "‥‥‥‥‥‥。",
                    () => SetCharacterImage("ヒロ子", "depressed")),
                new Section("ヒロシ", "おまえもそろそろ身の振り考えた方がいいよ \n" +
                                                   "いつまでも子供みたいな事やってられないだろ \n" +
                                                   "いい大学行かないと　親にだって‥‥"),
                new Section("ヒロ子", "ヒロシくん"),
                new Section("ヒロシ", "え？"),
                new Section("ヒロ子", "むかし　二人で探偵ごっこした時のこと　覚えてる？" +
                                                   "\nヒロシくんが探偵　私が助手役で…　" +
                                                   "\n二人で日が暮れるまで 町中いろんなとこ　走り回ったよね"),
                new Section("ヒロシ", "‥‥‥‥‥‥。"),
                new Section("ヒロ子", "大きくなってから　私たちほとんど　二人で遊ぶこともなくなったけど‥‥" +
                                                   "\nでも私　あの時のこと　ずっと憶えてるんだ" +
                                                   "\nすごく楽しかった　いい思い出なの　だから‥‥"),
                new Section("ヒロ子", "‥‥ううん　やっぱりなんでもない" +
                                                   "\n ごめんね　私もう行くね"),
                new Section("ヒロ子", "そう言うと　ヒロ子は去って行ってしまった‥‥",
                    () => RemoveCharacter("")),
                new Section("ヒロシ", "(‥‥悪い事したかなぁ)"),
                new Section("", "その後　ヒロ子とは疎遠になり　俺は彼女もできないまま　暗い学校生活を過ごすのであった"),
                new Section("", "～BAD END～"),
                new Section("", "", () =>
                {
                    CanvasStack.ClearAll();
                    new AdvGameOpening();
                }),
            };
            Scripts.Add("selected_dont_cooperate", script3);
            var script4 = new List<Section>()
            {
                new Section("", "どこに行こうか？",
                    () =>
                    {
                        var optionWindow = new OptionsWindow(AdvMessageWindow);
                        optionWindow.AddOption("2組の教室", () => new Scenario2_Classroom().Load("intro", 0));
                        optionWindow.AddOption("パソコン室", () => Load("start", 0));
                    })
            };
            Scripts.Add("options_where_to_go", script4);
        }
    }
}