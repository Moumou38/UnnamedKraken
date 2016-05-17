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

    protected void callOnLoaded(LevelManager iManager)
    {
        if (onLoaded != null)
            onLoaded(iManager); 
    }

    protected void callCallSavingModule(bool load)
    {
        if (callSavingModule != null)
            callSavingModule(load);
    }

    public delegate void OnChangeState(GameStateEnum iState);
    public event OnChangeState onChangeState;
    public delegate void OnLoaded(LevelManager iManager);
    public event OnLoaded onLoaded;
    public delegate void CallSavingModule(bool load);
    public event CallSavingModule callSavingModule;

}
