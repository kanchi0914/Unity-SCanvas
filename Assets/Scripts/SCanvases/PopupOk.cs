using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.SCanvases
{
    public class PopupOk : SCanvas
    {
        // set constructor
        public PopupOk(string message)
        {
            var window = new SubCanvas(this, 0.4f, 0.4f, 0.4f, 0.3f);
            new SButton(window, message)
                .SetLocalPos(0f, 0.5f)
                .SetRectSize(1f, 1f)
                .AddOnClick(GetFunction(CanvasStack.Pop));
        }
    }
}
