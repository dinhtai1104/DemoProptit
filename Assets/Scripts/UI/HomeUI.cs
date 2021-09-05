using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace MyGame.Pong.UI
{
    public class HomeUI : MonoBehaviour
    {
        public Text textLevelButton;
        public Text starInfor, levelInfor;
        public Image starSliderImg, levelSliderImg;

        public RectTransform starSlider, levelSlider;
        public RectTransform btnPlay, pongIcon;
        public RectTransform[] btns;

        public GameObject menuComponent;
        public SettingComponent settingComponent;

        private void Start()
        {
            GameMaster.JoinGame += this.JoinGameHandleEvent;
        }

        private void JoinGameHandleEvent()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            textLevelButton.text = "LEVEL " + Utils.LEVEL.ToString("00");
            float sizeX = 235;
            float sizeY = starSlider.sizeDelta.y;

            int totalStar = 0;
            for (int i = 0; i < Object.GameManager.TotalLevel; i++)
            {
                totalStar += Utils.GetStarLevel(i + 1);
            }

            starInfor.text = "STAR: " + 0 + "/" + (Object.GameManager.TotalLevel * 3);

            levelInfor.text = "LEVEL: " + 0 + "/" + Object.GameManager.TotalLevel;

            starInfor.DOText("STAR: " + totalStar + "/" + (Object.GameManager.TotalLevel * 3), 0.5f);
            levelInfor.DOText("LEVEL: " + Utils.LEVEL + "/" + Object.GameManager.TotalLevel, 0.5f);
            starSliderImg.rectTransform.DOSizeDelta(new Vector2(sizeX * (totalStar* 1.0f/(Object.GameManager.TotalLevel * 3)), sizeY), 0.5f).From(new Vector2(0, starSlider.sizeDelta.y));
            levelSliderImg.rectTransform.DOSizeDelta(new Vector2(sizeX * (Utils.LEVEL * 1.0f/ Object.GameManager.TotalLevel), sizeY), 0.5f).From(new Vector2(0, starSlider.sizeDelta.y));

            Sequence sq = DOTween.Sequence();
            sq.Append(pongIcon.DOAnchorPosX(0, 0.25f).From(new Vector2(Screen.width, pongIcon.anchoredPosition.y)));
            sq.Append(btnPlay.DOAnchorPosX(0, 0.25f).From(new Vector2(Screen.width, btnPlay.anchoredPosition.y)));
            foreach (RectTransform rect in btns)
            {
                sq.Append(rect.DOAnchorPosX(rect.anchoredPosition.x, 0.1f).From(new Vector2(Screen.width, rect.anchoredPosition.y)));
            }
        }


        public void SettingsOnClicked()
        {
            settingComponent.gameObject.SetActive(true);
        }
    }
}