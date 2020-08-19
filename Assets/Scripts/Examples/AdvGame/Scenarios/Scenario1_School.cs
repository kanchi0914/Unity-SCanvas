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
            var script1 = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("生徒達", "がやがや…　がやがや…"),
                new EgMessageWindow.Section("ヒロシ", "ん？　なんか騒がしいな…"),
                new EgMessageWindow.Section("ヒロ子", "ほんとだね～どうしたんだろ　\nちょっと聞いてみよっか…",
                    () => AddCharactor("ヒロ子", "normal")),
                new EgMessageWindow.Section("ヒロ子", "あの～すみません　なにかあったんですか？"),
                new EgMessageWindow.Section("男子生徒", "いや実はね　2組の斎藤さんの財布が盗まれたらしいよ\n" +
                                                    "少し目を離した隙になくなってたらしい \nかわいそうに…",
                    () => AddCharactor("男子生徒", "normal")),
                new EgMessageWindow.Section("ヒロ子", "えぇ～～そりゃ大変だ",
                    () => SetCharactorImage("ヒロ子", "running")),
                new EgMessageWindow.Section("ヒロシ", "やれやれ　物騒な事件もあったもんだ"),
                new EgMessageWindow.Section("ヒロ子", "よし！こうしちゃいられない　ヒロシくん",
                    () =>
                    {
                        RemoveCharactor("男子生徒");
                        SetCharactorImage("ヒロ子", "joy");
                    }),
                new EgMessageWindow.Section("ヒロシ", "なんだ？"),
                new EgMessageWindow.Section("ヒロ子", "こーなったら犯人捜しだよ～　人のもの盗むなんて絶対許せない " +
                                                   "じっちゃんの名にかけて解決してやろ～よ　私たちが",
                    () => SetCharactorImage("ヒロ子", "normal")),
                new EgMessageWindow.Section("ヒロシ", "(どうしようかな…？)",
                    () =>
                    {
                        var optionWindow = new OptionWindow(AdvMessageWindow);
                        optionWindow.AddOption("ヒロ子に協力する", () => Load("selected_cooperate",0));
                        optionWindow.AddOption("ヒロ子に協力しない", () => Load("selected_dont_cooperate",0));
                    })
            };
            Scripts.Add("intro", script1);
            // 「協力する」
            var script2 = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("ヒロシ", "しょうがないな　付き合ってやるよ" +
                                                   "\nおまえは昔から　困ってる人を見たら放っておけないもんな"),
                new EgMessageWindow.Section("ヒロ子", "やった！そうこなっくちゃ～～～",
                    () => SetCharactorImage("ヒロ子", "joy")),
                new EgMessageWindow.Section("ヒロ子", "そうと決まればさっそく情報収集だね" +
                                                   "\nあとは任せたよ　探偵さん",
                    () => SetCharactorImage("ヒロ子", "normal")),
                new EgMessageWindow.Section("ヒロシ", "いや　俺が主体になって動くのかよ…" +
                                                   "\nおまえが言い出したくせに‥‥"),
                new EgMessageWindow.Section("", "", () =>
                {
                    Load("options_where_to_go",0);
                }),
            };
            Scripts.Add("selected_cooperate", script2);
            // 「協力しない」
            var script3 = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("ヒロシ", "いや…俺はいいかな"),
                new EgMessageWindow.Section("ヒロ子", "えっ",
                    () => SetCharactorImage("ヒロ子", "normal")),
                new EgMessageWindow.Section("ヒロシ", "俺だって忙しいんだよ　受験もそう遠くないし \n" +
                                                   "気持ちはわかるけど　他人にばかりかまってられないよ"),
                new EgMessageWindow.Section("ヒロ子", "‥‥‥‥‥‥。",
                    () => SetCharactorImage("ヒロ子", "depressed")),
                new EgMessageWindow.Section("ヒロシ", "おまえもそろそろ身の振り考えた方がいいよ \n" +
                                                   "いつまでも子供みたいな事やってられないだろ \n" +
                                                   "いい大学行かないと　親にだって‥‥"),
                new EgMessageWindow.Section("ヒロ子", "ヒロシくん"),
                new EgMessageWindow.Section("ヒロシ", "え？"),
                new EgMessageWindow.Section("ヒロ子", "むかし　二人で探偵ごっこした時のこと　覚えてる？" +
                                                   "\nヒロシくんが探偵　私が助手役で…　" +
                                                   "\n二人で日が暮れるまで 町中いろんなとこ　走り回ったよね"),
                new EgMessageWindow.Section("ヒロシ", "‥‥‥‥‥‥。"),
                new EgMessageWindow.Section("ヒロ子", "大きくなってから　私たちほとんど　二人で遊ぶこともなくなったけど‥‥" +
                                                   "\nでも私　あの時のこと　ずっと憶えてるんだ" +
                                                   "\nすごく楽しかった　いい思い出なの　だから‥‥"),
                new EgMessageWindow.Section("ヒロ子", "‥‥ううん　やっぱりなんでもない" +
                                                   "\n ごめんね　私もう行くね"),
                new EgMessageWindow.Section("ヒロ子", "そう言うと　ヒロ子は去って行ってしまった‥‥",
                    () => RemoveCharactor("")),
                new EgMessageWindow.Section("ヒロシ", "(‥‥悪い事したかなぁ)"),
                new EgMessageWindow.Section("", "その後　ヒロ子とは疎遠になり　俺は彼女もできないまま　暗い学校生活を過ごすのであった"),
                new EgMessageWindow.Section("", "～BAD END～"),
                new EgMessageWindow.Section("", "", () =>
                {
                    CanvasStack.ClearAll();
                    new AdvGameOpening();
                }),
            };
            Scripts.Add("selected_dont_cooperate", script3);
            var script4 = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("", "どこに行こうか？",
                    () =>
                    {
                        var optionWindow = new OptionWindow(AdvMessageWindow);
                        optionWindow.AddOption("2組の教室", () => new Scenario2_Classroom().Load("intro", 0));
                        optionWindow.AddOption("パソコン室", () => Load("start", 0));
                    })
            };
            Scripts.Add("options_where_to_go", script4);
        }
    }
}