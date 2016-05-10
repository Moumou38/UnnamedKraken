using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPathManager {

    public PlayerPathManager()
    {
        Init(); 
    }

    void Init()
    {
        m_intersectionList = new List<Intersections>(); 
        Intersections[] T = GameObject.FindObjectsOfType<Intersections>();
        foreach (Intersections inter in T)
        {
            inter.onIntersection += onIntersection;
            m_intersectionList.Add(inter); 
        }


        m_currentZoneList = new List<CurrentZone>();
        CurrentZone[] TZ = GameObject.FindObjectsOfType<CurrentZone>();
        foreach (CurrentZone zone in TZ)
        {
            zone.onCurrentZone += onCurrentZone;
            m_currentZoneList.Add(zone);
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onIntersection(IntersectionExit iExit, bool entered)
    {
        if (onChangeMovementAxis != null)
        {
            onChangeMovementAxis(iExit.getCollider().bounds.center, !entered); 
        }
    }

    void onCurrentZone(Current iCurrent, bool iIn)
    {
        if (onUpdateCurrentList != null)
        {
            onUpdateCurrentList(iCurrent, iIn); 
        }
    }

    public delegate void OnChangeMovementAxis(Vector3 iPosition, bool iLock);
    public event OnChangeMovementAxis onChangeMovementAxis;

    public delegate void OnUpdateCurrentList(Current iCurrent, bool iIn);
    public event OnUpdateCurrentList onUpdateCurrentList; 

    List<Intersections> m_intersectionList;
    List<CurrentZone> m_currentZoneList;

}
