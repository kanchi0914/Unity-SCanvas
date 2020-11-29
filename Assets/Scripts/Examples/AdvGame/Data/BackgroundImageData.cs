using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class BackgroundImageData
    {
        public static Dictionary<string, Sprite> Images = new Dictionary<string, Sprite>();
        
        static BackgroundImageData()
        {
            Images.Add("school", Resources.Load<Sprite>("Images/school"));
            Images.Add("bg_computer_room_hellowork", Resources.Load<Sprite>("Images/bg_computer_room_hellowork"));
            Images.Add("bg_dote_yuyake", Resources.Load<Sprite>("Images/bg_dote_yuyake"));
            Images.Add("bg_outside_jutaku", Resources.Load<Sprite>("Images/bg_outside_jutaku"));
            Images.Add("bg_school_room", Resources.Load<Sprite>("Images/bg_school_room"));
            Images.Add("bg_school_room_yuyake", Resources.Load<Sprite>("Images/bg_school_room_yuyake"));
            Images.Add("bg_school_rouka", Resources.Load<Sprite>("Images/bg_school_rouka"));
        }
    }
}