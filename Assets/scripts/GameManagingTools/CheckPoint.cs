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
        if (onCheckPoint != null)
            onCheckPoint(this); 
    }

    public delegate void OnCheckPoint(CheckPoint iCheckPoint);
    public event OnCheckPoint onCheckPoint; 
}
