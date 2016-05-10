using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 

public class UIManager {

    public enum UIElementEnum
    {
        HEALTHBAR, 
        ENERGYBAR, 
        LIGHTBAR,
        TUTORIAL_WELCOME,
        NONE
    }
    Canvas m_canvas;
    Dictionary<UIElementEnum, UIElement> m_UIDict; 

    public UIManager(Canvas iCanvas)
    {

    }

    public void HideUI()
    {
        if(m_canvas.gameObject.activeInHierarchy == true)
            m_canvas.gameObject.SetActive(false); 
    }

	
}
