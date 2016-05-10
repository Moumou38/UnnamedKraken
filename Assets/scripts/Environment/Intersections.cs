using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Intersections : MonoBehaviour {
    [SerializeField]
    private List<IntersectionExit> m_exitList; 

    void Awake()
    {
        if (m_exitList != null)
        {
            foreach (IntersectionExit exit in m_exitList)
            {
                exit.triggerEnter += OnEnterExit; 
            }
        }

        if (m_collider == null)
        {
            m_collider = GetComponent<Collider>(); 
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        if (m_collider != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(m_collider.bounds.center, m_collider.bounds.size);
            Color c = Color.blue;
            c.a = 0.4f;
            Gizmos.color = c;
            Gizmos.DrawCube(m_collider.bounds.center, m_collider.bounds.size);
        }
    }

    void OnEnterExit(IntersectionExit iIntersection, Collider player)
    {
        if (m_collider.bounds.Contains(player.bounds.center) && onIntersection != null)
        {
            onIntersection(iIntersection, true); 
        }
        else if (onIntersection != null)
        {
            onIntersection(iIntersection, false);
        }
    }

    public void setIntersectionExits(List<IntersectionExit> iExitList)
    {
        if (iExitList != null)
        {
            m_exitList = iExitList; 
        }
    }

    public Collider getCOllider()
    {
        return m_collider; 
    }

    public void setCollider(Collider iCollider)
    {
        m_collider = iCollider; 
    }

    [SerializeField]
    Collider m_collider; 

    public delegate void OnIntersection(IntersectionExit iExit, bool entered);
    public event OnIntersection onIntersection; 
    
}
