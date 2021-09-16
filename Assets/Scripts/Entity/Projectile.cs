using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Pong.Object
{
    public class Projectile : MonoBehaviour
    {
        private List<Transform> projectiles;
        public GameObject dotPrefab;
        private int dotsNumber;
        private float deltaTime = 0.15f;

        private void Start()
        {
            deltaTime = 0.08f;
        }

        public void PrepareDots(int dotsNumber)
        {
            if (dotsNumber == this.dotsNumber) return;
            projectiles = new List<Transform>();
            this.dotsNumber = dotsNumber;
            for (int i = 0; i < dotsNumber; i++)
            {
                Transform dot = Instantiate(dotPrefab, transform).GetComponent<Transform>();
                dot.localScale = Vector3.one * 0.6f;
                projectiles.Add(dot);
            }
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void UpdatePos(Vector2 ballPos, Vector2 force)
        {
            Vector2 pos = new Vector2();
            float timeStamp = deltaTime;
            for (int i = 0; i < dotsNumber; i++)
            {
                
                pos.x = (ballPos.x + force.x * timeStamp);
                pos.y = (ballPos.y + force.y * timeStamp - (Physics2D.gravity.magnitude * 3 * timeStamp * timeStamp) / 2f);
                projectiles[i].position = pos;
                timeStamp += deltaTime * 0.5f;
            }
        }
    }
}