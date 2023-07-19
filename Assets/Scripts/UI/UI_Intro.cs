using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Intro : UI_Base
{
    UnityEngine.UI.Button start;
    UnityEngine.UI.Button enter;
    UnityEngine.UI.Button tutorial;
    UnityEngine.UI.Button setting;
    UnityEngine.UI.Button previous;
    TMPro.TMP_InputField[] inputs = new TMPro.TMP_InputField[10];

    public override void Init()
    {
        enter = Get<UnityEngine.UI.Button>("Enter");
        tutorial = Get<UnityEngine.UI.Button>("Tutorial");
        setting = Get<UnityEngine.UI.Button>("Setting");
        previous = Get<UnityEngine.UI.Button>("Prev");
        start = Get<UnityEngine.UI.Button>("Start");

        for (int i = 1; i <= 10; i++)
        {
            inputs[i - 1] = Get<TMPro.TMP_InputField>($"Input{i}");
            inputs[i - 1].gameObject.SetActive(false);
        }

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
    public void PhoneNumber(string text)
    {
        inputs[2].text = $"{uint.Parse(text):0##-####-####}";
    }
    public void PinNumber(string text)
    {
        if (text.Substring(0, 1) != "0")
            inputs[7].text = $"{uint.Parse(text):##/##}";
        else
            inputs[7].text = $"{uint.Parse(text):0#/##}";
    }

    // 태양이가 추가할 내용 : 데이터가 있으면 정보 입력 과정 스킵. *****
    public void StartBtn(PointerEventData eventData)
    {   
        string cardnumFull = inputs[3].text + "-" + inputs[4].text + "-" + inputs[5].text + "-" + inputs[6].text;
        GameMng.I.dataMng.userData = new UserData(inputs[0].text, inputs[1].text, inputs[2].text, cardnumFull, inputs[7].text, inputs[8].text, inputs[9].text);
        GameMng.I.dataMng.SaveUserData();
        LoadingScene.Load("MainScene");
    }

    public void EnterEvt(PointerEventData eventData)
    {
        try
        {
            GameMng.I.dataMng.LoadUserDataFromJson();
            LoadingScene.Load("MainScene");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Load User Data Failed \n Exception : {e}");
            btnActive();
        }    
    }

    public void TutorialEvt(PointerEventData eventData)
    {
        Debug.Log("tutorial");
        LoadingScene.Load("TutorialScene");
    }

    public void SettingEvt(PointerEventData eventData)
    {
        Debug.Log("setting");
    }

    public void PreviousEvt(PointerEventData eventData)
    {
        Debug.Log("Previous");
        btnActive();
    }

    public void btnActive()
    {
        if (enter.gameObject.activeSelf)
        {
            enter.gameObject.SetActive(false);
            tutorial.gameObject.SetActive(false);
            setting.gameObject.SetActive(false);
            previous.gameObject.SetActive(true);
            start.gameObject.SetActive(true);
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].gameObject.SetActive(true);
            }
        }
        else
        {
            enter.gameObject.SetActive(true);
            tutorial.gameObject.SetActive(true);
            setting.gameObject.SetActive(true);
            previous.gameObject.SetActive(false);
            start.gameObject.SetActive(false);
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].gameObject.SetActive(false);
            }
        }
    }
}
