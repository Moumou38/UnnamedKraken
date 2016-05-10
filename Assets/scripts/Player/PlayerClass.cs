using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerClass : MonoBehaviour {

    public float MaxEnergy
    {
        get { return m_maxEnergy; }
        set { m_maxEnergy = value; }
    }
    public float CurrentEnergy
    {
        get { return m_maxEnergy; }
        set { m_maxEnergy = value; }
    }
    
    public float MaxLight
    {
        get { return m_maxLightBar; }
        set { m_maxLightBar = value; }
    }
    public float CurrentLight
    {
        get { return m_currentLightBar; }
        set { m_currentLightBar = value; }
    }
    
    public float MaxHealth
    {
        get { return m_maxHealth; }
        set { m_maxHealth = value; }
    }
    public float CurrentHealth
    {
        get { return m_currentHealth; }
        set { m_currentHealth = value; }
    }
    public int GoldCoins
    {
        get { return m_goldCoins; }
        set { m_goldCoins = value; }
    }



    // Use this for initialization
    void Awake () {
        m_playerPhysics = GetComponent<PlayerPhysics>();
        m_playerPathManager = new PlayerPathManager();
        if (m_playerPathManager != null)
        {
            m_playerPathManager.onChangeMovementAxis += ChangeMovementAxis;
            m_playerPathManager.onUpdateCurrentList += UpdateCurrentList;
        }
        if (m_playerPhysics != null)
        {
            m_playerPhysics.isJumping += handleSwimmingEnergy;
            m_playerPhysics.setJumpEnergy(m_maxEnergy); 
        }
        if (m_materialList != null && m_materialList.Count > 0)
        {
            m_lightManager = new LightManager(m_materialList, m_light, m_mesh);
            m_lightManager.onLightEnergyEmpty += onLightEnergyEmpty;
            m_lightManager.onCurrentLightEnergyUpdate += setCurrentLight; 
            if (m_mesh != null)
            {
                m_mesh.GetComponent<Renderer>().material = m_materialList[0];
                m_light.SetActive(false);

                
            }
        }

        m_currentEnergy = m_maxEnergy; 

    }

    void UpdateCurrentList(Current iCurrent, bool iIn)
    {
        m_playerPhysics.UpdateCurrentList(iCurrent, iIn);
    }


    void setCurrentLight(float iCurrentLightEnergy)
    {
        m_currentLightBar = iCurrentLightEnergy; 
    }

    void onLightEnergyEmpty(LightManager.LightMode iLightMode)
    {
        if (iLightMode == LightManager.LightMode.ANIMATIONSTART)
        {
            StartCoroutine(m_lightManager.SwitchOffAnimation()); 
        }
        else if(iLightMode == LightManager.LightMode.ANIMATIONEND)
        m_currentLightBar = m_maxLightBar; 
    }

    void ChangeMovementAxis(Vector3 iPosition, bool iLock)
    {
        m_playerPhysics.changeDepth(iPosition, iLock); 
    }

    void handleSwimmingEnergy(bool iJumping)
    {
        if (m_currentEnergy > 0f && iJumping)
        {
            m_currentEnergy -= Time.deltaTime; 
        }
        else if (m_currentEnergy < m_maxEnergy && !iJumping)
        {
            m_currentEnergy += (Time.deltaTime/4);
        }

        m_playerPhysics.setJumpEnergy(m_currentEnergy); 
    }

    void OnGUI()
    {
        GUILayout.Label("JUMP ENERGY : " + m_currentEnergy + "/" + m_maxEnergy);
        GUILayout.Label("LIGHT ENERGY : " + m_currentLightBar + "/" + m_maxLightBar);

    }

    // Update is called once per frame
    void Update () {

        m_lightManager.HandleInput(m_maxLightBar, m_currentLightBar);
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject go = Instantiate(m_inkBall) as GameObject;
            go.transform.position = m_inkOrigin.transform.position; 
        }

    }

    


    ///// PRIVATE ////

    private float m_maxLightBar = 15f; // in seconds
    private float m_currentLightBar = 15f;
    private float m_maxHealth = 10f;
    private float m_currentHealth = 10f;
    private float m_maxEnergy = 0.7f; // in seconds
    private float m_currentEnergy = 0.7f;
    private int m_goldCoins = 0;

    public GameObject m_inkBall; 

    [SerializeField]
    GameObject m_inkOrigin; 
    private PlayerPhysics m_playerPhysics;
    private PlayerPathManager m_playerPathManager;
    private LightManager m_lightManager; 

    public GameObject m_mesh;
    public GameObject m_light;
    public List<Material> m_materialList; 

}
