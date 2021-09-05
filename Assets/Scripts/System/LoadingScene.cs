using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class LoadingScene : MonoBehaviour
{
    public Image filled;
    public int TotalLevel;
    // Start is called before the first frame update
    void Start()
    {
        MyGame.Pong.Object.GameManager.TotalLevel = TotalLevel;
        filled.fillAmount = 0;
        AsyncOperation async = SceneManager.LoadSceneAsync("Play");
        async.allowSceneActivation = false;
        filled.DOFillAmount(1, 3f).OnComplete(() =>
        {
            async.allowSceneActivation = true;
        });
        Utils.LoadData();
    }
}
