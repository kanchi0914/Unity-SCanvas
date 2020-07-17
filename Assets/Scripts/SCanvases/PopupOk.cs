using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;
using SGUI.Base;
using SGUI.SGameObjects;

namespace Assets.Scripts.SCanvases
{
    public class PopupOk : SCanvas
    {
        // set constructor
        public PopupOk (string text, string name = "PopupOk") : base(name)
        {
            var window = new SSubCanvas (this, "", 0.4f, 0.4f, 0.4f, 0.3f);
            new SButton (window, text)
                .SetLocalPosByRatio (0.2f, 0.6f)
                .SetRectSizeByRatio (0.6f, 0.3f)
                .AddOnClick (GetFunction (CanvasStack.Pop));
        }
    }
}