using prototype2;
using prototype3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameBehaviour : LC.Behaviour //inherits from
{
    //unquie to this project
    protected static UIManager _UI { get { return UIManager.Instance; } }
    protected static P1UIManager _P1UI { get { return P1UIManager.Instance; } }
    protected static P1GameManager _P1GM { get { return P1GameManager.Instance; } }
    protected static PlayerControllerP2 _PC2 { get { return PlayerControllerP2.Instance; } }
    protected static GameStateManager _GAMESTATE { get { return GameStateManager.Instance; } }
    protected static EnemyManagerP2 _EM2 { get { return EnemyManagerP2.Instance; } }
    protected static EquationGenerator _EG { get { return EquationGenerator.Instance; } }
    protected static MathSpawning _MS { get { return MathSpawning.Instance; } }
    protected static Movment _PC4 { get { return Movment.Instance; } }
    protected static UISkillTree _UIST { get { return UISkillTree.Instance; } }
    protected static GameManagerP2 _GM2 { get { return GameManagerP2.Instance; } }
    protected static prototype3.UIManager _UI3 { get { return prototype3.UIManager.Instance; } }
    protected static prototype4.GameManager _GM4 { get { return prototype4.GameManager.Instance; } }

    public enum Gamestate { Title, Pause, Instructions, Playing, GameOver }



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
