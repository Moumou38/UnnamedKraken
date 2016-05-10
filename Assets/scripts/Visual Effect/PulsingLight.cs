using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class PulsingLight : MonoBehaviour {

    public float m_min = 3f;
    public float m_speed = 5f;
    public float m_max = 6f;  
    Light m_light; 
	// Use this for initialization
	void Awake () {
        m_light = GetComponent<Light>(); 
	}
	
	// Update is called once per frame
	void Update () {
        m_light.range = m_min + Mathf.PingPong(Time.time * m_speed, m_max);
    }
}
