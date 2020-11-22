using System;
using System.Collections.Generic;
using Assets.Scripts.Examples.AdvGame.Objects;
using EGUI.Base;
using EGUI.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class Scenario4_AfternoonDote : Scenario
    {
        public Scenario4_AfternoonDote()
        {
            InitScripts();
            SetBackGroundImage("Images/bg_dote_yuyake");
            Load();
        }

        private void InitScripts()
        {
            var intro = new List<Section>()
            {
                new Section("", "その後　俺はヒロ子の100円を使って　ミケをおびき出し\n後を追って盗まれたもののありかを見つけ出した"),
                new Section("", "斎藤さん含め　盗まれたものはすべて持ち主のところへ帰り\nみんなからだけでなく　ついでに猫の持ち主からも感謝されることになった‥"),
                new Section("ヒロ子", "一件落着だね～　今日のヒロシくん　すっごくカッコよかったよ～", 
                    () => SetCharacterImage("ヒロ子", "smile")),
                new Section("ヒロシ", "まあな　俺にかかれば　あんなのどうってことないさ"),
                new Section("ヒロ子", "そうだね～　でも今日は楽しくて　なんか昔の事　思い出しちゃった", 
                    () => SetCharacterImage("ヒロ子", "normal")),
                new Section("ヒロシ", "昔のこと？"),
                new Section("ヒロ子", "うん　ヒロシくんは覚えてないかもしれないけど\n昔はよく二人で　探偵ごっこやってたんだよ\nだから　そのときのこと"),
                new Section("ヒロシ", "いや　俺も覚えてるよ　俺が探偵で　おまえが助手役だったよな\nそうそう　ちょうど今日みたいだった\nたしかに　あれは楽しかったな"),
                new Section("ヒロ子", "‥‥‥‥‥", () => SetCharacterImage("ヒロ子", "smile")),
                new Section("ヒロシ", "それにしてもおまえ　100円くらいで騒ぎすぎなんだよ\nちょっと借りるって言ったぐらいでさ‥‥\n" +
                                   "ケチなのは知ってたけど　あんな渋るかよ普通"),
                new Section("ヒロ子", "お金‥‥大事なんだ　将来のために とっておいてあるの", 
                    () => SetCharacterImage("ヒロ子", "normal")),
                new Section("ヒロシ", "将来のため？"),
                new Section("ヒロ子", "うん\n‥‥‥ヒロシくんとの結婚資金", 
                    () => SetCharacterImage("ヒロ子", "tere")),
                new Section("ヒロシ", "‥‥‥‥は？"),
                new Section("ヒロ子", "ヒロシくん！！好きです　わたしと付き合ってください！！！", 
                    () =>
                    {
                        SetCharacterImage("ヒロ子", "love");
                        var optionWindow = new OptionsWindow(AdvMessageWindow);
                        optionWindow.AddOption("はい", () => Load("choose_yes"));
                        optionWindow.AddOption("いいえ", () => Load("choose_no"));
                    }),
            };
            Scripts.Add("intro", intro);
            
            var choose_yes = new List<Section>()
            {
                new Section("ヒロシ", "仕方ないな‥‥　頼りにならない助手役だが\nこれからもよろしくな"),
                new Section("ヒロ子", "やったー！！！　ヒロシくん　愛してる！！！", 
                    () => SetCharacterImage("ヒロ子", "joy")),
                new Section("ヒロシ", "その後　俺たちは無事に結婚し　幸せな家庭を築くのであった‥‥"),
                new Section("", "～HAPPY END!!～"),
                new Section("", "", () =>
                {
                    CanvasStack.ClearAll();
                    new AdvGameOpening();
                }),
            };
            Scripts.Add("choose_yes", choose_yes);
            
            var choose_no = new List<Section>()
            {
                new Section("ヒロシ", "悪いけど‥‥おまえのことは　友達としてしか見れないな"),
                new Section("ヒロ子", "そっか‥‥そうだよね　ごめんね　今のは忘れて", 
                    () => SetCharacterImage("ヒロ子", "depressed")),
                new Section("ヒロシ", "その後　ヒロ子とはほとんど話さなくなり　俺は結局彼女ができないまま" +
                                   "寂しい学校生活を送るのであった‥‥"),
                new Section("", "～BAD END～"),
                new Section("", "", () =>
                {
                    CanvasStack.ClearAll();
                    new AdvGameOpening();
                }),
            };
            Scripts.Add("choose_no", choose_no);
        }
    }
}