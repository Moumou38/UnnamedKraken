using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System; 

public class UIManager : MonoBehaviour {

    public enum UIElementEnum
    {
        HEALTHBAR, 
        ENERGYBAR, 
        LIGHTBAR,
        TUTORIAL_WELCOME,
        FADE,
        MAIN_MENU,
        LOADING, 
        DEBUG,
        NONE
    }



    [Serializable]
    public struct UIObjects
    {
        public UIElementEnum m_type;
        public GameObject m_element;
    }
    public UIObjects[] UITab;

    void Awake()
    {
        
    }

    public void init()
    {
        foreach (UIObjects o in UITab)
        {
            m_UIElements.Add(o.m_type, o.m_element);
        }
    }

    public void HideUIElement(UIElementEnum iElementType)
    {
        m_UIElements[iElementType].gameObject.SetActive(false); 
    }

    public void ShowUIElement(UIElementEnum iElementType)
    {
        m_UIElements[iElementType].gameObject.SetActive(true);
    }

    public void disableButton(UIElementEnum iElementType, string name)
    {
        GameObject go = m_UIElements[iElementType];
        if(go != null)
        {
            foreach(Transform t in go.GetComponentsInChildren<Transform>())
            {
                if(t.name == name)
                {
                    Button b = t.gameObject.GetComponent<Button>();
                    if(b != null)
                    {
                        b.interactable = false; 
                    }

                    break; 
                }
            }
        }
    }

    public void enableButton(UIElementEnum iElementType, string name)
    {
        GameObject go = m_UIElements[iElementType];
        if (go != null)
        {
            foreach (Transform t in go.GetComponentsInChildren<Transform>())
            {
                if (t.name == name)
                {
                    Button b = t.gameObject.GetComponent<Button>();
                    if (b != null)
                    {
                        b.interactable = true;
                    }
                    break; 
                }
            }
        }
    }


    public IEnumerator FadeIn(float iTime) //asynchron
    {

        //Image blackScreen = m_UIElements[UIElementEnum.FADE].GetComponent<Image>();
        //blackScreen.gameObject.SetActive(true);
        //float step = iTime / 255f;

        //Color c = blackScreen.GetComponent<Image>().color;
        //c.a = 0;
        //blackScreen.color = c;


        //for (float f = 0f; f <= iTime; f += step)
        //{
        //    c.a += step;
        //    blackScreen.color = c;
        //    yield return new WaitForSeconds(step);
        //}

        //if (onFadeDone != null)
        //    onFadeDone();
        //yield return null; 


        Image blackScreen = m_UIElements[UIElementEnum.FADE].GetComponent<Image>();
        blackScreen.gameObject.SetActive(true);
        float step = iTime / 255f;

        Color c = blackScreen.GetComponent<Image>().color;
        c.a = 0;
        blackScreen.color = c;


        for (float f = 0f; f <= 1f; f += step)
        {
            c.a += step;
            blackScreen.color = c;
            //Debug.Log("Step : " + step + " :::: c.a :" + c.a);
            yield return new WaitForSeconds(step);
        }

        if (onFadeDone != null)
            onFadeDone();
        yield return null;

    }



    public IEnumerator FadeOut(float iTime)
    {

        Image blackScreen = m_UIElements[UIElementEnum.FADE].GetComponent<Image>();
        
        float step = iTime / 255f;

        Color c = blackScreen.GetComponent<Image>().color;
        c.a = 1f;
        blackScreen.color = c;


        for (float f = 0f; f <= 1f; f += step)
        {
            c.a -= step;
            blackScreen.color = c;
            //Debug.Log("Step : " + step + " :::: c.a :" + c.a);
            yield return new WaitForSeconds(step);
        }

        blackScreen.gameObject.SetActive(false);
        if (onFadeDone != null)
            onFadeDone();
        yield return null;

    }

    public void SwitchUI(UIElementEnum iEnum)
    {
        HideAll();
        ShowUIElement(iEnum);

    }

    public void HideAll()
    {
        foreach (UIElementEnum s in m_UIElements.Keys)
        {
            m_UIElements[s].SetActive(false);
        }
    }

   Dictionary<UIElementEnum, GameObject> m_UIElements = new Dictionary<UIElementEnum, GameObject>();
    public delegate void OnFadeDone();
    public event OnFadeDone onFadeDone; 
}
