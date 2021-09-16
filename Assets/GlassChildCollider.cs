using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Pong.Object
{
    public class GlassChildCollider : MonoBehaviour
    {
        public GlassObject parent;



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Untagged"))
            {
                parent.BreakGlass();
            }
        }
    }
}