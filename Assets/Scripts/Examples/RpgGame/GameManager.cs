using System;
using EGUI.GameObjects;
using UnityEngine;

namespace Examples.RpgGame
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            new CommandBattle(gameObject);
            // new EGGameObject().SetSize(100, 100).SetImageColor(Color.black).AddOnClick(() => Debug.Log("!!!!s"));
        }
    }
}