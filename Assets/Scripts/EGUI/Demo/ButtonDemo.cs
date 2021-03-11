using EGUI.GameObjects;

namespace EGUI.Demo
{
    public class ButtonDemo
    {
        public ButtonDemo()
        {
            new EGButton()
                .SetText("Button")
                .SetSize(200,50);
        }
    }
}