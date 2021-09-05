using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyGame.Pong.UI.LevelSelect
{
    public class ChapterButton : MonoBehaviour, IPointerClickHandler
    {
        public GameObject lockedObject;
        public Image background;
        public Text chapterName;
        public Text starCount;
        private int id = 0;
        private bool unlocked = false;

        public void InitChapter(int id, bool unlocked)
        {
            this.id = id;
            chapterName.text = "CHAPTER " + id.ToString("00");

            this.unlocked = unlocked;
            if (unlocked)
            {
                lockedObject.SetActive(false);
            } else
            {
                lockedObject.SetActive(true);
            }

            SetStarCount();
        }

        private void SetStarCount()
        {
            int totalStars = GetTotalStars();

            starCount.text = totalStars.ToString("00") + "/36";
        }

        public int GetTotalStars()
        {
            int totalStars = 0;
            int levelStart = (id - 1) * 12 + 1;

            for (int i = 0; i < 12; i++)
            {
                totalStars += Utils.GetStarLevel(i + levelStart);
            }
            return totalStars;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!unlocked)
            {
                Debug.Log("Chapter Locked");
            } else
            {
                GameMaster.SelectChapterLevel?.Invoke(id);
            }
        }
    }
}
