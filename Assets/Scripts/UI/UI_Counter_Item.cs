using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Counter_Item : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI productName;
    [SerializeField] TMPro.TextMeshProUGUI count;
    [SerializeField] TMPro.TextMeshProUGUI price;

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    public void InitProductInfo(string name)
    {
        productName.text = name;
        count.text = GameMng.I.basketDict[name].ToString();
        price.text = (GameMng.I.dataMng.TryGetObjectValue(name).PRICE * GameMng.I.basketDict[name]).ToString();
    }

    public void DelectBtnAction()
    {
        GameMng.I.basketDict.Remove(productName.text);
        Destroy(this.gameObject);
    }
}
