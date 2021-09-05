using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace MyGame.Pong.UI
{
    public class UIController : Singleton<UIController>
    {
        public List<Image> listBallsUI;
        private int NumberBall = 0;
        public Sprite ballOn, ballOff;
        public Text controllerModeText;

        [HideInInspector]
        public GameplayMode playMode;
        public int target = 0;
        public int current = 0;


        [Header("Main UI")]
        public Text difficultLevel; // Hard, Easy, Evil...
        public Text levelText;  // Level 11....
        public WinComponent winComponent;
        public LoseComponent loseComponent;
        public PauseComponent pauseComponent;
        public RectTransform InGameUI;


        [Header("Home")]
        public HomeUI homeUI;

        private void Start()
        {
           // Utils.LoadData();
            GameMaster.JoinGame += this.JoinGameHandleEvent;
            GameMaster.ReplayGame += this.ReplayGameHandleEvent;
        }

        private void ReplayGameHandleEvent()
        {
            InGameUI.gameObject.SetActive(false);
            winComponent.gameObject.SetActive(false);
            loseComponent.gameObject.SetActive(false);
            pauseComponent.gameObject.SetActive(false);
            InGameUI.gameObject.SetActive(true);
        }

        internal int GetBall()
        {
            return NumberBall;
        }

        private void JoinGameHandleEvent()
        {
            InGameUI.gameObject.SetActive(false);
            winComponent.gameObject.SetActive(false);
            loseComponent.gameObject.SetActive(false);
            pauseComponent.gameObject.SetActive(false);
            InGameUI.gameObject.SetActive(true);
        }

        public void HomeOnClicked()
        {
            Object.PoolingSystem.Instance.ResetPool();
            Object.GameManager.Instance.QuitLevel();
            InGameUI.gameObject.SetActive(false);
            winComponent.gameObject.SetActive(false);
            loseComponent.gameObject.SetActive(false);
            pauseComponent.gameObject.SetActive(false);
            homeUI.gameObject.SetActive(true);
        }

        public void PlayOnClicked()
        {
            homeUI.gameObject.SetActive(false);
            InGameUI.gameObject.SetActive(true);
            Object.GameManager.Instance.LoadLevel();
        }

        #region GamePlay


        public void PauseOnClicked()
        {
            pauseComponent.OnShow();
        }


        public void PrepareUI()
        {
            levelText.text = "LEVEL " + Utils.LEVELCURRENT.ToString("00");
            difficultLevel.text = "EASY";
            foreach(Image image in listBallsUI)
            {
                image.gameObject.SetActive(false);
            }
        }
        public void SetBallInGame(int number)
        {
            this.NumberBall = number;
            for (int i = 0; i < number; i++)
            {
                listBallsUI[i].sprite = ballOn;
                listBallsUI[i].gameObject.SetActive(true);
            }
        }

        public void BallOutUI()
        {
            if (NumberBall == 0) return;

            if (NumberBall > 0)
            {
                int index = NumberBall - 1;
                listBallsUI[index].rectTransform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
                {
                    listBallsUI[index].sprite = ballOff;
                    listBallsUI[index].rectTransform.DOScale(Vector3.one, 0.2f);
                }); 
                NumberBall--;
            }
            if (NumberBall == 0)
            {
                Debug.Log("Checking.......====");
                StartCoroutine(CheckingLoseGameByBall());
            }
        }
        /// <summary>
        /// Checking Lose game If Ball is end
        /// </summary>
        /// <returns></returns>
        private IEnumerator CheckingLoseGameByBall()
        {
            yield return new WaitForSeconds(3f);
            if (Object.GameManager.Instance.State != Object.STATE.WIN)
            {
                Object.GameManager.Instance.State = Object.STATE.LOSE;
            }
        }

        internal void NextGame()
        {
            StopAllCoroutines();
            Utils.LEVELCURRENT++;
            Object.PoolingSystem.Instance.ResetPool();
            Object.GameManager.Instance.LoadLevel();
            
        }

        public void SetControllerModeText(int target)
        {
            current = 0;
            this.target = target;
            string valueText = "";
            if (playMode == GameplayMode.BEER)
            {
                valueText = "Pong the Beer\n" + 0 + "/" + target + " BEER";
            }
            else if (playMode == GameplayMode.STAR)
            {
                valueText = "Get the Stars\n" + 0 + "/" + target + " STAR";
            }
            else if (playMode == GameplayMode.BREAK)
            {
                valueText = "Break the Glasses\n" + 0 + "/" + target + " GLASS";
            }
            controllerModeText.text = valueText;
        }

        public void SetPlayMode(GameplayMode mode)
        {
            playMode = mode;
        }


        public void UpdateTarget()
        {
            current++;
            string valueText = "";
            if (playMode == GameplayMode.BEER)
            {
                valueText = "Pong the Beer\n" + current + "/" + target + " BEER";
            }
            else if (playMode == GameplayMode.STAR)
            {
                valueText = "Get the Stars\n" + current + "/" + target + "STAR";
            }
            else if (playMode == GameplayMode.BREAK)
            {
                valueText = "Break the Glasses\n" + current + "/" + target + "GLASS";
            }
            controllerModeText.text = valueText;

            if (current == target)
            {
                Debug.Log("WINNNNNN");

                Object.GameManager.Instance.State = Object.STATE.WIN;

            }
        }

        private IEnumerator CallDelayAction(float time, System.Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }

        public void ReplayOnClick()
        {
            StopAllCoroutines();
            Object.PoolingSystem.Instance.ResetPool();
            Object.GameManager.Instance.ReplayGame();
        }

        public void PauseOnClick()
        {
            Debug.Log("Pause onclick");
        }

        public void HintOnClick()
        {
            Debug.Log("Hint Onclick");
        }
        public void SkipOnClick()
        {
            Debug.Log("Skip Onclick");
        }
        #endregion
    }
}