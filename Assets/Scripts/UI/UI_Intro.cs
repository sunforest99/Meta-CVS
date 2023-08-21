using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// TODO : <앞으로 수정할 내용>
// 1. 전화번호 '-' 구분자 입력안됨
// 2. 카드 만료일 '/' 구분자 입력안됨

public class UI_Intro : UI_Base
{
    UnityEngine.UI.Button start;
    UnityEngine.UI.Button enter;
    UnityEngine.UI.Button tutorial;
    UnityEngine.UI.Button setting;
    UnityEngine.UI.Button previous;
    TMPro.TMP_InputField[] inputs = new TMPro.TMP_InputField[10];

    [SerializeField] GameObject keyboard;
    [SerializeField] GameObject logo;

    public override void Init()
    {
        enter = Get<UnityEngine.UI.Button>("Enter");
        tutorial = Get<UnityEngine.UI.Button>("Tutorial");
        setting = Get<UnityEngine.UI.Button>("Setting");
        previous = Get<UnityEngine.UI.Button>("Prev");
        start = Get<UnityEngine.UI.Button>("Start");

        for (int i = 0; i < 10; i++)
        {
            inputs[i] = Get<TMPro.TMP_InputField>($"Input{i + 1}");
            inputs[i].gameObject.SetActive(false);
        }

        keyboard.SetActive(false);
        previous.gameObject.SetActive(false);
        start.gameObject.SetActive(false);

        inputs[2].onDeselect.AddListener(PhoneNumber);
        inputs[7].onDeselect.AddListener(PinNumber);

        BindClickEvent(enter.gameObject, EnterEvt);
        BindClickEvent(tutorial.gameObject, TutorialEvt);
        BindClickEvent(setting.gameObject, SettingEvt);
        BindClickEvent(previous.gameObject, PreviousEvt);
        BindClickEvent(start.gameObject, StartBtn);
    }

    // 휴대폰 번호 사이에 '-' 자동 삽입
    void PhoneNumber(string text)
    {
        try
        {
            inputs[2].text = $"{uint.Parse(text):0##-####-####}";
        }
        catch
        {
            inputs[2].text = text;
        }
    }
    void PinNumber(string text)
    {
        try
        {
            if (text[0] != '0')
                inputs[7].text = $"{uint.Parse(text):##/##}";
            else
                inputs[7].text = $"{uint.Parse(text):0#/##}";
        }
        catch
        {
            inputs[7].text = text;
        }
    }

    void StartBtn(PointerEventData eventData)
    {
        int count = 0;
        string cardnumFull = inputs[3].text + "-" + inputs[4].text + "-" + inputs[5].text + "-" + inputs[6].text;
        for (int i = 0; i < inputs.Length; i++)
        {
            string content = inputs[i].text;
            if (content.Length == 0)
                count += 1;
        }
        if (count > 0) return;
        GameMng.I.dataMng.userData = new UserData(inputs[0].text, inputs[1].text, inputs[2].text, cardnumFull, inputs[7].text, inputs[8].text, inputs[9].text);
        GameMng.I.dataMng.SaveUserData();
        LoadingScene.Load("MainScene");
    }

    void EnterEvt(PointerEventData eventData)
    {
        if (!GameMng.I.userDataSaveFile.CheckNullOrEmpty())
        {
            btnActive();
        }
        else
        {
            LoadingScene.Load("MainScene");
        }

        // catch (System.IO.FileNotFoundException)      // 안먹히고
        // {
        //     Debug.LogWarning($"Load User File Not Found");
        //     btnActive();
        // }
        // catch (System.Exception e)
        // {
        //     Debug.LogError($"Load User Data Failed \n Exception : {e}");
        // }
    }

    void TutorialEvt(PointerEventData eventData)
    {
        Debug.Log("tutorial");
        LoadingScene.Load("TutorialScene");
    }

    void SettingEvt(PointerEventData eventData)
    {
        Debug.Log("setting");
    }

    void PreviousEvt(PointerEventData eventData)
    {
        Debug.Log("Previous");
        btnActive();
    }

    void btnActive()
    {
        if (enter.gameObject.activeSelf)
        {
            enter.gameObject.SetActive(false);
            tutorial.gameObject.SetActive(false);
            setting.gameObject.SetActive(false);
            previous.gameObject.SetActive(true);
            start.gameObject.SetActive(true);
            keyboard.SetActive(true);
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].gameObject.SetActive(true);
            }
            logo.SetActive(false);
        }
        else
        {
            enter.gameObject.SetActive(true);
            tutorial.gameObject.SetActive(true);
            setting.gameObject.SetActive(true);
            previous.gameObject.SetActive(false);
            start.gameObject.SetActive(false);
            keyboard.SetActive(false);
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].gameObject.SetActive(false);
            }
            logo.SetActive(true);
        }
    }
}
