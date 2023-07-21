using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Basket_Item : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI productName;
    [SerializeField] TMPro.TextMeshProUGUI count;
    [SerializeField] TMPro.TextMeshProUGUI price;
    [SerializeField] UnityEngine.UI.Button delectBtn;

    // TODO : 렉걸리면 오브젝트 풀로 바꾸기
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

   /// <summary>
    /// 상품 이름, 개수, 개수 * 가격 표시
    /// </summary>
    /// <param name="name"></param>
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
