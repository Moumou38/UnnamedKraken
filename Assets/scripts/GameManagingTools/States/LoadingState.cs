using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingState : RootState
{

    AsyncOperation m_async; 

    public override void start()
    {
        Debug.Log("Entered : Loading");
        // Fade black ?
        m_async = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Single);
        
        
    }

    void onSceneLoaded(LevelManager iManager)
    {
        // Scene is open, time to initialise evenything

        if (onLoaded != null)
            onLoaded(iManager); 
    }  


    public override void stop()
    {


    }
    public override void run()
    {
        if (m_async != null && m_async.isDone && !trigger)
        {
            trigger = true; 
            Loader loader = GameObject.FindObjectOfType<Loader>();

            if (loader == null)
            {
                Debug.Log("Loader not found");
                loader.onSceneLoaded += onSceneLoaded;
            }
            else
            {
                if (onLoadingSceneOpen != null)
                    onLoadingSceneOpen(); 
                loader.StartLoadCoroutine("ReefBarrier");
            }
        }

    }
    bool trigger = false; 
    string m_levelToLoad = "";
    public delegate void OnLoaded(LevelManager iManager);
    public event OnLoaded onLoaded;
    public delegate void OnLoadingSceneOpen();
    public event OnLoadingSceneOpen onLoadingSceneOpen; 

}
