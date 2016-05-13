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


    public IEnumerator FadeIn(float iTime) //asynchron
    {
       
        Image blackScreen = m_UIElements[UIElementEnum.FADE].GetComponent<Image>();
        blackScreen.gameObject.SetActive(true);
        float step = iTime / 255f;

        for (float f = 0f; f <= iTime; f += step)
        {
            Color c = blackScreen.GetComponent<Image>().color;
            c.a += step;
            blackScreen.color = c;
            yield return new WaitForSeconds(step);
        }

        yield return null; 
    }

    

   public IEnumerator FadeOut(float iTime)
    {
        
        float step = iTime / 255f;
        Image blackScreen = m_UIElements[UIElementEnum.FADE].GetComponent<Image>(); 
        for (float f = iTime; f >= 0f; f -= step)
        {
            Color c = blackScreen.color;
            c.a -= step;
            blackScreen.color = c;
            yield return new WaitForSeconds(step);
        }
        blackScreen.gameObject.SetActive(false);
        yield return null; 
    }

   Dictionary<UIElementEnum, GameObject> m_UIElements = new Dictionary<UIElementEnum, GameObject>(); 

}
