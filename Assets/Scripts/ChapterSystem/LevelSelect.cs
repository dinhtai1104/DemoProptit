using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MyGame.Pong.UI.LevelSelect
{
    public class LevelSelect : MonoBehaviour
    {
        public Button next, previous;
        public LevelButton buttonPrefab;
        public Transform content;
        public Text chapterName;
        private List<LevelButton> listLevels;
        private int chapter = 1;
        private int maxChapter = 1;

        public Text starCountText, levelCountText;
        public Image starSlider, levelSlider;


        // Start is called before the first frame update
        void Start()
        {
            GameMaster.JoinGame += this.JoinGameHandleEvent;
            next.onClick.AddListener(() =>
            {

                chapter++;
                Debug.Log("Chapter :" + chapter);
                chapter %= (maxChapter + 1);
                if (chapter == 0) chapter = 1;
                RefreshUI();
            });

            previous.onClick.AddListener(() =>
            {
                chapter--;
                Debug.Log("Chapter :" + chapter);
                if (chapter <= 0)
                {
                    chapter = maxChapter;
                }
                RefreshUI();
            });
        }

        

        private void OnEnable()
        {
            if (listLevels == null)
            {
                listLevels = new List<LevelButton>();
                for (int i = 0; i < 12; i++)
                {
                    LevelButton btn = Instantiate(buttonPrefab, content);
                    listLevels.Add(btn);
                }
            }
            RefreshUI();
        }

        private void RefreshUI()
        {
            chapterName.text = "CHAPTER " + chapter.ToString("00");
            int levelStart = (chapter - 1) * 12 + 1;
            int totalStar = 0, totalLevel = 12;

            for (int i = 0; i < 12; i++)
            {
                int id = levelStart + i;
                listLevels[i].InitButton(id, (id) <= Utils.LEVEL, (id) == Utils.LEVEL);
                if (id > Object.GameManager.TotalLevel)
                {
                    listLevels[i].gameObject.SetActive(false);
                } else
                {
                    listLevels[i].gameObject.SetActive(true);
                }
                totalStar += Utils.GetStarLevel(id);
                totalLevel += (id <= Utils.LEVEL) ? 1 : 0;
            }


            starCountText.text = totalStar.ToString("00") + "/" + (totalLevel * 3);
            levelCountText.text = Utils.LEVEL.ToString("00") + "/" + totalLevel;

            float sizeX = 235;
            float sizeY = starSlider.rectTransform.sizeDelta.y;

            starSlider.rectTransform.DOSizeDelta(new Vector2(sizeX * (totalStar * 1.0f / (totalLevel * 3)), sizeY), 0.5f).From(new Vector2(0, sizeY));
            levelSlider.rectTransform.DOSizeDelta(new Vector2(sizeX * (Utils.LEVEL * 1.0f / totalLevel), sizeY), 0.5f).From(new Vector2(0, sizeY));

        }

        private void JoinGameHandleEvent()
        {
            gameObject.SetActive(false);
        }

        public void SetChapterLevel(int chapter)
        {
            this.chapter = chapter;
            gameObject.SetActive(true);
        }
    
        public void SetMaxChapter(int mChapter)
        {
            this.maxChapter = mChapter;
        }
        public void Back()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            GameMaster.JoinGame -= this.JoinGameHandleEvent;
        }
    }
}