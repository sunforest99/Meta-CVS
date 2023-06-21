using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class UI_Intro : UI_Base
{
    UnityEngine.UI.Button enter;
    UnityEngine.UI.Button tutorial;
    UnityEngine.UI.Button setting;
    UnityEngine.UI.Button prev;

    public override void Init()
    {
        enter = Get<UnityEngine.UI.Button>("Enter");
        tutorial = Get<UnityEngine.UI.Button>("Tutorial");
        setting = Get<UnityEngine.UI.Button>("Setting");
        prev = Get<UnityEngine.UI.Button>("Prev");

        BindClickEvent(enter.gameObject, enterEvt);
        BindClickEvent(tutorial.gameObject, tutEvt);
        BindClickEvent(setting.gameObject, setEvt);
        BindClickEvent(prev.gameObject, preEvt);
    }

    public void enterEvt(PointerEventData eventData)
    {
        Debug.Log("enter");
        btnActive();
    }

    public void tutEvt(PointerEventData eventData)
    {
        Debug.Log("tutorial");
        SceneManager.LoadScene("TutorialScene");
    }

    public void setEvt(PointerEventData eventData)
    {
        Debug.Log("setting");
    }

    public void preEvt(PointerEventData eventData)
    {
        Debug.Log("Prev");
        btnActive();
    }

    public void btnActive()
    {
        if (enter.gameObject.activeSelf)
        {
            enter.gameObject.SetActive(false);
            tutorial.gameObject.SetActive(false);
            setting.gameObject.SetActive(false);
            prev.gameObject.SetActive(true);
        }
        else
        {
            enter.gameObject.SetActive(true);
            tutorial.gameObject.SetActive(true);
            setting.gameObject.SetActive(true);
            prev.gameObject.SetActive(false);
        }
    }
}
