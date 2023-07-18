using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] GameObject counterUI = null;
    [SerializeField] GameObject itemPrefab = null;

    [SerializeField] Transform productInfoParent = null;

    private void Start()
    {
        counterUI.SetActive(false);
    }

    void SetUICounter()
    {
        // TODO : 나중에 봐서 ObjectPool로 교체하기
        UI_Counter_Item counterUI = null;
        foreach (string key in GameMng.I.basketDict.Keys)
        {
            counterUI = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity).GetComponent<UI_Counter_Item>();
            Debug.Log($"<color=cyan>{counterUI}</color>");
            counterUI.transform.SetParent(productInfoParent, false);
            counterUI.InitProductInfo(key);
        }
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
}
