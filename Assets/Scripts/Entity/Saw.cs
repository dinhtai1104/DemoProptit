using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace MyGame.Pong.Object
{
    public class Saw : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            transform.DOLocalRotate(-Vector3.forward * 360, 1.5f).SetRelative().SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        }

    }
}