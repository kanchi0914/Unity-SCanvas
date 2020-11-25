using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public static class CharactorData
    {
        public static List<Character> Characters = new List<Character>();
        static CharactorData()
        {
            var heroine = new Character(){Name = "ヒロ子"};
            heroine.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/ryuugakusei_seifuku_asia_woman")},
                {"joy", Resources.Load<Sprite>("Images/happy_schoolgirl")},
                {"running", Resources.Load<Sprite>("Images/chikoku_chugakusei_girl")},
                {"sumaho", Resources.Load<Sprite>("Images/smartphone_schoolgirl_stand")},
                {"tere", Resources.Load<Sprite>("Images/sick_atsui_school_girl")},
                {"smile", Resources.Load<Sprite>("Images/school_blazer_girl_kurubushi")},
                {"depressed", Resources.Load<Sprite>("Images/school_girl_cry_walk")},
                {"love", Resources.Load<Sprite>("Images/message_loveletter_girl")},
            };
            var saito = new Character(){Name = "斎藤"};
            saito.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/shisyunki_girl2")},
            };
            var danshi = new Character(){Name = "男子生徒"};
            danshi.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/himan_pocchari_blazer_schoolboy")},
            };
            var joshi = new Character(){Name = "女子生徒"};
            joshi.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/school_joshikousei_kogyaru_90s")},
            };
            Characters.Add(heroine);
            Characters.Add(saito);
            Characters.Add(danshi);
            Characters.Add(joshi);
        }
    }
}