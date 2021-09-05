using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace MyGame.Pong.Object
{
    public class Ball : MonoBehaviour
    {
        public float force;
        public Projectile projectile;
        public int dotsNumber;
        public Rigidbody2D rb;
        private Vector2 startPos;
        private Camera cam;
        public ParticleSystem particle;

        public GameObject colliderObject;
        private bool isReleasedBall = false;
        public SpriteRenderer sr;

        public void SetSkin()
        {
            if (GameManager.Instance)
            {
                Sprite s = GameManager.Instance.data.ballsSkin[Utils.BALL];
                if (s != null)
                {
                    sr.sprite = s;
                }
                transform.eulerAngles = Vector3.zero;
            }
        }


        private void OnEnable()
        {
            cam = Camera.main;
            projectile.SetActive(false);
            projectile.PrepareDots(dotsNumber);
            SetKinematic(true);
            transform.DOScale(Vector3.one, 0.25f).From(Vector3.zero);
            isReleasedBall = false;
        }
        public void BallUpdate()
        {
        }


        public void OnClickStart()
        {
            if (isReleasedBall) return;

            SetKinematic(true);
            startPos = cam.ScreenToWorldPoint(Input.mousePosition);
            projectile.SetActive(true);
        }

        public void OnClick()
        {
            if (isReleasedBall) return;

            Vector2 endPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (startPos - endPos);
            projectile.UpdatePos(transform.position, direction * force);
        }

        public void EndClick()
        {
            if (isReleasedBall) return;

            Vector2 endPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (startPos - endPos);
            AddForce(direction);
            projectile.SetActive(false);

            isReleasedBall = true;

            //Began Event
            GameMaster.AddNewBall?.Invoke();
            /*
             Chua 1 ham Add New Ball
             for 
                ball.Tao();
             */
        }

        public void SetKinematic(bool isKinematic)
        {
            rb.isKinematic = isKinematic;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            if (isKinematic)
            {
                colliderObject.SetActive(false);
            } else
            {
                colliderObject.SetActive(true);
            }
        }

        public void AddForce(Vector2 direction)
        {
            SetKinematic(false);
            rb.velocity = (direction * force);
        }
        private float beforeTimeParticle = 0;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (Time.time - beforeTimeParticle > 0.2f)
            {
                ParticleSystem effect = PoolingSystem.Instance.GetCollideEffect();
                effect.gameObject.transform.position = collision.contacts[0].point;
                effect.gameObject.SetActive(true);

                SoundManager.CollideWoodSound();

                beforeTimeParticle = Time.time;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Star"))
            {
                Debug.Log("Star Trigger");

                ParticleSystem effect = PoolingSystem.Instance.GetStarEffect();
                effect.gameObject.transform.position = transform.position;
                effect.gameObject.SetActive(true);
                SoundManager.CollideStarSound();

                collision.GetComponent<Collider2D>().enabled = false;
                collision.transform.DOScale(Vector3.zero, 0.2f)
                    .OnComplete(() =>
                    {
                        UI.UIController.Instance.UpdateTarget();
                    });
            } else if (collision.CompareTag("Saw"))
            {
                colliderObject.SetActive(false);

                ParticleSystem effect = PoolingSystem.Instance.GetExplosion();
                effect.gameObject.transform.position = transform.position;
                effect.gameObject.SetActive(true);

                //SetKinematic(true);
                rb.velocity = -rb.velocity * 0.3f;
                transform.DOScale(Vector3.zero, 0.1f);
                SoundManager.ExplosionBall();
            }

        }
    }
}
