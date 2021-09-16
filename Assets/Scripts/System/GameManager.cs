using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace MyGame.Pong.Object
{
    public enum STATE { WIN, LOSE, PLAYING, NONE }
    public class GameManager : Singleton<GameManager>
    {
        public static int TotalLevel = 1;
        public int totalLevel = 100;
        private GameController levelCurrent, gameController;
        public Database data;

        private STATE state = STATE.PLAYING;
        public STATE State
        {
            get
            {
                return state;
            }
            set
            {
                STATE last = state;
                state = value;
                if (state == STATE.NONE)
                {
                    transform.DOKill();
                }
                if (last == STATE.PLAYING && state != STATE.PLAYING)
                {
                    if (state == STATE.WIN && last == STATE.PLAYING)
                    {
                        transform.DOScale(Vector3.one, 1.8f).OnComplete(() =>
                        {
                            UI.UIController.Instance.winComponent.OnShow(UI.UIController.Instance.GetBall());
                        });
                    }
                    else
                    {
                        if (state == STATE.LOSE && last == STATE.PLAYING)
                        {
                            transform.DOScale(Vector3.one, 3f).OnComplete(() =>
                            {
                                UI.UIController.Instance.loseComponent.OnShow();
                            });
                        }
                    }
                }
            }
        }


        
        private void Start()
        {
            // LoadLevel();
            GameMaster.JoinGame += this.JoinGameHandleEvent;
        }

        private void JoinGameHandleEvent()
        {
            LoadLevel();
        }

        public void QuitLevel()
        {
            if (gameController)
            {
                Destroy(gameController.gameObject);
            }
        }

        public void LoadLevel()
        {
            StopAllCoroutines();
            StartCoroutine(loadLevel());
        }

        private IEnumerator loadLevel()
        {
            State = STATE.PLAYING;
            GameMaster.LoadLevel = null;
            if (gameController)
            {
                Destroy(gameController.gameObject);
            }
            yield return null;

            levelCurrent = null;

            levelCurrent = Resources.Load<GameController>("Level " + Utils.LEVELCURRENT.ToString("00"));


            gameController = Instantiate(levelCurrent);

            int target = 0;
            if (gameController.mode == GameplayMode.BEER) // tha bong vao coc
            {
                target = gameController.numberOfBeers;
            } else if (gameController.mode == GameplayMode.STAR)
            {
                target = gameController.numberOfStars;
            } else if (gameController.mode == GameplayMode.BREAK)
            {
                target = gameController.numberOfGlasses;
            }
            UI.UIController.Instance.playMode = gameController.mode;
            UI.UIController.Instance.SetControllerModeText(target);
        }

        public void ReplayGame()
        {
            StopAllCoroutines();
            State = STATE.PLAYING;
            if (gameController)
            {
                Destroy(gameController.gameObject);
            }
            gameController = Instantiate(levelCurrent);
            int target = 0;
            if (gameController.mode == GameplayMode.BEER)
            {
                target = gameController.numberOfBeers;
            }
            else if (gameController.mode == GameplayMode.STAR)
            {
                target = gameController.numberOfStars;
            }
            else if (gameController.mode == GameplayMode.BREAK)
            {
                target = gameController.numberOfGlasses;
            }
            UI.UIController.Instance.SetControllerModeText(target);
        }

    }
}