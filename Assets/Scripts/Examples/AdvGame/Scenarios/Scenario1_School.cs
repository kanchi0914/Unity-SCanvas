using System;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame.GameObjects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Scenario1_School : Scenario
    {
        public Scenario1_School()
        {
            SetBackGroundImage("bg_school_rouka");
        }

        public void LoadScript(string scriptId = null, int sectionNumber = -1)
        {
            if (GameData.IsAllFlagCompleted)
            {
                base.LoadScript("solved_intro");
            }
            else
            {
                base.LoadScript(scriptId, sectionNumber);
            }
        }

        protected override void InitScripts()
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
                        optionWindow.AddOption(new Option($"{ScenarioName}_selected_cooperate", "ヒロ子に協力する",
                            () => LoadScript("selected_cooperate")));
                        optionWindow.AddOption(new Option($"{ScenarioName}_selected_dont_cooperate", "ヒロ子に協力しない",
                            () => LoadScript("selected_dont_cooperate")));
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
                    LoadScript("options_where_to_go");
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
                    new AdvGameMain();
                }),
            };
            Scripts.Add("selected_dont_cooperate", script3);
            
            var options_where_to_go = new List<Section>()
            {
                new Section("", "どこに行こうか？",
                    () =>
                    {
                        RemoveAllCharacters();
                        var optionWindow = new OptionsWindow(AdvMessageWindow);
                        optionWindow.AddOption(new Option($"{ScenarioName}_goto_saito", "斎藤さんの所",
                            () => LoadScript("goto_saito")));
                        optionWindow.AddOption(new Option($"{ScenarioName}_goto_class2", "2組の教室",
                            () => new Scenario2_Classroom().LoadScript()));
                        optionWindow.AddOption(new Option($"{ScenarioName}_goto_pc_room", "パソコン室",
                            () => new Scenario3_PCroom().LoadScript()));
                    })
            };
            Scripts.Add("options_where_to_go", options_where_to_go);
            
            // 「協力する」
            var goto_saito = new List<Section>()
            {
                new Section("", "まずは斎藤さんの所へ行ってみた\nどうやらすごく落ち込んでいるようだ‥‥", () =>
                {
                    RemoveAllCharacters();
                }),
                new Section("ヒロシ", "あの‥‥斎藤さん\nちょっといいかな"),
                new Section("斎藤さん", "あ‥‥はい　なんでしょうか‥‥",
                    () => SetCharacterImage("斎藤", "normal")),
                new Section("ヒロシ", "財布がなくなったって話聞いたよ\nなにか俺たちにできることはないかと思ってさ\n" +
                                   "なんでもいいから　情報が欲しいんだ", () => LoadScript("options_what_to_ask"))
            };
            
            var options_what_to_ask = new List<Section>()
            {
                new Section("斎藤さん", "答えられること‥‥あまりないとおもいますが‥",
                    () =>
                    {
                        RemoveAllCharacters();
                        SetCharacterImage("斎藤", "normal");
                        var optionWindow = new OptionsWindow(AdvMessageWindow);
                        optionWindow.AddOption(new Option($"{ScenarioName}_ask_about_wallet", "盗まれた財布について聞く",
                            () => LoadScript("ask_about_wallet")));
                        optionWindow.AddOption(new Option($"{ScenarioName}_ask_about_situation", "盗まれた時の状況について聞く",
                            () => LoadScript("ask_about_situation")));
                        optionWindow.AddOption("やめる", () =>
                        {
                            LoadScript("options_where_to_go");
                        });
                    })
            };
            Scripts.Add("options_what_to_ask", options_what_to_ask);
            
            var ask_about_wallet = new List<Section>()
            {
                new Section("ヒロシ", "盗まれた財布って、どんな感じだった？"),
                new Section("斎藤さん", "どんなって‥‥　別に普通です　普通の財布‥‥"),
                new Section("ヒロシ", "そうか‥‥\n(特に情報を引き出せそうにないな‥‥)"),
                new Section("", "", () =>
                {
                    LoadScript("options_what_to_ask");
                }),
            };
            Scripts.Add("ask_about_wallet", ask_about_wallet);
            
            var ask_about_situation = new List<Section>()
            {
                new Section("ヒロシ", "財布がなくなったときの事　思い出せる？"),
                new Section("斎藤さん", "ちょっと目を離しただけなんです　他のクラスの子に呼ばれて\n" +
                                    "教室の入り口で少し話した後　戻ったらなくなってて‥‥"),
                new Section("斎藤さん", "きっとクラスに　私の事が嫌いな人がいるんです\n私もう　怖くてみんなと話せない‥‥"),
                new Section("ヒロ子", "(ヒロシくん‥‥　これはまずいよ　早く何とかしてあげないと～)",
                    () => SetCharacterImage("ヒロ子", "running")),
                new Section("", "", () =>
                {
                    LoadScript("options_what_to_ask");
                }),
            };
            Scripts.Add("ask_about_situation", ask_about_situation);

            Scripts.Add("goto_saito", goto_saito);
            
            var solved_intro = new List<Section>()
            {
                new Section("ヒロ子", "いろいろ調べたけど　結局よくわかんないね～",
                    () => SetCharacterImage("ヒロ子", "normal")),
                new Section("ヒロシ", "そんなことはないぞ　今ある情報で 謎はすべて解けた"), 
                new Section("ヒロ子", "えっ!? すごいよ～　さすがはヒロシくん\nそれで　誰が犯人なの？",
                    () => SetCharacterImage("ヒロ子", "running")),
                new Section("ヒロシ", "授業が始まるし　それはまた後にしよう\n放課後教室でな"),
                new Section("ヒロ子", "え～気になるよ～",
                    () => SetCharacterImage("ヒロ子", "smile")),
                new Section("", "", () => new Scenario4_Afternoon().LoadScript())
            };
            Scripts.Add("solved_intro", solved_intro);
        }
    }
}