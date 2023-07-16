using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.EventSystems;

public class UI_ProductInfo : UI_Base
{
    StringBuilder productString = new StringBuilder();

    ProductData data;

    TMPro.TMP_Text dataText;
    TMPro.TMP_Text countText;

    Button cancele;
    Button shoopingBasket;
    Button addCount;
    Button minCount;

    int count = 1;
    
    // 딕셔너리에 있는 카운트 값 꺼내기
    int tempCount = 0;

    public Transform baseParent;       // 원래 있던 부모
    
    public override void Init()
    {
        dataText = Get<TMPro.TMP_Text>("Data");
        countText = Get<TMPro.TMP_Text>("Count");
        cancele = Get<Button>("Exit");
        shoopingBasket = Get<Button>("Basket");
        addCount = Get<Button>("AddCount");
        minCount = Get<Button>("MinCount");

        BindClickEvent(shoopingBasket.gameObject, AddBasket);
        BindClickEvent(cancele.gameObject, ExitUI);
        BindClickEvent(addCount.gameObject, AddCount);
        BindClickEvent(minCount.gameObject, MinCount);

        gameObject.SetActive(false);
    }

    /// <summary>
    ///  수량 증가
    /// </summary>
    /// <param name="pointerEvent"></param>
    public void AddCount(PointerEventData pointerEvent)
    {
        count++;
        countText.text = count.ToString();
    }

    /// <summary>
    ///  수량 감소
    /// </summary>
    /// <param name="pointerEvent"></param>
    public void MinCount(PointerEventData pointerEvent)
    {
        count = count > 1 ? count - 1 : count;
        countText.text = count.ToString();
    }

    /// <summary>
    /// 장바구니 등록
    /// </summary>
    /// <param name="pointerEvent"></param>
    public void AddBasket(PointerEventData pointerEvent)
    {
        try
        {
            GameMng.I.basketDict.Add(data.DESC_KOR, count);
        }
        catch
        {
            if (GameMng.I.basketDict.TryGetValue(data.DESC_KOR, out tempCount))
            {
                GameMng.I.basketDict.Remove(data.DESC_KOR);
                GameMng.I.basketDict.Add(data.DESC_KOR, count + tempCount);
            }
        }

        gameObject.SetActive(false);
        transform.parent = baseParent;
        Clear();
    }

    /// <summary>
    /// UI 종료 후 원래 위치로 보내기
    /// </summary>
    /// <param name="pointerEvent"></param>
    public void ExitUI(PointerEventData pointerEvent)
    {
        gameObject.SetActive(false);
        transform.parent = baseParent;
        Clear();
    }

    /// <summary>
    /// UI에 상품 정보 넣기
    /// </summary>
    public void AddUiInfo(string name)
    {
        Clear();

        data = GameMng.I.dataMng.TryGetObjectValue(name);

        productString.Append($"1회제공량 :\t{data.SERVING_SIZE} \n");
        productString.Append($"열량 :\t{data.NUTR_CONT1} (kcal)\n");
        productString.Append($"탄수화물 :\t{data.NUTR_CONT2} (g)\n");
        productString.Append($"단백질 :\t{data.NUTR_CONT3} (g)\n");
        productString.Append($"지방 :\t{data.NUTR_CONT4} (g)\n");
        productString.Append($"당류 :\t{data.NUTR_CONT5} (g)\n");
        productString.Append($"나트륨 :\t{data.NUTR_CONT6} (mg)\n");
        productString.Append($"콜레스테롤 :\t{data.NUTR_CONT7} (mg)\n");
        productString.Append($"포화지방산 :\t{data.NUTR_CONT8} (g)\n");
        productString.Append($"트렌스지방 :\t{data.NUTR_CONT9} (g)\n");

        dataText.text = productString.ToString();
    }

    /// <summary>
    /// UI 초기값으로 초기화
    /// </summary>
    void Clear()
    {
        count = 1;
        countText.text = count.ToString();
        data = null;
        productString.Clear();
        tempCount = 0;
    }    
}
