using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameBehaviour : LC.Behaviour //inherits from
{
    //unquie to this project
    protected static UIManager _UI { get { return UIManager.Instance; } }
    protected static P1UIManager _P1UI { get { return P1UIManager.Instance; } }


}
//
// Instanced GameBehaviour
//
public class GameBehaviour<T> : GameBehaviour where T : GameBehaviour
{
    public bool dontDestroy;
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameBehaviour<" + typeof(T).ToString() + "> not instantiated!\nNeed to call Instantiate() from " + typeof(T).ToString() + "Awake().");
            return _instance;
        }
    }
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            if (dontDestroy) DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    //
    // Instantiate singleton
    // Must be called first thing on Awake()
    protected bool Instantiate()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Instance of GameBehaviour<" + typeof(T).ToString() + "> already exists! Destroying myself.\nIf you see this when a scene is LOADED from another one, ignore it.");
            DestroyImmediate(gameObject);
            return false;
        }
        _instance = this as T;
        return true;
    }
}
