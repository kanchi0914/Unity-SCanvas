using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EGUI.Base
{
    public class Utils
    {
        private static Camera mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();

        public static int DefaultFontSize
        {
            get
            {
                return (int) (Screen.width * 0.025f);
            }
        }


    }
}