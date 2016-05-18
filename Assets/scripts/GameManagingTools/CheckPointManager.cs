using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPointManager {

    public CheckPointManager()
    {
        
    }

    public void initCheckpoints(CheckPoint[] iCheckPointList)
    {          
        foreach (CheckPoint checkpoint in iCheckPointList)
        {
            m_CheckPointList.Add(checkpoint);
            if(checkpoint.m_startCheckPoint)
            {
                m_startCheckPoint = checkpoint;
            }
            checkpoint.onCheckPoint += onCurrentCheckPoint; 
        }
        
    }

    public CheckPoint getCheckPoint(int id)
    {
        foreach(CheckPoint c in m_CheckPointList)
        {
            if (c.m_id == id)
                return c; 
        }
        return null;
    }

    void onCurrentCheckPoint(CheckPoint iCurrentCheckpoint)
    {
        if(m_currentCheckpoint != null)
        {
            m_currentCheckpoint.switchActivity();
        }
        m_currentCheckpoint = iCurrentCheckpoint;
        m_currentCheckpoint.switchActivity();

        if (onCheckPoint != null)
            onCheckPoint(iCurrentCheckpoint); 
    }

    public CheckPoint getStartCheckPoint()
    {
        return m_startCheckPoint; 
    }

    public delegate void OnCheckPoint(CheckPoint iCheckPoint);
    public event OnCheckPoint onCheckPoint;
    CheckPoint m_startCheckPoint = null;
    private CheckPoint m_currentCheckpoint; 
    private List<CheckPoint> m_CheckPointList = new List<CheckPoint>();
}
