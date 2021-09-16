using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MyGame.Pong.UI
{
    public class PauseComponent : MonoBehaviour
    {
        public Text levelName;
        public RectTransform[] btns;

        public Image sound, music;
        public Sprite soundOff, soundOn, musicOff, musicOn;

        public void OnShow()
        {
            levelName.text = "LEVEL " + Utils.LEVELCURRENT.ToString("00");
            gameObject.SetActive(true);

            Sequence sq = DOTween.Sequence();
            foreach (RectTransform rect in btns)
            {
                rect.localScale = Vector3.zero;
                sq.Append(rect.DOScale(Vector3.one, 0.1f));
            }

            SetInfoSOUND_MUSIC();
        }


        private void SetInfoSOUND_MUSIC()
        {
            sound.sprite = Utils.SOUND ? soundOn : soundOff;
            music.sprite = Utils.MUSIC ? musicOn : musicOff;
        }

        private void OnClose(System.Action callback = null)
        {
            Sequence sq = DOTween.Sequence();
            UIController.Instance.winComponent.gameObject.SetActive(false);
            UIController.Instance.loseComponent.gameObject.SetActive(false);

            foreach (RectTransform rect in btns)
            {
                sq.Append(rect.DOScale(Vector3.zero, 0.1f));
            }
            sq.OnComplete(() =>
            {
                callback?.Invoke();
                gameObject.SetActive(false);
            });
        }

        public void CloseOnClicked()
        {
            OnClose();
        }

        public void HomeOnClicked()
        {
            UIController.Instance.HomeOnClicked();
        }

        public void MusicOnClicked()
        {
            Utils.MUSIC = !Utils.MUSIC;
            music.sprite = Utils.MUSIC ? musicOn : musicOff;
        }

        public void SoundOnClicked()
        {
            Utils.SOUND = !Utils.SOUND;
            sound.sprite = Utils.SOUND ? soundOn : soundOff;
        }

        public void ChapterOnClicked()
        {
            HomeOnClicked();
        }
    }
}