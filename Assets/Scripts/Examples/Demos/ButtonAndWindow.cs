using EGUI.Base;
using UnityEngine;

namespace EGUI.GameObjects.Demos
{
    public class ButtonAndWindow
    {
        public ButtonAndWindow()
        {
            new EGButton().SetText("Click me!")
                .SetAnchorType(AnchorType.TopLeft)
                .SetPosition(20, -20)
                .SetSize(100, 40)
                .AddOnClick(() =>
                {
                    var canvas = new EGCanvas();
                    var hidePanel = new EGGameObject(canvas)
                        .SetImageColor(Color.black, 0.5f)
                        .SetRelativeSize(1,1);
                    var window = new EGGameObject(hidePanel)
                        .SetImageColor(Color.white)
                        .SetSize(300, 100);
                    new EGText(window)
                        .SetText("クリックされました。")
                        .ResizeBestFIt()
                        .SetRelativeSize(.8f, .5f)
                        .SetAnchorType(AnchorType.TopCenter);
                    new EGButton(window)
                        .SetText("OK")
                        .AddOnClick(() => canvas.DestroySelf())
                        .SetAnchorType(AnchorType.BottomCenter)
                        .SetPosition(0, 10)
                        .SetSize(80, 30);
                });
        }
    }
}