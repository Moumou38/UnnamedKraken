using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
     

    public void NewGame()
    {
        StartCoroutine(GoToLoadingState(LoadingCommand.NEW_GAME));
    }

    public void Continue()
    {
        StartCoroutine(GoToLoadingState(LoadingCommand.LOAD_SAVE));
    }

    IEnumerator GoToLoadingState(LoadingCommand iCommand)
    {
        yield return (StartCoroutine(m_UIManager.FadeIn(5f)));
        HandleChangeState(GameStateEnum.LOADING);
        if(m_currentState == m_states[GameStateEnum.LOADING])
        {
            LoadingState loading = m_states[GameStateEnum.LOADING] as LoadingState;
            loading.setLoadingCommand(iCommand);
        }


        yield return null;
    }

    public IGameState getCurrentState()
    {
        return m_currentState;
    }

    public void initPlayer()
    {

    }


    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
                
            //if not, set instance to this
            Instance = this;
            
        //If instance already exists and it's not this:
        else if (Instance != this)
                
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    
            
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
            
         m_UIManager = GetComponent<UIManager>();      
        //Call the InitGame function to initialize the first level 
        InitGame();

          
        
    }

    //Initializes the game for each level.
    void InitGame()
    {
        // check for save : 
        m_saveHandler = new SaveLoadHandler();
        if (m_saveHandler.CheckSave())
            m_UIManager.enableButton(UIManager.UIElementEnum.MAIN_MENU, "Continue");
        else
            m_UIManager.disableButton(UIManager.UIElementEnum.MAIN_MENU, "Continue"); 

        m_states.Add(GameStateEnum.MAIN_MENU, new MainMenuState());  
        m_states.Add(GameStateEnum.IN_GAME, new InGameState());  
        m_states.Add(GameStateEnum.LOADING, new LoadingState());  
        m_states.Add(GameStateEnum.PAUSE_MENU, new PauseState());  
        m_states.Add(GameStateEnum.MAP_MENU, new MapMenu());  
        m_states.Add(GameStateEnum.SKILL_MENU, new SkillMenu());

        foreach (IGameState r in m_states.Values)
        {
            RootState RS = r as RootState; 
            RS.onChangeState += HandleChangeState;
            RS.onLoaded += OnLoaded;
            RS.callSavingModule += SaveLoad;
        }

        m_currentState = m_states[GameStateEnum.MAIN_MENU];
        m_currentState.start(); 

    }

    void SaveLoad(bool load)
    {
        if (load)
        {
            m_saveHandler.LoadData();
        }
        else
        {
            m_saveHandler.SaveData(); 
        }
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
        HandleChangeState(GameStateEnum.IN_GAME);


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

    

    public GameObject PlayerPrefab;
    LevelManager m_currentSceneManager;
    Dictionary<string, LevelManager> m_levelList = new Dictionary<string, LevelManager>();
    public UIManager m_UIManager; 
    Dictionary<GameStateEnum, IGameState> m_states = new Dictionary<GameStateEnum, IGameState>(); 
    IGameState m_currentState;
    SaveLoadHandler m_saveHandler;
}