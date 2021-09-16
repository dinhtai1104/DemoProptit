using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using System;

namespace MyGame.Pong.UI.LevelSelect
{
    public class ChapterSystem : MonoBehaviour
    {
        public int totalLevel = 10;
        private int chapter;
        public ChapterButton chapterPrefab;
        public Transform content;
        private List<ChapterButton> listChapters;

        public Text starCountText, levelCountText;
        public Image starSlider, levelSlider;


        public LevelSelect levelSelect;
        private void Start()
        {
            totalLevel = Object.GameManager.TotalLevel;
            GameMaster.JoinGame += this.JoinGameHandleEvent;
            GameMaster.SelectChapterLevel += this.SelectChapterHandleEvent;
            levelSelect.SetMaxChapter(totalLevel / 12 + 1);
        }

        private void SelectChapterHandleEvent(int pChapter)
        {
            levelSelect.SetChapterLevel(pChapter);
        }

        private void JoinGameHandleEvent()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            totalLevel = Object.GameManager.TotalLevel;
            chapter = totalLevel / 12 + 1;
            if (listChapters == null)
            {
                GenerateChapters();
            }
            RefreshUI();
        }

        private void GenerateChapters()
        {
            listChapters = new List<ChapterButton>();
            for (int i = 0; i < chapter; i++)
            {
                ChapterButton chapter = Instantiate(chapterPrefab, content);
                listChapters.Add(chapter);
            }
        }

        private void RefreshUI()
        {
            int totalStar = 0;

            for (int i = 0; i < listChapters.Count; i++)
            {
                listChapters[i].InitChapter(i + 1, true);
            }

            for (int i = 0; i < totalLevel; i++)
            {
                totalStar += Utils.GetStarLevel(i + 1);
            }

            starCountText.text = totalStar.ToString("00") + "/" + (totalLevel * 3);
            levelCountText.text = Utils.LEVEL.ToString("00") + "/" + totalLevel;

            float sizeX = 220;
            float sizeY = starSlider.rectTransform.sizeDelta.y;

            starSlider.rectTransform.DOSizeDelta(new Vector2(sizeX * (totalStar * 1.0f / (totalLevel * 3)), sizeY), 0.5f).From(new Vector2(0, sizeY));
            levelSlider.rectTransform.DOSizeDelta(new Vector2(sizeX * (Utils.LEVEL * 1.0f / totalLevel), sizeY), 0.5f).From(new Vector2(0, sizeY));

        }
        private void OnDestroy()
        {
            GameMaster.JoinGame -= this.JoinGameHandleEvent;
        }

    }
}