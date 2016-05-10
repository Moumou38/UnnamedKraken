using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPointManager {

    public CheckPointManager()
    {
        
    }

    public void initCheckpoints(List<CheckPoint> iCheckPointList)
    {
        m_CheckPointList = iCheckPointList;

        if (m_CheckPointList != null)
        {
            foreach (CheckPoint checkpoint in m_CheckPointList)
            {
                checkpoint.onCheckPoint += onCurrentCheckPoint; 
            }
        }
    }

    void onCurrentCheckPoint(CheckPoint iCurrentCheckpoint)
    {
        m_currentCheckpoint = iCurrentCheckpoint; 
    }

    private CheckPoint m_currentCheckpoint; 
    private List<CheckPoint> m_CheckPointList;
}
