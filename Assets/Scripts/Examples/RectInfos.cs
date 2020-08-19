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
    // 攻撃、防御など基本コマンドのボタンを表示するエリア
    public static readonly RectInfo commandButtons = new RectInfo(commandsView, 0.00f, 0.00f, 0.40f, 1.00f);
    public static readonly RectInfo subCommandsView = new RectInfo(commandsView, 0.40f, 0.00f, 0.60f, 1.00f);
    public static readonly RectInfo subCommands = new RectInfo(20, 20, subCommandsView.Width - 40, subCommandsView.Height - 40);

    public static readonly RectInfo messageWindow  = new RectInfo(screen, 0.15f, 0.65f, 0.70f, 0.20f);

    // 仲間選択時のパネル
    public static readonly RectInfo whomAllyPanel  = new RectInfo(screen.Width * 0.10f, screen.Height * 0.30f, 100, 50);

    // 仲間選択時のパネル
    public static readonly RectInfo whomEnemyPanel = new RectInfo(screen.Width * 0.10f, screen.Height * 0.65f, 100, 50);
}