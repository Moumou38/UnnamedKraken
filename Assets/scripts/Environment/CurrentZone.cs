using UnityEngine;
using System.Collections;

public class Current
{

    public Current(Vector3 iOrigin, Vector3 iDirection, float istrength, bool ireverse)
    {
        origin = iOrigin;
        direction = iDirection;
        reverse = ireverse;
        strength = istrength; 
    }

    public Vector3 origin = Vector3.zero;
    public Vector3 direction = Vector3.zero;
    public bool reverse = false; // true = swallow / false = blow 
    public float strength = 0f; 
};

public class CurrentZone : MonoBehaviour {

    public GameObject m_direction;
    public GameObject m_origin;
    public Collider m_collider;

    

	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        m_current = new Current(m_origin.transform.position, m_direction.transform.position, strength, reverse); 
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && onCurrentZone != null)
        {
            onCurrentZone(m_current, true); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8 && onCurrentZone != null)
        {
            onCurrentZone(m_current, false);
        }
    }

    void OnDrawGizmos()
    {
        if (m_collider != null)
        {
            Gizmos.color = Color.black; 
            Gizmos.DrawWireCube(m_collider.bounds.center, m_collider.bounds.size);
            Color c = Color.yellow;
            c.a = 0.2f;
            Gizmos.color = c;
            Gizmos.DrawCube(m_collider.bounds.center, m_collider.bounds.size);
        }

        if (m_direction != null && m_origin != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(m_origin.transform.position, m_direction.transform.position);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(m_direction.transform.position, 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(m_origin.transform.position, 0.2f); 
        }

    }

    public delegate void OnCurrentZone(Current iCurrent, bool iIn);
    public event OnCurrentZone onCurrentZone;
    public Current m_current;
    public bool reverse = false;
    public float strength = 0f;
}
