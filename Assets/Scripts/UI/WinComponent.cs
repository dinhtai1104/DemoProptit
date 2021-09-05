using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MyGame.Pong.UI
{
    public class WinComponent : MonoBehaviour
    {

        public RectTransform bannerWin, coinContainer;
        public List<RectTransform> stars;
        public Text levelText;

        public Text coinText;

        public List<RectTransform> buttons;
        // Start is called before the first frame update
       
        public void OnShow(int ball = 0)
        {
            Object.SoundManager.WinSound();

            gameObject.SetActive(true);
            levelText.text = "LEVEL " + Utils.LEVELCURRENT.ToString("00");
            Sequence sq = DOTween.Sequence();

            sq.Append(bannerWin.DOAnchorPosX(0, 0.5f).SetEase(Ease.OutBounce).From(new Vector2(Screen.width * 1.2f, bannerWin.anchoredPosition.y)));
            
            int star = 0;

            if (ball < 1)
            {
                star = 1;
            } else if (ball < 2)
            {
                star = 2;
            } else
            {
                star = 3;
            }

            for (int i = 0; i < 3; i++)
            {
                stars[i].localScale = Vector3.zero;
            }

            for (int i = 0; i < star; i++)
            {
                sq.Append(stars[i].DOScale(Vector3.one, 0.2f).OnStart(()=>Object.SoundManager.StarWinAppear()));
            }

            Utils.SaveLevel(Utils.LEVELCURRENT, star);


            sq.Append(coinContainer.DOAnchorPosX(0, 0.25f).From(new Vector2(Screen.width * 1.2f, coinContainer.anchoredPosition.y)));
            foreach (RectTransform rect in buttons)
            {
                rect.localScale = Vector3.zero;
                sq.Append(rect.DOScale(Vector3.one, 0.2f));
            }
        }

        public void ReplayOnClicked()
        {
            gameObject.SetActive(false);
            UIController.Instance.ReplayOnClick();
        }

        public void NextOnClicked()
        {
            gameObject.SetActive(false);
            UIController.Instance.NextGame();
        }
    }
}