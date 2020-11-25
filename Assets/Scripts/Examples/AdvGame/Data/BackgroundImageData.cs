using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Examples.AdvGame
{
    public class BackgroundImageData
    {
        public static Dictionary<string, Sprite> Images = new Dictionary<string, Sprite>();
        
        // public enum BackgroundImageName
        // {
        //     pc_room,
        //     dote,
        //     street,
        //     class_room,
        //     class_room_afternoon,
        //     hallway
        // }

        static BackgroundImageData()
        {
            Images.Add("bg_computer_room_hellowork", Resources.Load<Sprite>("Images/bg_computer_room_hellowork"));
            Images.Add("bg_dote_yuyake", Resources.Load<Sprite>("Images/bg_dote_yuyake"));
            Images.Add("bg_outside_jutaku", Resources.Load<Sprite>("Images/bg_outside_jutaku"));
            Images.Add("bg_school_room", Resources.Load<Sprite>("Images/bg_school_room"));
            Images.Add("bg_school_room_yuyake", Resources.Load<Sprite>("Images/bg_school_room_yuyake"));
            Images.Add("bg_school_rouka", Resources.Load<Sprite>("Images/bg_school_rouka"));
        }
    }
}