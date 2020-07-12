using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Utils
    {
        private static Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        

        public static Vector3 getScreenTopLeft()
        {
            // 画面の左上を取得
            Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
            // 上下反転させる
            topLeft.Scale(new Vector3(1f, -1f, 1f));
            return topLeft;
        }

        public static Vector3 getScreenBottomRight()
        {
            // 画面の右下を取得
            Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
            // 上下反転させる
            bottomRight.Scale(new Vector3(1f, -1f, 1f));
            return bottomRight;
        }

        public static Vector3 GetScreenMiddleCenter()
        {
            return mainCamera.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 0.0f));
        }

        public enum TransitionType
        {
            Overlay,
            ClearAndPop,
            Recurrence
        }

        public enum ColorType
        {
            Black, White
        }
        
        public static Color GetColor(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.Black:
                    return Color.black;
                case ColorType.White:
                    return Color.white;
                default:
                    return Color.black;
            }

        }

        private static void fun()
        {
            //var button = UICreator.CreateButton();
            //button.transform.SetParent(mainCanvas, false);
            //var width = button.transform.GetComponent<RectTransform>().sizeDelta.x;
            //var height = button.transform.GetComponent<RectTransform>().sizeDelta.y;

            //var screenPos = getScreenTopLeft();
            //Debug.Log(screenPos);
            //button.transform.position = screenPos;
            //var swidth = Screen.width;
            //var sheight = Screen.height;
            //button.transform.SetPosZ(0);
            //button.transform.AddLocalPosX(width / 2);
            //button.transform.AddLocalPosX(swidth / 2);
            //button.transform.AddLocalPosY(-height / 2);
            //button.transform.AddLocalPosY(-sheight / 2);
        }
    }
}
