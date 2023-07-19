using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Basket : UI_Base
{    
    [SerializeField] GameObject itemPrefab = null;

    public override void Init()
    {
        gameObject.SetActive(false);
    }

    // TODO : Player에서 호출해주기
    public void SetUIBasket()
    {
        // TODO : 나중에 봐서 ObjectPool로 교체하기
        UI_Basket_Item basketUI = null;
        foreach (string key in GameMng.I.basketDict.Keys)
        {
            basketUI = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity).GetComponent<UI_Basket_Item>();
            basketUI.transform.SetParent(this.transform, false);
            basketUI.InitProductInfo(key);
        }
    }
}
