using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    private bool isTurning = false; 
    public GameObject m_player;
    private Camera m_cam;
    public float m_maxDistanceFromObject = 0.5f;
    public float m_minDistanceFromObject = 1;
    public float m_damping = 2.0f;
    // Use this for initialization
    void Start () {
        m_cam = gameObject.GetComponent<Camera>(); 
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_cam != null)
        {
            
            if (Mathf.Abs(Vector3.Dot(m_player.transform.position - m_cam.transform.position, new Vector3(1,0,0))) > 0.9f) //(Mathf.Abs(m_player.transform.position.x - m_cam.transform.position.x) > m_maxDistanceFromObject)
            {
                // we are on the right of the screen
                Vector3 v = new Vector3(m_cam.transform.position.x, m_cam.transform.position.y, m_cam.transform.position.z); 
                v.x = m_player.transform.position.x;

                m_cam.transform.position = Vector3.Lerp(m_cam.transform.position, v, Time.deltaTime * m_damping);
                //m_cam.transform.position = v; 

            }
            //m_cam.transform.LookAt(m_player.transform.position);
            if (Mathf.Abs(Vector3.Dot(m_player.transform.position - m_cam.transform.position, new Vector3(0, 1, 0))) > 0.55f)
            {
                // we are on the right of the screen
                Vector3 v = new Vector3(m_cam.transform.position.x, m_cam.transform.position.y, m_cam.transform.position.z);
                v.y = m_player.transform.position.y + 5;

                m_cam.transform.position = Vector3.Lerp(m_cam.transform.position, v, Time.deltaTime * m_damping);
                //m_cam.transform.position = v; 
                Quaternion t = m_cam.transform.rotation; 
                m_cam.transform.LookAt(m_player.transform);
                Quaternion t2 = m_cam.transform.rotation;
                t2.y = t.y;
                t2.z = t.z; 
                m_cam.transform.rotation = t2; 

            }

        }
	
	}
}
