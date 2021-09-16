using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Pong.Object
{
    public class PieceGlass : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1, 1), Random.Range(0, 1));
        }

    }
}