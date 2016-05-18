using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Awake () {       
        m_checkpointManager = new CheckPointManager();
        m_checkpointManager.onCheckPoint += OnCheckPointTriggered;    
    }

    public void initCheckPoints()
    {
        CheckPoint[] tab = FindObjectsOfType<CheckPoint>(); 
        if (tab != null)
            m_checkpointManager.initCheckpoints(tab);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public CheckPoint getCheckPoint(int id)
    {
        return m_checkpointManager.getCheckPoint(id);
     }

    public CheckPoint getStartCheckPoint()
    {
        return m_checkpointManager.getStartCheckPoint();
    }

    void OnCheckPointTriggered(CheckPoint iCheckPoint)
    {
        if (onCheckPoint != null)
            onCheckPoint(iCheckPoint); 
    }

    public delegate void OnCheckPoint(CheckPoint iCheckPoint);
    public event OnCheckPoint onCheckPoint;
    CheckPointManager m_checkpointManager;
    public string m_name = "";
    public List<string> m_subScene;

}
