using System;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame.Objects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Scenario3_PCroom : Scenario
    {
        public Scenario3_PCroom()
        {
            InitScripts();
            SetBackGroundImage("Images/bg_computer_room_hellowork");
        }

        private void InitScripts()
        {
            var intro = new List<Section>()
            {
                new Section("ヒロシ", "PC室にやってきたぞ"),
                new Section("ヒロ子", "ヒロシくん　ここで何する気～？", () => SetCharacterImage("ヒロ子", "normal")),
                new Section("ヒロ子", "まさか　エッチな画像を検索する気じゃ‥‥", () => SetCharacterImage("ヒロ子", "tere")),
                new Section("ヒロシ", "バカだな　そんなわけないだろ\nお前じゃあるまいし"),
                new Section("ヒロ子", "わたしはそんなことしませ～～ん！！", () => SetCharacterImage("ヒロ子", "joy")),
                new Section("ヒロシ", "なんでちょっと嬉しそうなんだよ‥‥"),
                new Section("ヒロシ", "この学校のBBSがネットにあるんだよ　何か情報が得られるかもしれないと思ってな"),
                new Section("ヒロ子", "へ～知らなかった　早速調べてみよーよ", () => SetCharacterImage("ヒロ子", "normal")),
                new Section("", "", () => LoadScript("options_what_to_do"))
            };
            Scripts.Add("intro", intro);
            // 
            var options_what_to_do = new List<Section>()
            {
                new Section("", "学校のBBSだ。いくつかスレッドがあるみたいだが‥‥",
                    () =>
                    {
                        RemoveAllCharacters();
                        var optionWindow = new OptionsWindow(AdvMessageWindow);
                        optionWindow.AddOption(new Option($"{ScenarioName}_search_about_incident", "「校内で発生中の連続盗難事件」",
                            () => LoadScript("search_about_incident")));
                        optionWindow.AddOption(new Option($"{ScenarioName}_search_about_girl", "「校内美少女ランキングを作成するスレ」",
                            () => LoadScript("search_about_girl")));
                        optionWindow.AddOption(new Option($"{ScenarioName}_search_about_cat", "「飼い猫を探しています」",
                            () => LoadScript("search_about_cat")));
                        optionWindow.AddOption(new Option($"{ScenarioName}_options_where_to_go", "別の場所へ行く",
                            () => new Scenario1_School().LoadScript("options_where_to_go")));
                    })
            };
            Scripts.Add("options_what_to_do", options_what_to_do);

            // 「協力しない」
            var search_about_incident = new List<Section>()
            {
                new Section("ヒロシ", "気になるタイトルのスレッドがあるぞ　どれどれ‥‥"),
                new Section("", "『最近この学校で　よく物が盗まれる件　知ってる人？"),
                new Section("", "『知ってる～　あたしもアクセサリー盗まれた～！\nまじサイアク"),
                new Section("", "『僕も　カード盗まれたよ\nプレミア加工の超レアカードだったのに‥‥"),
                new Section("", "『↑学校でカードゲームする奴ｗｗｗ"),
                new Section("", "『さっき聞いたんだけど　2組にも　財布盗まれた子がいるらしいね"),
                new Section("ヒロ子", "え～！　盗まれたの斎藤さんだけじゃないんだ\n犯人は同一人物かな～",
                    () => SetCharacterImage("ヒロ子", "running")),
                new Section("ヒロシ", "もしそうなら　犯人は金目のものばかり狙っていることになるぞ\n悪質だな‥‥"),
                new Section("", "", () => LoadScript("options_what_to_do"))
            };
            Scripts.Add("search_about_incident", search_about_incident);

            var search_about_girl = new List<Section>()
            {
                new Section("ヒロシ", "校内美少女ランキング？　くだらないことを考えるやつがいるもんだな"),
                new Section("ヒロ子", "‥‥‥", () => SetCharacterImage("ヒロ子", "tere")),
                new Section("ヒロシ", "お前の事を考えて　見るのはやめといてやるよ"),
                new Section("ヒロ子", "それ　どういう意味なんでしょうか‥‥", () => SetCharacterImage("ヒロ子", "depressed")),
                new Section("", "", () => LoadScript("options_what_to_do"))
            };
            Scripts.Add("search_about_girl", search_about_girl);

            var search_about_cat = new List<Section>()
            {
                new Section("ヒロシ", "飼い猫を探している？　事件には関係なさそうだけど　一応のぞいてみるか‥‥"),
                new Section("", "『飼い猫を探しています　名前はミケ\n大きめの三毛猫で　キラキラしたものが好き"),
                new Section("ヒロ子", "探してあげたいけど　今は財布のことがあるしね", () => SetCharacterImage("ヒロ子", "depressed")),
                new Section("ヒロシ", "‥‥いや　ひょっとするとこれはいい情報かもしれないぞ"),
                new Section("ヒロ子", "？？？", () => SetCharacterImage("ヒロ子", "normal")),
                new Section("", "", () => LoadScript("options_what_to_do"))
            };
            Scripts.Add("search_about_cat", search_about_cat);
        }
    }
}