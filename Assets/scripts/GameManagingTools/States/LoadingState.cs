using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public enum LoadingCommand
{
    NEW_GAME,
    LOAD_SAVE,
    BACK_TO_MENU,
    NEW_LEVEL,
    NONE
}

public class LoadingState : RootState
{

    AsyncOperation m_async; 

    public void setLoadingCommand(LoadingCommand iCommand, string iLevelName ="")
    {
        switch(iCommand)
        {
            case LoadingCommand.BACK_TO_MENU:
                break;
            case LoadingCommand.LOAD_SAVE:
                break;
            case LoadingCommand.NEW_GAME:
                break;
            case LoadingCommand.NEW_LEVEL:
                break;
            case LoadingCommand.NONE:
                break;
        }
    }

    public override void start()
    {
        Debug.Log("Entered : Loading");
        // Fade black ?

        m_async = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
    }

    void onSceneLoaded(LevelManager iManager)
    {
        // Scene is open, time to initialise evenything
        GameManager.Instance.initPlayer();


        if (onLoaded != null)
            onLoaded(iManager);

        GameManager.Instance.StartCoroutine(FadeInOut());

    }

    IEnumerator FadeInOut()
    {
        yield return (GameManager.Instance.StartCoroutine(GameManager.Instance.m_UIManager.FadeIn(5f)));
        GameManager.Instance.m_UIManager.HideUIElement(UIManager.UIElementEnum.LOADING);
        yield return (GameManager.Instance.StartCoroutine(GameManager.Instance.m_UIManager.FadeOut(5f)));
        yield return null;
    }



    public override void stop()
    {


    }
    public override void run()
    {
        // wait for start
    

        if (m_async != null && m_async.isDone && !trigger)
        {
            trigger = true; 
            GameManager.Instance.StartCoroutine(FadeOutCoroutine());              
        }

    }

    IEnumerator FadeOutCoroutine()
    {
        GameManager.Instance.m_UIManager.HideUIElement(UIManager.UIElementEnum.MAIN_MENU);
        GameManager.Instance.m_UIManager.ShowUIElement(UIManager.UIElementEnum.LOADING);
        yield return (GameManager.Instance.StartCoroutine(GameManager.Instance.m_UIManager.FadeOut(5f)));

        OpenScene("ReefBarrier");

        yield return null;
    }

    IEnumerator LoadMasterScene(string iSceneToLoad)
    {
        // STEP 1
        // check if savefile. If yes : load level in save file if not, default start
        AsyncOperation async = SceneManager.LoadSceneAsync(iSceneToLoad, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(iSceneToLoad));
        LevelManager manager = GameObject.FindObjectOfType<LevelManager>();
        if (manager == null)
        {
            Debug.Log("No SubScene to Load");
        }
        else
        {
            foreach (string s in manager.m_subScene)
            {

                AsyncOperation subasync = SceneManager.LoadSceneAsync(s, LoadSceneMode.Additive);

                while (!subasync.isDone)
                {
                    Debug.Log("Loading : " + s);
                    yield return new WaitForEndOfFrame();
                }
            }

        }


        yield return new WaitForSeconds(2f);
        onSceneLoaded(manager);

        SceneManager.UnloadScene("LoadingScene");


        yield return null;
    }

    void OpenScene(string iSceneToLoad)
    {
        GameManager.Instance.StartCoroutine(LoadMasterScene(iSceneToLoad));
    }

    bool trigger = false; 
    string m_levelToLoad = "";
    LoadingCommand m_currentCommand = LoadingCommand.NONE;
    public delegate void OnLoaded(LevelManager iManager);
    public event OnLoaded onLoaded;
    public delegate void OnLoadingSceneOpen();
    public event OnLoadingSceneOpen onLoadingSceneOpen; 

}
