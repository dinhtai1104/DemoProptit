using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Pong.Object
{
    public class Teleport : MonoBehaviour 
    {
        public Teleport next;
        // Start is called before the first frame update
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ball"))
            {
                if (next != null)
                {
                    collision.transform.parent.transform.position = next.transform.position;
                }
            }
        }
    }
}