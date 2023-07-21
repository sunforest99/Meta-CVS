using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Counter : UI_Base
{
    [SerializeField] Transform scrollParent = null;
    [SerializeField] GameObject itemPrefab = null;
    [SerializeField] GameObject receipt;
    
    TMPro.TextMeshProUGUI resultText = null;
    UnityEngine.UI.Button exitButton = null;
    UnityEngine.UI.Button payButton = null;

    public override void Init()
    {
        resultText = Get<TMPro.TextMeshProUGUI>("ResultPrice");
        exitButton = Get<UnityEngine.UI.Button>("ExitButton");
        payButton = Get<UnityEngine.UI.Button>("PayButton");

        BindClickEvent(exitButton.gameObject, ExitUI);
        BindClickEvent(payButton.gameObject, ReceiptOpen);

        gameObject.SetActive(false);
        receipt.SetActive(false);
    }

    private void OnEnable()
    {
        CalcResultPrice();
    }

    /// <summary>
    /// 가격 총 합 계산 후 보여주기
    /// </summary>
    void CalcResultPrice()
    {
        GameMng.I.totalPrice = 0;

        foreach (string name in GameMng.I.basketDict.Keys)
        {
            GameMng.I.totalPrice += (GameMng.I.dataMng.TryGetObjectValue(name).PRICE * GameMng.I.basketDict[name]);
        }

        resultText.text = GameMng.I.totalPrice.ToString();
    }

    /// <summary>
    /// 장바구니에 있는 물품들 UI에 띄워주기
    /// </summary>
    public void SetUICounter()
    {
        // TODO : 나중에 봐서 ObjectPool로 교체하기
        UI_Counter_Item basketUI = null;
        foreach (string key in GameMng.I.basketDict.Keys)
        {
            basketUI = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity).GetComponent<UI_Counter_Item>();
            basketUI.transform.SetParent(scrollParent, false);
            basketUI.InitProductInfo(key);
        }
    }

    /// <summary>
    /// UI 종료 후 원래 위치로 보내기
    /// </summary>
    /// <param name="pointerEvent"></param>
    public void ExitUI(PointerEventData pointerEvent)
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 영수증 표시
    /// </summary>
    /// <param name="eventData"></param>
    void ReceiptOpen(PointerEventData eventData)
    {
        receipt.SetActive(true);
        gameObject.SetActive(false);
        StartCoroutine("CloseReceipt");
    }

    /// <summary>
    /// 2초 뒤 영수증 닫기
    /// </summary>
    IEnumerator CloseReceipt()
    {
        yield return new WaitForSeconds(2f);
        receipt.SetActive(false);
    }
}
