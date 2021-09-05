using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace MyGame.Pong.UI
{
    public class SettingComponent : MonoBehaviour
    {
        public Image sound, music;
        public Sprite soundOff, soundOn, musicOff, musicOn;

        public RectTransform[] btns;

        private void OnEnable()
        {
            sound.sprite = Utils.SOUND ? soundOn : soundOff;
            music.sprite = Utils.MUSIC ? musicOn : musicOff;

            Sequence sq = DOTween.Sequence();

            foreach (RectTransform rect in btns)
            {
                rect.localScale = Vector3.zero;
                sq.Append(rect.DOScale(Vector3.one, 0.1f));
            }
            
        }

        public void HomeOnClicked()
        {
            Sequence sq = DOTween.Sequence();

            foreach (RectTransform rect in btns)
            {
                sq.Append(rect.DOScale(Vector3.zero, 0.1f));
            }

            sq.OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
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
    }
}