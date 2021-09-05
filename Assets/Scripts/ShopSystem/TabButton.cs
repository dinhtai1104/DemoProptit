using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyGame.Pong.UI.Shop
{
    public class TabButton : MonoBehaviour, IPointerClickHandler
    {
        public Image btnImage;
        public bool startTabButton = false;
        public Tab tab;
        public ShopComponent shop = null;



        private void OnEnable()
        {

            if (startTabButton)
            {
                btnImage.color = Color.yellow;
                SetActiveTab(true);
            } else
            {
                SetActiveTab(false);
                btnImage.color = Color.white;
            }
        }
        public void SetActiveTab(bool active)
        {
            if (active)
            {
                btnImage.color = Color.yellow;
                tab.gameObject.SetActive(true);
            }
            else
            {
                btnImage.color = Color.white;
                tab.gameObject.SetActive(false);
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            shop.Clear();
            SetActiveTab(true);
        }
    }
}