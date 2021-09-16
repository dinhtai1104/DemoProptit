using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Pong.Object
{
    public class GlassObject : MonoBehaviour
    {
        public float forceCanBreakGlass = 6f;
        public Transform breakParent;
        public Transform waterParent;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.relativeVelocity.magnitude > forceCanBreakGlass)
            {
                BreakGlass(); 
            } 
        }

        public void BreakGlass()
        {
            UI.UIController.Instance.UpdateTarget();
            SoundManager.BreakGlassSound();

            breakParent.transform.parent = transform.parent;
            breakParent.gameObject.SetActive(true);
            waterParent.transform.parent = transform.parent;
            //waterParent.gameObject.layer = LayerMask.NameToLayer("Water");
            for (int i = 0; i < waterParent.childCount; i++)
            {
                waterParent.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Water");
            }

            Destroy(gameObject);
        }
    }
}