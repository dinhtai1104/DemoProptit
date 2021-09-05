using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MyGame.Pong.UI.Shop
{
    public class ShopComponent : MonoBehaviour
    {
        public List<TabButton> tabButtons;



        public void Clear()
        {
            foreach (TabButton bt in tabButtons)
            {
                bt.SetActiveTab(false);
            }
        }

        public void BackToMenu()
        {
            gameObject.SetActive(false);
        }
    }
}