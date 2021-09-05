using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MyGame.Pong.Object;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace MyGame.Pong
{
    public class GameController : MonoBehaviour
    {
        public GameplayMode mode;
        public int NumberBall = 3;
        public Ball ballPrefab;
        public Ball ballCurrent;
        public Transform ballPos;

        [Space(10)]
        [Header("Mode BEER")]
        public int numberOfBeers;
        [Space(10)]
        [Header("Mode Star")]
        public int numberOfStars;
        [Space(10)]
        [Header("Mode Break Glass")]
        public int numberOfGlasses;

        private void Start()
        {
            ballCurrent = PoolingSystem.Instance.GetBall();
            ballCurrent.transform.position = ballPos.position;
            ballCurrent.gameObject.SetActive(true);
            //List.Add
            GameMaster.AddNewBall += this.AddNewBallHandleEvent;

            UI.UIController.Instance.PrepareUI();
            UI.UIController.Instance.SetBallInGame(NumberBall);
            NumberBall--;
        }

        private void AddNewBallHandleEvent()
        {
            ballCurrent = null;
            StartCoroutine(WaitForInstantiate());
        }

        IEnumerator WaitForInstantiate()
        {
            UI.UIController.Instance.BallOutUI();
            yield return new WaitForSeconds(0.7f);
            if (NumberBall > 0)
            {
                //ballCurrent = Instantiate(ballPrefab);
                ballCurrent = PoolingSystem.Instance.GetBall();
                ballCurrent.transform.position = ballPos.position;
                ballCurrent.gameObject.SetActive(true);
                NumberBall--;
            }
        }
        private bool canDrag = true;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // If click UI => not use ball
#if UNITY_EDITOR
                if (EventSystem.current.IsPointerOverGameObject()) canDrag = false;
#else
                if (EventSystem.current.IsPointerOverGameObject(0)) canDrag = false;
#endif
                if (canDrag && ballCurrent)
                    ballCurrent.OnClickStart();
            }
            if (Input.GetMouseButton(0))
            {
                if (canDrag && ballCurrent)
                    ballCurrent.OnClick();
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (canDrag && ballCurrent)
                    ballCurrent.EndClick();
                canDrag = true;
            }
        }

        private void OnDestroy()
        {
            //List.Remove(this)
            GameMaster.AddNewBall -= this.AddNewBallHandleEvent;
        }


#if UNITY_EDITOR
        [ButtonEditor]
        public void SaveLevel()
        {
            string name = gameObject.name + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(gameObject, "Assets/Resources/" + name);
        }
#endif
    }
}
