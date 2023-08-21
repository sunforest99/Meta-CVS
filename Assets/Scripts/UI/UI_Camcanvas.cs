using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// TODO 적용하면 지우기
// 룰렛 돌리기/멈추기/그만두기 버튼 눌리게 하기 -> 스크립트 적용해야함

public class UI_Camcanvas : UI_Base
{
    [SerializeField] TMPro.TextMeshProUGUI coinText;
    UnityEngine.UI.Button roll;
    UnityEngine.UI.Button stop;
    UnityEngine.UI.Button quit;
    UnityEngine.UI.RawImage roullette;
    float rotSpeed = 0.0f;
    bool rotState = false;
    public override void Init()
    {
        coinText = Get<TMPro.TextMeshProUGUI>("TotalCoin");
        roll = Get<UnityEngine.UI.Button>("Roll");
        stop = Get<UnityEngine.UI.Button>("Stop");
        quit = Get<UnityEngine.UI.Button>("Quit");
        roullette = Get<UnityEngine.UI.RawImage>("RoulletteImage");

        roll.gameObject.SetActive(false);
        stop.gameObject.SetActive(false);
        roullette.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);

        BindClickEvent(roll.gameObject, RollEvent);
        BindClickEvent(stop.gameObject, StopRollEvent);
        BindClickEvent(quit.gameObject, QuitEvent);
    }

    /// <summary>
    ///    coinCount를 받아와서 화면에 코인 개수 출력
    /// </summary>
    void Update()
    {
        if (GameMng.I.mainCamera != null)
        {
            this.gameObject.transform.position = GameMng.I.mainCamera.transform.position + (GameMng.I.mainCamera.transform.forward * 10.0f);
            this.gameObject.transform.rotation = GameMng.I.mainCamera.transform.rotation;
        }
        coinText.text = "Coin : " + GameMng.I.coinCount;
        if (GameMng.I.coinCount >= 0)
        {
            if (rotSpeed == 300f)
                roullette.gameObject.transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
        }
        if (rotState == false && rotSpeed != 0.0f)
        {
            rotSpeed -= 1f;
            roullette.gameObject.transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    ///    룰렛 돌리는 함수
    /// </summary>
    public void RollEvent(PointerEventData eventData)
    {
        rotSpeed = 300f;
        rotState = true;
    }

    /// <summary>
    ///    룰렛 서서히 멈추는 함수
    /// </summary>
    public void StopRollEvent(PointerEventData eventData)
    {
        rotState = false;
        // 당첨된 것 이미지 띄우기
    }
    /// <summary>
    ///    룰렛 화면에 보이게 하는 함수
    /// </summary>
    public void AppearRoullette()
    {
        roll.gameObject.SetActive(true);
        stop.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
        roullette.gameObject.SetActive(true);
    }

    public void QuitEvent(PointerEventData eventData)
    {
        roll.gameObject.SetActive(false);
        stop.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        roullette.gameObject.SetActive(false);
    }

    public void EnterCVS()
    {
        LoadingScene.Load("ConvienceStoreScene");
    }
}
