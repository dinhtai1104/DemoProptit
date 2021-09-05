using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MyGame.Pong.UI
{
    public class LoseComponent : MonoBehaviour
    {
        public RectTransform[] btns;
        public Text levelName;

        public void OnShow()
        {
            Object.SoundManager.LoseSound();

            gameObject.SetActive(true);
            Sequence sq = DOTween.Sequence();
            foreach (RectTransform rect in btns)
            {
                rect.localScale = Vector3.zero;
                sq.Append(rect.DOScale(Vector3.one, 0.2f));
            }
            levelName.text = "LEVEL " + Utils.LEVELCURRENT.ToString("00");
        }

        private void OnClose(System.Action callback = null)
        {
            Sequence sq = DOTween.Sequence();
            foreach (RectTransform rect in btns)
            {
                sq.Append(rect.DOScale(Vector3.zero, 0.2f));
            }
            sq.OnComplete(() =>
            {
                gameObject.SetActive(false);
                callback?.Invoke();
            });
        }

        public void ReplayOnClicked()
        {
            OnClose(()=>
            {
                UIController.Instance.ReplayOnClick();
            });
        }

        public void SkipOnClicked()
        {
            OnClose(()=>
            {
                UIController.Instance.NextGame();
            });
        }
    }
}