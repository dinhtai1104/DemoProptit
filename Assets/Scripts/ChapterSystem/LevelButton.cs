using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyGame.Pong.UI.LevelSelect
{
    public class LevelButton : MonoBehaviour, IPointerClickHandler
    {
        public Image background;
        public Sprite currentSprite, lockedSprite, unlockedSprite;
        public GameObject starContainer;
        public GameObject currentObject;
        public GameObject lockedObject;
        public Text levelName;

        private int id = 1;
        private bool unlocked = false;
        public void InitButton(int id, bool unlocked, bool current)
        {
            this.id = id;
            this.unlocked = unlocked;

            starContainer.SetActive(false);
            currentObject.SetActive(false);
            lockedObject.SetActive(false);
            levelName.text = id.ToString("00");
            if (current)
            {
                background.sprite = currentSprite;
                currentObject.SetActive(true);
            } else
            {
                if (unlocked)
                {
                    background.sprite = unlockedSprite;
                    starContainer.SetActive(true);
                    
                    int star = Utils.GetStarLevel(id);

                    for (int i = 0; i < 3; i++)
                    {
                        starContainer.transform.GetChild(i).gameObject.SetActive(false);
                    }


                    for(int i = 0; i < star; i++)
                    {

                        starContainer.transform.GetChild(i).gameObject.SetActive(true);

                    }
                } else
                {
                    levelName.text = "";
                    background.sprite = lockedSprite;
                    lockedObject.SetActive(true);
                }
            }

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (unlocked)
            {
                Utils.LEVELCURRENT = id;
                GameMaster.JoinGame?.Invoke();
            } else
            {
                Debug.Log("Level Locked");
            }
        }
    }
}