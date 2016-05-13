using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour {

    public void StartLoadCoroutine(string iSceneToLoad)
    {
        StartCoroutine(StartLoading(iSceneToLoad)); 

    }
    public IEnumerator StartLoading(string iSceneToLoad)
    {
        yield return(StartCoroutine(LoadMasterScene(iSceneToLoad)));
        yield return null; 
    }
    private IEnumerator LoadMasterScene(string iSceneToLoad)
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
                    if (m_LoadingText != null)
                        m_LoadingText.text = "Loading : " + s; 

                    Debug.Log("Loading : " + s);
                    yield return new WaitForEndOfFrame();
                }
            }

        }

        if (onSceneLoaded != null)
        {
            
            yield return new WaitForSeconds(2f);
            onSceneLoaded(manager);
        }

        SceneManager.UnloadScene("LoadingScene");

        
        yield return null;
    }

    public Text m_LoadingText; 
    public delegate void OnSceneLoaded(LevelManager iSceneManager);
    public event OnSceneLoaded onSceneLoaded; 
}
