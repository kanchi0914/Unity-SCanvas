using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class RectInfos
{
    public static float sw = Screen.width;
    public static float sh = Screen.height;

    public static readonly RectInfo screen = new RectInfo(0, 0, sw, sh);

    //                                                                    parent posX   posY   width  height   
    public static readonly RectInfo alliesView     = new RectInfo(screen, 0.10f, 0.00f, 0.80f, 0.30f);
    public static readonly RectInfo allyView       = new RectInfo(alliesView, 0.00f, 0.00f, 0.25f, 1.00f);

    public static readonly RectInfo enemiesView    = new RectInfo(screen, 0.10f, 0.35f, 0.80f, 0.30f);

    public static readonly RectInfo commandsView   = new RectInfo(screen, 0.00f, 0.70f, 1.00f, 0.30f);
    public static readonly RectInfo commandButtons = new RectInfo(commandsView, 0.00f, 0.00f, 0.40f, 1.00f);
    public static readonly RectInfo commandSubView = new RectInfo(commandsView, 0.00f, 0.00f, 0.60f, 1.00f);
    public static readonly RectInfo commandSubButtons = new RectInfo(20, 20, commandSubView.Width - 40, commandSubView.Height - 40);

    public static readonly RectInfo messageWindow  = new RectInfo(screen, 0.15f, 0.65f, 0.70f, 0.20f);

    // 仲間選択時のパネル
    public static readonly RectInfo whomAllyPanel  = new RectInfo(screen.PosX * 0.10f, screen.PosY * 0.30f, 100, 50);

    public static RectInfo messageCanvas = new RectInfo(0.15f, 0.65f, 0.7f, 0.2f);
}