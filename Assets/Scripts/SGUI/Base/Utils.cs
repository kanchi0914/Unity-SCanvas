using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SGUI.Base
{
    public class Utils
    {
        private static Camera mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();

        public static Vector3 getScreenTopLeft ()
        {
            // 画面の左上を取得
            Vector3 topLeft = mainCamera.ScreenToWorldPoint (Vector3.zero);
            // 上下反転させる
            topLeft.Scale (new Vector3 (1f, -1f, 1f));
            return topLeft;
        }

        public static Vector3 getScreenBottomRight ()
        {
            // 画面の右下を取得
            Vector3 bottomRight = mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f));
            // 上下反転させる
            bottomRight.Scale (new Vector3 (1f, -1f, 1f));
            return bottomRight;
        }

        public static Vector3 GetScreenMiddleCenter ()
        {
            return mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, 0.0f));
        }

        public static int DefaultFontSize
        {
            get
            {
                return (int) (Screen.width * 0.025f);
            }
        }
        
        public static Color GetColor (ColorType colorType, float alpha)
        {
            Color color;
            switch (colorType)
            {
                case ColorType.Black:
                    {
                        color = Color.black;
                        break;
                    }
                case ColorType.White:
                    {
                        color = Color.white;
                        break;
                    }
                case ColorType.Cyan:
                    {
                        color = Color.cyan;
                        break;
                    }
                case ColorType.Gray:
                    {
                        color = Color.gray;
                        break;
                    }
                case ColorType.Magenta:
                    {
                        color = Color.magenta;
                        break;
                    }
                case ColorType.Red:
                    {
                        color = Color.red;
                        break;
                    }
                case ColorType.Yellow:
                    {
                        color = Color.yellow;
                        break;
                    }
                case ColorType.Green:
                    {
                        color = Color.green;
                        break;
                    }
                case ColorType.Blue:
                    {
                        color = Color.blue;
                        break;
                    }
                default:
                    return new Color (0, 0, 0, 0);
            }
            color.a = alpha;
            return color;
        }

        public enum AnchorType
        {
            TopLeft,    TopCenter,    TopRight,
            MiddleLeft, MiddleCenter, MiddleRight,
            BottomLeft, BottomCenter, BottomRight,
            HorizontalStretch, VerticalStretch,
            FullStretch
        }

    }
}