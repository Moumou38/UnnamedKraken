using UnityEngine;
using System.Collections;

public class InkBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        cpt += Time.deltaTime;
        //if (gameObject.transform.localScale.x < 5f)
        //{
        //    gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        //}
        if (cpt > m_inkBallLifeSpan)
            GameObject.Destroy(gameObject); 
	}

    void OnTriggerEnter(Collider other)
    {

    }

    float cpt = 0f; 
    [SerializeField]
    float m_inkBallLifeSpan = 10f; //in Seconds 

    float m_maxSize = 5f; 
}
