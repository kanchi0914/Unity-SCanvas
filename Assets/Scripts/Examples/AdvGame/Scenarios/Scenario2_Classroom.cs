using System;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame.GameObjects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Scenario2_Classroom : Scenario
    {
        public Scenario2_Classroom()
        {
            SetBackGroundImage("bg_school_room");
        }

        protected override void InitScripts()
        {
            var intro = new List<Section>()
            {
                new Section("", "俺たちは事件が起こったという2組の教室にやってきた"),
                new Section("", "", () => LoadScript("options_what_to_do"))
            };
            Scripts.Add("intro", intro);
            // 
            var options_what_to_do = new List<Section>()
            {
                new Section("", "何をしようかな？",
                    () =>
                    {
                        RemoveAllCharacters();
                        var optionWindow = new OptionsWindow(AdvMessageWindow);
                        optionWindow.AddOption(
                            new Option($"{ScenarioName}_ask_girl", "周りの人に話を聞く", () => LoadScript("ask_girl")));
                        if (GameData.SelectedOptions.Contains($"{ScenarioName}_ask_about_seat"))
                        {
                            optionWindow.AddOption(new Option($"{ScenarioName}_check_seat_with_info", "席を調べる",
                                () =>
                                {
                                    // GameData.HasAskedAboutSeat = true;
                                    LoadScript("check_seat_with_info");
                                }));
                        }
                        else
                        {
                            optionWindow.AddOption(new Option($"{ScenarioName}_check_seat_without_info", "席を調べる",
                                () => LoadScript("check_seat_without_info")));
                        }
                        optionWindow.AddOption(new Option($"{ScenarioName}_options_where_to_go", "別の場所へ行く",
                            () => new Scenario1_School().LoadScript("options_where_to_go")));
                    })
            };
            Scripts.Add("options_what_to_do", options_what_to_do);
            // 「協力しない」
            var ask_girl = new List<Section>()
            {
                new Section("ヒロシ", "(とりあえず、周りの人に話を聞いてみるか)"),
                new Section("ヒロシ", "(今話しかけられそうなのは　あの女の子だけか…)",
                    () => AddCharacter("女子生徒", "normal")),
                new Section("ヒロシ", "ごめん　ちょっと今いいかな"),
                new Section("女子生徒", "なに～？　ナンパならお断りだけど～"),
                new Section("ヒロ子", "ヒロシくんはナンパなんかしません！！",
                    () => AddCharacter("ヒロ子", "depressed")),
                new Section("ヒロシ", "おまえは少し黙っててくれ‥‥"),
                new Section("ヒロシ", "このクラスで起こったらしい　財布盗難事件について聞きたいんだけど",
                    () => RemoveCharacter("ヒロ子")),
                new Section("女子生徒", "あ～あれね　マジかわいそーってカンジだけど" +
                                    "\nアタシそんなよく知らないかんね　あんま期待しないでね～"),
                new Section("", "",
                    () => LoadScript("options_what_to_ask"))
            };
            Scripts.Add("ask_girl", ask_girl);

            var options_what_to_ask = new List<Section>()
            {
                new Section("ヒロシ", "何を聞こうか？",
                    () =>
                    {
                        var optionWindow = new OptionsWindow(AdvMessageWindow);
                        optionWindow.AddOption(new Option($"{ScenarioName}_ask_about_saito", "斎藤さんの特徴について",
                            () => LoadScript("ask_about_saito")));
                        optionWindow.AddOption(new Option($"{ScenarioName}_ask_about_wallet", "財布の特徴について",
                            () => LoadScript("ask_about_wallet")));
                        optionWindow.AddOption(new Option($"{ScenarioName}_ask_about_seat", "斎藤さんの席について",
                            () => LoadScript("ask_about_seat")));
                        optionWindow.AddOption("やめる", () => { LoadScript("options_what_to_do"); });
                    })
            };
            Scripts.Add("options_what_to_ask", options_what_to_ask);

            // 斎藤さんの特徴について
            var ask_about_saito = new List<Section>()
            {
                new Section("ヒロシ", "斎藤さんについて、何かわかることはない？"),
                new Section("女子生徒", "ん～あんま仲良くないからわかんないけど…" +
                                    "\nでもそんな悪い子に見えないけどね～　少なくとも　誰かからウラミ買うようなタイプではないかな～"),
                new Section("ヒロシ", "じゃあ別に　いじめられてた‥‥ってわけでも？"),
                new Section("女子生徒", "ないない　ウチのクラスめっちゃ仲良しってか～" +
                                    "\nもじイジメとかあったら　ウチが見逃すはずないし～"),
                new Section("", "",
                    () => LoadScript("options_what_to_ask"))
            };
            Scripts.Add("ask_about_saito", ask_about_saito);

            // 財布の特徴について
            var ask_about_wallet = new List<Section>()
            {
                new Section("ヒロシ", "盗まれた財布について、何かわかることはない？"),
                new Section("女子生徒", "あー　それは覚えてるよ" +
                                    "\nなんつーか　デコ財布っていうの？　めっちゃラメとかキラキラしてて" +
                                    "\nとにかく目立つ財布だったね～アレは"),
                new Section("女子生徒", "本人　見た目地味なのに" +
                                    "\n趣味はイガイと派手なんだね～" +
                                    "\nあ　それは言い過ぎか？ごめんね～"),
                new Section("ヒロシ", "(なるほど‥‥目立つ財布か)"),
                new Section("", "",
                    () => LoadScript("options_what_to_ask"))
            };
            Scripts.Add("ask_about_wallet", ask_about_wallet);

            // 斎藤さんの席について
            var ask_about_seat = new List<Section>()
            {
                new Section("ヒロシ", "斎藤さんの席って　どこかわかる？"),
                new Section("女子生徒", "あ～はいはい　窓際の一番奥の席だよ～"),
                new Section("ヒロシ", "(窓際の一番奥の席‥と)"),
                new Section("", "",
                    () => LoadScript("options_what_to_ask"))
            };
            Scripts.Add("ask_about_seat", ask_about_seat);

            // 席を調べる　
            var check_seat_with_info = new List<Section>()
            {
                new Section("ヒロシ", "窓際の一番奥の席‥‥あれだな"),
                new Section("ヒロ子", "ん～　とくに変わったところはなさそうだね‥‥" +
                                   "\nあっ！", () => AddCharacter("ヒロ子", "normal")),
                new Section("ヒロシ", "どうした？"),
                new Section("ヒロ子", "見て！　窓が全開になってる…" +
                                   "\n犯人はここから逃げたんだよ！間違いない！",
                    () => SetCharacterImage("ヒロ子", "joy")),
                new Section("ヒロシ", "バカ言え‥ここは3階だぞ" +
                                   "\n人間じゃとても無理だろ"),
                new Section("ヒロシ", "(とはいえ　窓が開いてるのは気になるな‥‥？)"),
                new Section("", "",
                    () => LoadScript("options_what_to_do"))
            };
            Scripts.Add("check_seat_with_info", check_seat_with_info);

            // 席を調べる(情報フラグなし)
            var check_seat_without_info = new List<Section>()
            {
                new Section("ヒロシ", "斎藤さんの席を調べたいけど、場所がわからないな…"),
                new Section("", "",
                    () => LoadScript("options_what_to_do"))
            };
            Scripts.Add("check_seat_without_info", check_seat_without_info);
        }
    }
}