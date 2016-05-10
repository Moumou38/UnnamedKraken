using UnityEngine;
using System.Collections;

public class InGameState : RootState
{
    public override void start()
    {
        Debug.Log("Entered : InGame");
    }
    public override void stop()
    {


    }
    public override void run()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changeState(GameStateEnum.PAUSE_MENU); 
        }

    }
}
