using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame.Pong.UI.Shop
{
    public class Tab : MonoBehaviour
    {
        public ItemButton itemPrefab;
        public ItemType type;
        private List<ItemButton> listItems = null;
        public Transform content;
        private void OnEnable()
        {
            if (listItems == null)
            {
                listItems = new List<ItemButton>();
                // Sinh ra cac item
                int size = 0;
                if (type == ItemType.BALL)
                {
                    size = Object.GameManager.Instance.data.ballsSkin.Length;
                } else if (type == ItemType.CUP)
                {
                    size = Object.GameManager.Instance.data.cupsSkin.Length;
                }

                for (int i = 0; i < size; i++)
                {
                    ItemButton it = Instantiate(itemPrefab, content);
                    it.SetTab(this);
                    listItems.Add(it);
                }
            }
            RefreshUI();
        }

        public void RefreshUI()
        {
            int i = 0;
            foreach (ItemButton btn in listItems)
            {
                btn.InitInfor(i, type, 
                    (type == ItemType.CUP ? 
                        Object.GameManager.Instance.data.cupsSkin[i] :
                        Object.GameManager.Instance.data.ballsSkin[i]), 
                    false, 
                    i == (type == ItemType.CUP ? Utils.CUP : Utils.BALL));
                i++;
            }
        }
    }
}