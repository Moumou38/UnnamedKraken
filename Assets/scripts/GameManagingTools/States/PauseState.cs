using UnityEngine;
using System.Collections;

public class PauseState : RootState
{
    public override void start()
    {
        Debug.Log("Entered : PauseState");
        m_timeScame = Time.timeScale; 
        Time.timeScale = 0; 
    }
    public override void stop()
    {
        Time.timeScale = m_timeScame; 

    }
    public override void run()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changeState(GameStateEnum.IN_GAME);
        }

    }

    float m_timeScame = 0f; 
}
