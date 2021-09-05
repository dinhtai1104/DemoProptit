using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Object
{
    private static T _ins;
    public static T Instance
    {
        get
        {
            if (_ins == null)
            {
                _ins = FindObjectOfType<T>();
            }

            return _ins;
        }
    }

    #region Unity Methods

    protected virtual void Awake()
    {
        SetInstance();
    }


    #endregion

    #region Public Methods

    public static bool Exists()
    {
        return _ins != null;
    }

    public bool SetInstance()
    {
        if (_ins != null && _ins != gameObject.GetComponent<T>())
        {
            Debug.LogWarning("[SingletonComponent] Instance already set for type " + typeof(T));
            return false;
        }

        _ins = gameObject.GetComponent<T>();

        return true;
    }

    private void OnDestroy()
    {
        _ins = null;
    }
    #endregion
}
