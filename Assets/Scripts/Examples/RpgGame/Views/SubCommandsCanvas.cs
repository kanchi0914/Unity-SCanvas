using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.EGUI;
using Assets.Scripts.Extensions;
using EGUI.Base;
using EGUI.GameObjects;
using UniRx;
using UnityEngine;

namespace Examples.RpgGame.Views
{
    public class SubCommandsCanvas : RpgCanvas
    {
        protected EGGameObject subCommandsWindow;
        protected EGGridLayoutView layoutGrid;
        protected EGGameObject descriptionTextWindow;
        protected RpgText descriptionTextLabel;
        private List<CommandTextLabel> commandTextLabels;

        public SubCommandsCanvas(CommandsWindow commandsWindow, int rowNum = 2, int colNum = 2) : base(
            "SubCommandsCanvas")
        {
            subCommandsWindow = new EGGameObject(this, "commandsWindow")
                .SetImage("Images/clear_box")
                .SetAnchorType(AnchorType.BottomCenter)
                .SetPosition(100, 60)
                .SetSize(300, commandsWindow.RectSize.y - 50);

            descriptionTextWindow = new EGGameObject(this, "descriptionTextWindow")
                .SetAnchorType(AnchorType.MiddleRight)
                .SetImage("Images/clear_box")
                .SetAnchorType(AnchorType.BottomCenter)
                .SetPosition(100, 10)
                .SetSize(300, 40);

            descriptionTextLabel = new RpgText(descriptionTextWindow)
                .SetCharacter(fontSize: 16)
                .SetParagraph(TextAnchor.MiddleLeft)
                .SetSize(280, 30)
                .As<RpgText>();

            layoutGrid = new EGGridLayoutView(subCommandsWindow, rowNum, colNum)
                .SetRelativeSize(1, 1, false).As<EGGridLayoutView>();
        }

        public void Disable()
        {
            subCommandsWindow.SetImageColor(Color.gray);
            descriptionTextWindow.SetImageColor(Color.gray);
            descriptionTextLabel.SetTextColor(Color.gray);
            layoutGrid.gameObject.GetChildrenObjects().ForEach(g =>
            {
                var label = g.GetComponent<EGUIObjectInfo>().EgGameObject as CommandTextLabel;
                label.Disable();
            });
        }

        public void Enable()
        {
            subCommandsWindow.SetImageColor(Color.white);
            descriptionTextWindow.SetImageColor(Color.white);
            descriptionTextLabel.SetTextColor(Color.white);
            layoutGrid.gameObject.GetChildrenObjects().ForEach(g =>
            {
                var label = g.GetComponent<EGUIObjectInfo>().EgGameObject as CommandTextLabel;
                label.Enable();
            });
        }
    }
}