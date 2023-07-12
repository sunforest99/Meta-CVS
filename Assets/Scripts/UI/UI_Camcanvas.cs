using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Camcanvas : UI_Base
{
    [SerializeField] TMPro.TextMeshProUGUI coinText;
    UnityEngine.UI.Button roll;
    UnityEngine.UI.Button stop;
    UnityEngine.UI.RawImage roullette;
    float rotSpeed = 0.0f;

     public override void Init()
    {
        coinText = Get<TMPro.TextMeshProUGUI>("TotalCoin"); 
        roll = Get<UnityEngine.UI.Button>("Roll");
        stop = Get<UnityEngine.UI.Button>("Stop");
        roullette = Get<UnityEngine.UI.RawImage>("RoulletteImage");

        roll.gameObject.SetActive(false);
        stop.gameObject.SetActive(false);
        roullette.gameObject.SetActive(false);
        BindClickEvent(roll.gameObject, RollEvt);
        BindClickEvent(stop.gameObject, StopRollEvt);
    }
    /// <summary>
    ///    coinCount를 받아와서 화면에 코인 개수 출력
    /// </summary>
    void Update() {
        coinText.text = "Coin : " + GameMng.I.coinCount;
    }
    /// <summary>
    ///    룰렛 돌리는 함수
    /// </summary>
    public void RollEvt(PointerEventData pointerEvent) {
        if(GameMng.I.coinCount >= 20) {
            GameMng.I.coinCount -= 20;
            rotSpeed = 200.0f;
            transform.Rotate(rotSpeed*Time.deltaTime,0,0);
        }
    }
    /// <summary>
    ///    룰렛 서서히 멈추는 함수
    /// </summary>
    public void StopRollEvt(PointerEventData pointerEvent) {
        while(rotSpeed != 0.0f) {
            rotSpeed -= 10.0f;
        }
        transform.Rotate(rotSpeed*Time.deltaTime, 0, 0);
    }
    /// <summary>
    ///    룰렛 화면에 보이게 하는 함수
    /// </summary>
    public void AppearRoullette() {
        roll.gameObject.SetActive(true);
        stop.gameObject.SetActive(true);
        roullette.gameObject.SetActive(true);
    }
}
