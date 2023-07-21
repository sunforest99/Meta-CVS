using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] GameObject counterUI = null;

    [SerializeField] Transform scrollParent = null;
    [SerializeField] GameObject itemPrefab = null;

    [SerializeField] TMPro.TextMeshProUGUI resultText = null;
    [SerializeField] GameObject receipt;

    private void Start()
    {
        counterUI.SetActive(false);
        receipt.SetActive(false);
    }

    private void OnEnable()
    {
        CalcResultPrice();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Equals("<isMinePlayer>"))
        {
            counterUI.SetActive(true);
            SetUICounter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name.Equals("<isMinePlayer>"))
        {
            counterUI.SetActive(false);
        }
    }

    void CalcResultPrice()
    {
        GameMng.I.totalPrice = 0;

        foreach (string name in GameMng.I.basketDict.Keys)
        {
            GameMng.I.totalPrice += (GameMng.I.dataMng.TryGetObjectValue(name).PRICE * GameMng.I.basketDict[name]);
        }

        resultText.text = GameMng.I.totalPrice.ToString();
    }

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

    public void ReceiptOpen() {
        receipt.SetActive(true);
        counterUI.SetActive(false);
        StartCoroutine("CloseReceipt");
    }
    IEnumerator CloseReceipt() {
        yield return new WaitForSeconds(2f);
        receipt.SetActive(false);
    }
}
