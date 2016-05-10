using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightManager {

    public enum LightMode
    {
        LIGHTED,
        UNLIT,
        CHANGETOLIGHTED,
        CHANGETOUNLIT, 
        ANIMATIONSTART, 
        ANIMATIONEND
    }

    public LightManager(List<Material> iMaterialList, GameObject iLight, GameObject iMesh)
    {
        if(iMaterialList != null)
        {
            m_materialList = iMaterialList; 
        }
        m_light = iLight;
        m_mesh = iMesh; 

    }

    public void HandleInput(float imaxLight, float icurrentLight)
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (m_currentLightMode == LightMode.LIGHTED)
            {
                onChangeLightMode(LightMode.UNLIT);
            }
            else if (m_currentLightMode == LightMode.UNLIT)
            {
                icurrentLight = imaxLight;
                onChangeLightMode(LightMode.LIGHTED);
            }
        }
        ManageLightingEnergy(imaxLight, icurrentLight);
    }

    void ManageLightingEnergy(float imaxLight, float icurrentLight)
    {
        if (m_currentLightMode == LightMode.LIGHTED)
        {
            icurrentLight -= Time.deltaTime;

            if (icurrentLight / imaxLight < 0.40f)
                m_mesh.GetComponent<Renderer>().material.Lerp(m_materialList[0], m_materialList[1], (icurrentLight / imaxLight) / 0.40f);
            if (icurrentLight < 0 && m_animation == false)
            {
                m_animation = true;
                if (onLightEnergyEmpty != null)
                {
                    onLightEnergyEmpty(LightMode.ANIMATIONSTART);
                }
            }
        }

        if (onCurrentLightEnergyUpdate != null)
        {
            onCurrentLightEnergyUpdate(icurrentLight); 
        }

    }

    void onChangeLightMode(LightMode iLightMode)
    {
        if (iLightMode == LightMode.LIGHTED)
        {
            m_mesh.GetComponent<Renderer>().material = m_materialList[1];
            m_light.SetActive(true);
        }
        else if (iLightMode == LightMode.UNLIT)
        {
            m_mesh.GetComponent<Renderer>().material = m_materialList[0];
            m_light.SetActive(false);

        }
        if (!m_animation)
            m_currentLightMode = iLightMode;
    }

    public IEnumerator SwitchOffAnimation()
    {
        onChangeLightMode(LightMode.UNLIT);
        yield return new WaitForSeconds(0.5f);
        onChangeLightMode(LightMode.LIGHTED);
        yield return new WaitForSeconds(0.4f);
        onChangeLightMode(LightMode.UNLIT);
        yield return new WaitForSeconds(0.3f);
        onChangeLightMode(LightMode.LIGHTED);
        yield return new WaitForSeconds(0.2f);
        onChangeLightMode(LightMode.UNLIT);
        m_animation = false;
        if(onLightEnergyEmpty != null)
        {
            onLightEnergyEmpty(LightMode.ANIMATIONEND); 
        }
        onChangeLightMode(LightMode.UNLIT);
        yield return null;
    }

    public delegate void OnLightEnergyEmpty(LightMode iLightMode);
    public event OnLightEnergyEmpty onLightEnergyEmpty;

    public delegate void OnCurrentLightEnergyUpdate(float icurrentLightEnergy);
    public event OnCurrentLightEnergyUpdate onCurrentLightEnergyUpdate;

    bool m_animation = false;
    private LightMode m_currentLightMode = LightMode.UNLIT;
    private GameObject m_mesh;
    private GameObject m_light;
    private List<Material> m_materialList;

}
