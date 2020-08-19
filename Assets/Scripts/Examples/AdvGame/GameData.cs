using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public static class GameData
    {
        public static List<Charactor> Charactors = new List<Charactor>();
        public static SaveData[] SaveSatas = new SaveData[6];

        public static bool HasAskedAboutSeat = false;

        public static Dictionary<string, Font> FontsMap = new Dictionary<string, Font>();

        static GameData()
        {
            var heroine = new Charactor(){Name = "ヒロ子"};
            heroine.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/ryuugakusei_seifuku_asia_woman")},
                {"joy", Resources.Load<Sprite>("Images/happy_schoolgirl")},
                {"running", Resources.Load<Sprite>("Images/chikoku_chugakusei_girl")},
                {"sumaho", Resources.Load<Sprite>("Images/smartphone_schoolgirl_stand")},
                {"tere", Resources.Load<Sprite>("Images/sick_atsui_school_girl")},
                {"smile", Resources.Load<Sprite>("Images/school_blazer_girl_kurubushi")},
                {"depressed", Resources.Load<Sprite>("Images/school_girl_cry_walk")},
            };
            var mobko = new Charactor(){Name = "モブ子"};
            mobko.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/study_gariben_girl")},
            };
            var danshi = new Charactor(){Name = "男子生徒"};
            danshi.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/himan_pocchari_blazer_schoolboy")},
            };
            var joshi = new Charactor(){Name = "女子生徒"};
            joshi.ImageMap = new Dictionary<string, Sprite>()
            {
                {"normal", Resources.Load<Sprite>("Images/school_joshikousei_kogyaru_90s")},
            };
            Charactors.Add(heroine);
            Charactors.Add(mobko);
            Charactors.Add(danshi);
            Charactors.Add(joshi);
            FontsMap.Add("genju", Resources.Load<Font>("Fonts/GenJyuuGothic-Bold"));
        }
    }
}