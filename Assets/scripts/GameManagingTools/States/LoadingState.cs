using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingState : RootState
{
    public override void start()
    {
        Debug.Log("Entered : Loading");
        // Fade black ?
        SceneManager.LoadScene("LoadingScene"); 
    }

    private IEnumerator LoadGame(string iSceneToLoad)
    {
        
        // STEP 1
        // check if savefile. If yes : load level in save file if not, default start

        // STEP 2

        // STEP 3

        // STEP 4



        yield return null; 
    }

    public override void stop()
    {


    }
    public override void run()
    {


    }

    string m_levelToLoad = "";
    public delegate void OnLoaded();
    public event OnLoaded onLoaded; 
}
