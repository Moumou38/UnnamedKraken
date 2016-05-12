using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        m_checkpointManager = new CheckPointManager(); 


    }
	
	// Update is called once per frame
	void Update () {
	
	}

    CheckPointManager m_checkpointManager;
    public string m_name = "";
    public List<string> m_subScene;

}
