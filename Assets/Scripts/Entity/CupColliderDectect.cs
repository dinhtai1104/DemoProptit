using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyGame.Pong.Object
{
    public class CupColliderDectect : MonoBehaviour
    {
        public CupObject parent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Ball")
            {
                parent.StartAnimation();
                SoundManager.BallInPongSound();
                collision.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}