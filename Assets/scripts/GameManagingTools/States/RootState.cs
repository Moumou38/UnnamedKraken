using UnityEngine;
using System.Collections;

public class RootState : IGameState {

    public virtual void start()
    {

    }
    public virtual void stop()
    {

    }
    public virtual void run()
    {

    }

    protected void changeState(GameStateEnum iState)
    {
        if (onChangeState != null)
            onChangeState(iState); 
    }

    public delegate void OnChangeState(GameStateEnum iState);
    public event OnChangeState onChangeState; 

}
