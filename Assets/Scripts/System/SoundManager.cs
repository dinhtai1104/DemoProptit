using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Pong.Object
{
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioSource backgroundAudio;
        public AudioSource sfx;
        public AudioClip
            collideBall,
            collideStar,
            winSfx,
            loseSfx,
            buttonSfx,
            starWin,
            explosionBall,
            ballInPong;
        public AudioClip[] pongCollide;
        public void PlayAudio(AudioClip au)
        {
            if (Utils.SOUND)
            {
                if (au != null)
                {
                    sfx.PlayOneShot(au);
                }
            }
        }

        private void Start()
        {
            GameMaster.MusicClick += this.MusicClickHandleEvent;
            GameMaster.SoundClick += this.SoundClickHandleEvent;
        }

        private void OnDestroy()
        {
            GameMaster.MusicClick -= this.MusicClickHandleEvent;
            GameMaster.SoundClick -= this.SoundClickHandleEvent;
        }

        
        private void SoundClickHandleEvent()
        {
            if (Utils.SOUND)
            {
                sfx.mute = false;
            }
            else
            {
                sfx.mute = true;
            }
        }

        private void MusicClickHandleEvent()
        {
            if (Utils.MUSIC)
            {
                backgroundAudio.mute = false; 
            } else
            {
                backgroundAudio.mute = true;
            }
        }

        public static void BallInPongSound()
        {
            Instance.PlayAudio(Instance.ballInPong);
        }

        public static void CollideWoodSound()
        {
            Instance.PlayAudio(Instance.pongCollide[UnityEngine.Random.Range(0, 2)]);
        }
        public static void StarWinAppear()
        {
            Instance.PlayAudio(Instance.starWin);
        }
        public static void CollideStarSound()
        {
            Instance.PlayAudio(Instance.collideStar);
        }

        public static void ButtonSound()
        {
            Instance.PlayAudio(Instance.buttonSfx);
        }

        public static void WinSound()
        {
            Instance.PlayAudio(Instance.winSfx);
        }

        public static void LoseSound()
        {
            Instance.PlayAudio(Instance.loseSfx);
        }

        internal static void ExplosionBall()
        {
            Instance.PlayAudio(Instance.explosionBall);
        }
    }
}