using UnityEngine;
using System.Collections;

public class IntersectionExit : MonoBehaviour {


	// Use this for initialization
	void Awake () {

        if (m_collider == null)
        {
            m_collider = GetComponent<Collider>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider other)
    {
        if (triggerEnter != null && other.gameObject.layer == 8)
        {
            triggerEnter(this, other); 
        }
    }

    public Collider getCollider()
    {
        return m_collider;
    }

    public void setCollider(Collider iCollider)
    {
        m_collider = iCollider;
    }

    void OnDrawGizmos()
    {
        if (m_collider != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(m_collider.bounds.center, m_collider.bounds.size);
            Color c = Color.red;
            c.a = 0.4f;
            Gizmos.color = c;
            Gizmos.DrawCube(m_collider.bounds.center, m_collider.bounds.size);
        }
    }

    [SerializeField]
    Collider m_collider; 
    public delegate void TriggerEnter(IntersectionExit iExit, Collider player);
    public event TriggerEnter triggerEnter; 
}
