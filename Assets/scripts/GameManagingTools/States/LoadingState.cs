using UnityEngine;
using System.Collections;

public class LoadingState : RootState
{
    public override void start()
    {
        Debug.Log("Entered : Loading");
    }

    private IEnumerator LoadGame()
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
}
