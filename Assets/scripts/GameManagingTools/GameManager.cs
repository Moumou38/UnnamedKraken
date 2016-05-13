using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 
    
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.


    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
                
            //if not, set instance to this
            instance = this;
            
        //If instance already exists and it's not this:
        else if (instance != this)
                
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    
            
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
            
            
        //Call the InitGame function to initialize the first level 
        InitGame();

        m_UIManager = GetComponent<UIManager>(); 
        
        
    }

    //Initializes the game for each level.
    void InitGame()
    {
        m_states.Add(GameStateEnum.MAIN_MENU, new MainMenuState());  
        m_states.Add(GameStateEnum.IN_GAME, new InGameState());  
        m_states.Add(GameStateEnum.LOADING, new LoadingState());  
        m_states.Add(GameStateEnum.PAUSE_MENU, new PauseState());  
        m_states.Add(GameStateEnum.MAP_MENU, new MapMenu());  
        m_states.Add(GameStateEnum.SKILL_MENU, new SkillMenu());

        LoadingState loading = m_states[GameStateEnum.LOADING] as LoadingState;
        loading.onLoaded += OnLoaded; 

        foreach (IGameState r in m_states.Values)
        {
            RootState RS = r as RootState; 
            RS.onChangeState += HandleChangeState; 
        }

        m_currentState = m_states[GameStateEnum.MAIN_MENU];
        m_currentState.start(); 

    }
        
    public void Play()
    {
        StartCoroutine(GoToLoadingState()); 
    }

    IEnumerator GoToLoadingState()
    {
        yield return (StartCoroutine(m_UIManager.FadeIn(2f)));
        HandleChangeState(GameStateEnum.LOADING);
        yield return (StartCoroutine(m_UIManager.FadeOut(2f)));
        yield return null; 
    }
        
    //Update is called every frame.
    void Update()
    {
        if (m_currentState != null)
            m_currentState.run();
            
    }

    void OnLoaded(LevelManager iManager)
    {
        m_currentSceneManager = iManager; 

    }

    void HandleChangeState(GameStateEnum iState)
    {
        if (m_currentState != null)
        {
            m_currentState.stop();
            m_currentState = m_states[iState];
            m_currentState.start(); 
        }


        //switch (iState)
        //{
        //    case GameStateEnum.MAIN_MENU:
        //        break; 
        //    case GameStateEnum.IN_GAME:
        //        break; 
        //    case GameStateEnum.LOADING:
        //        break; 
        //    case GameStateEnum.MAP_MENU:
        //        break;
        //    case GameStateEnum.PAUSE_MENU:
        //        break;
        //    case GameStateEnum.SKILL_MENU:
        //        break; 
        //}
    }

    public IGameState getCurrentState()
    {
        return m_currentState; 
    }

    LevelManager m_currentSceneManager;
    Dictionary<string, LevelManager> m_levelList = new Dictionary<string, LevelManager>();
    UIManager m_UIManager; 
    Dictionary<GameStateEnum, IGameState> m_states = new Dictionary<GameStateEnum, IGameState>(); 
    IGameState m_currentState;
}