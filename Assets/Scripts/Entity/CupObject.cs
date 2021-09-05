using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace MyGame.Pong.Object
{
    public class CupObject : MonoBehaviour
    {
        public Image niceShotPic;
        public Sprite[] previewNiceImage;

        public ParticleSystem liquidInCup;
        public Rigidbody2D rb;

        public SpriteRenderer sr;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            SetSkin();
        }

        public void SetSkin()
        {
            if (GameManager.Instance)
            {
                Sprite s = GameManager.Instance.data.cupsSkin[Utils.CUP];
                if (s != null)
                {
                    sr.sprite = s;
                }
            }
        }

        public void StartAnimation()
        {
            niceShotPic.rectTransform.localScale = Vector3.zero;
            niceShotPic.color = Color.white;
            niceShotPic.sprite = previewNiceImage[Random.Range(0, previewNiceImage.Length)];
            niceShotPic.SetNativeSize();

            Sequence sq = DOTween.Sequence();
            sq.Join(niceShotPic.rectTransform.DOScale(Vector3.one, 0.3f))
                .Join(niceShotPic.rectTransform.DOLocalMoveY(120f, 0.3f).From(Vector3.zero))
                .Append(niceShotPic.DOFade(0, 0.3f).SetDelay(0.5f))
                .Append(transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => gameObject.SetActive(false)));

            UI.UIController.Instance.UpdateTarget();
            return;
            StartCoroutine(CheckAngleLose(() =>
            {
                if (!isLoseCup)
                {
                    //Check if liquid not over cup
                   
                }
            }));

            
        }

        private bool checking = false;
        private bool isLoseCup = false;
        private IEnumerator CheckAngleLose(System.Action ac = null)
        {
            isLoseCup = false;
            yield return new WaitForSeconds(4f);

            if (GameManager.Instance.State != STATE.WIN && rb.velocity.magnitude >= rb.mass * 0.8f)
            {
                /// 0.1736 - 0.5
                checking = true;
                float zAngle = Mathf.Abs( Mathf.Cos(transform.eulerAngles.z) );

                Debug.Log("z Angle: " + zAngle);
                // Max 80 Min -80
                if (zAngle >= 0.12f && zAngle <= 1f)
                {
                    isLoseCup = true;
                    Object.GameManager.Instance.State = STATE.LOSE;
                }
                ac?.Invoke();

                checking = false;
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Ball"))
            {
                StartCoroutine(CheckAngleLose());
            }
        }


        public void CupLose()
        {
            liquidInCup.Play();
        }
    }
}