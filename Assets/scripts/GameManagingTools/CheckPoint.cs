using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && onCheckPoint != null && !m_active)
        {
            onCheckPoint(this);
        }
    }

    void OnDrawGizmos()
    {
        if (m_collider != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(m_collider.bounds.center, m_collider.bounds.size);
            Color c = Color.green;
            c.a = 0.2f;
            Gizmos.color = c;
            Gizmos.DrawCube(m_collider.bounds.center, m_collider.bounds.size);
        }
    }

    public void switchActivity()
    {
        m_active = !m_active;
    }

    public Collider m_collider; 
    public delegate void OnCheckPoint(CheckPoint iCheckPoint);
    public event OnCheckPoint onCheckPoint;
    public int m_id = 0;
    public bool m_startCheckPoint = false;
    bool m_active = false;
}
