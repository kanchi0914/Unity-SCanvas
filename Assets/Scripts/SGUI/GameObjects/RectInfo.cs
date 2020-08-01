using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RectInfo
{
    public float PosX { get; protected set; }
    public float PosY { get; protected set; }
    public float Width { get; protected set; }
    public float Height { get; protected set; }

    public RectInfo(float posX, float posY, float width, float height)
    {
        PosX = posX;
        PosY = posY;
        Width = width;
        Height = height;
    }

    public RectInfo(RectInfo parentRectInfo, float posXRatio, float posYRatio, float widthRatio, float heightRatio)
    {
        PosX = parentRectInfo.PosX * posXRatio;
        PosY = parentRectInfo.PosY * posYRatio;
        Width = parentRectInfo.Width * widthRatio;
        Height = parentRectInfo.Height * heightRatio;
    }
}