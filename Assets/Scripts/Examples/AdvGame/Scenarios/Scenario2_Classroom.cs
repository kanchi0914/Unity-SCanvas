using System;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame.Objects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Scenario2_Classroom : Scenario
    {
        public Scenario2_Classroom()
        {
            InitScripts();
            SetBackGroundImage("Images/bg_school_room");
        }

        private void InitScripts()
        {
            var intro = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("", "俺たちは事件が起こったという2組の教室にやってきた"),
                new EgMessageWindow.Section("", "", () => Load("options_what_to_do", 0))
            };
            Scripts.Add("intro", intro);
            // 
            var options_what_to_do = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("", "何をしようかな？",
                    () =>
                    {
                        var optionWindow = new OptionWindow(AdvMessageWindow);
                        optionWindow.AddOption("周りの人に話を聞く", () => Load("ask_girl",0));
                        optionWindow.AddOption("席を調べる", () =>
                        {
                            if (GameData.HasAskedAboutSeat) Load("check_seat_with_info", 0);
                            else Load("check_seat_without_info", 0);
                        });
                        optionWindow.AddOption("別の場所へ行く", () => new Scenario1_School().Load("options_where_to_go",0));
                    })
            };
            Scripts.Add("options_what_to_do", options_what_to_do);
            // 「協力しない」
            var ask_girl = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("", "(とりあえず、周りの人に話を聞いてみるか)"),
                new EgMessageWindow.Section("", "(今話しかけられそうなのは　あの女の子だけか…)",
                    () => AddCharactor("女子生徒", "normal")),
                new EgMessageWindow.Section("ヒロシ", "ごめん　ちょっと今いいかな"),
                new EgMessageWindow.Section("女子生徒", "なに～？　ナンパならお断りだけど～"),
                new EgMessageWindow.Section("ヒロ子", "ヒロシくんはナンパなんかしません！！",
                    () => AddCharactor("ヒロ子", "depressed")),
                new EgMessageWindow.Section("ヒロシ", "おまえは少し黙っててくれ‥‥"),
                new EgMessageWindow.Section("ヒロシ", "このクラスで起こったらしい　財布盗難事件について聞きたいんだけど",
                    () => RemoveCharactor("ヒロ子")),
                new EgMessageWindow.Section("女子生徒", "あ～あれね　マジかわいそーってカンジだけど" +
                                                    "\nアタシそんなよく知らないかんね　あんま期待しないでね～"),
                new EgMessageWindow.Section("", "",
                    () => Load("options_what_to_ask",0))
            };
            Scripts.Add("ask_girl", ask_girl);
            
            var options_what_to_ask = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("", "何を聞こうか？",
                    () =>
                    {
                        var optionWindow = new OptionWindow(AdvMessageWindow);
                        optionWindow.AddOption("斎藤さんの特徴について", () => Load("asked_about_saito",0));
                        optionWindow.AddOption("財布の特徴について", () => Load("asked_about_wallet",0));
                        optionWindow.AddOption("斎藤さんの席について", () => Load("asked_about_seat",0));
                        optionWindow.AddOption("やめる", () =>
                        {
                            Load("options_what_to_do", 0);
                            RemoveCharactor("女子生徒");
                        });
                    })
            };
            Scripts.Add("options_what_to_ask", options_what_to_ask);
            
            // 斎藤さんの特徴について
            var asked_about_saito = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("ヒロシ", "斎藤さんについて、何かわかることはない？"),
                new EgMessageWindow.Section("女子生徒", "ん～あんま仲良くないからわかんないけど…" +
                                                    "\nでもそんな悪い子に見えないけどね～　少なくとも　誰かからウラミ買うようなタイプではないかな～"),
                new EgMessageWindow.Section("ヒロシ", "じゃあ別に　いじめられてた‥‥ってわけでも？"),
                new EgMessageWindow.Section("女子生徒", "ないない　ウチのクラスめっちゃ仲良しってか～" +
                                                   "\nもじイジメとかあったら　ウチが見逃すはずないし～"),
                new EgMessageWindow.Section("", "",
                    () => Load("options_what_to_ask",0))
            };
            Scripts.Add("asked_about_saito", asked_about_saito);
            
            // 財布の特徴について
            var asked_about_wallet = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("", "盗まれた財布について、何かわかることはない？"),
                new EgMessageWindow.Section("女子生徒", "あー　それは覚えてるよ" +
                                                    "\nなんつーか　デコ財布っていうの？　めっちゃラメとかキラキラしてて" +
                                                    "\nとにかく目立つ財布だったね～アレは"),
                new EgMessageWindow.Section("女子生徒", "本人　見た目地味なのに" +
                                                    "\n趣味はイガイと派手なんだね～" +
                                                    "\nあ　それは言い過ぎか？ごめんね～"),
                new EgMessageWindow.Section("ヒロシ", "(なるほど‥‥目立つ財布か)"),
                new EgMessageWindow.Section("", "",
                    () => Load("options_what_to_ask",0))
            };
            Scripts.Add("asked_about_wallet", asked_about_wallet);
            
            // 斎藤さんの席について
            var asked_about_seat = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("", "盗まれた財布について、何かわかることはない？"),
                new EgMessageWindow.Section("女子生徒", "あ～はいはい　窓際の一番奥の席だよ～"),
                new EgMessageWindow.Section("ヒロシ", "(窓際の一番奥の席‥と)",
                    () => GameData.HasAskedAboutSeat = true),
                new EgMessageWindow.Section("", "",
                    () => Load("options_what_to_ask",0))
            };
            Scripts.Add("asked_about_seat", asked_about_seat);
            
            // 席を調べる　
            var check_seat_with_info = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("ヒロシ", "窓際の一番奥の席‥‥あれだな"),
                new EgMessageWindow.Section("ヒロ子", "ん～　とくに変わったところはなさそうだね‥‥" +
                                                   "\nあっ！", () => AddCharactor("ヒロ子", "normal")),
                new EgMessageWindow.Section("ヒロシ", "どうした？"),
                new EgMessageWindow.Section("ヒロ子", "見て！　窓が全開になってる…" +
                                                   "\n犯人はここから逃げたんだよ！間違いない！", 
                    () => SetCharactorImage("ヒロ子", "joy")),
                new EgMessageWindow.Section("ヒロシ", "バカ言え‥ここは3階だぞ" +
                                                   "\n人間じゃとても無理だろ"),
                new EgMessageWindow.Section("ヒロシ", "(とはいえ　窓が開いてるのは気になるな‥‥？)"),
                new EgMessageWindow.Section("", "",
                    () => Load("options_what_to_do",0))
            };
            Scripts.Add("check_seat_with_info", check_seat_with_info);
            
            // 席を調べる(情報フラグなし)
            var check_seat_without_info = new List<EgMessageWindow.Section>()
            {
                new EgMessageWindow.Section("ヒロシ", "斎藤さんの席を調べたいけど、場所がわからないな…"),
                new EgMessageWindow.Section("", "",
                    () => Load("options_what_to_do",0))
            };
            Scripts.Add("check_seat_without_info", check_seat_without_info);

        }
    }
}