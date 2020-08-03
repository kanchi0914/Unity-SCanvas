using EGUI.Base;
using UnityEngine;

namespace EGUI.GameObjects
{
    public class EGCanvas : EGGameObject
    {
        public EGCanvas(string name = "SCanvas", bool isStatic = false) : base
        (
            null,
            0,
            0,
            1,
            1,
            name,
            () => UIFactory.CreateCanvas(name)
        )
        {
            if (isStatic)
            {
                gameObject.GetComponent<Canvas>().sortingOrder = 1000;
            }
            else
            {
                CanvasStack.Push(this);
            }
        }
    }
}