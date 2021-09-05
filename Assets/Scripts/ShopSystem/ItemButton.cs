using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyGame.Pong.UI.Shop
{
    public enum ItemType { CUP, BALL, COIN }
    public class ItemButton : MonoBehaviour, IPointerClickHandler
    {
        private ItemType type;
        public Image itemImage;
        public Image marked;
        public Image background;

        private Tab tab;
        private int id = 0;
        private bool locked = false;
        public void InitInfor(int id, ItemType type, Sprite sprite, bool locked, bool current)
        {
            this.id = id;
            this.type = type;
            itemImage.sprite = sprite;
            itemImage.SetNativeSize();
            this.locked = locked;

            if (current)
            {
                marked.gameObject.SetActive(true);
                background.color = Color.cyan;
            } else
            {
                marked.gameObject.SetActive(false);
                background.color = Color.gray;
            }
        }

        public void SetTab(Tab tab)
        {
            this.tab = tab;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            switch (type)
            {
                case ItemType.CUP:
                    Utils.CUP = id;
                    break;
                case ItemType.BALL:
                    Utils.BALL = id;
                    break;
                case ItemType.COIN:
                    
                    break;
            }
            tab.RefreshUI();
        }
    }
}