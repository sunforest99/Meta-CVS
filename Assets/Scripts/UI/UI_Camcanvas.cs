using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Camcanvas : UI_Base
{
    [SerializeField] TMPro.TextMeshProUGUI coinText;

     public override void Init()
    {
        coinText = Get<TMPro.TextMeshProUGUI>("TotalCoin"); 
    }

    void Update() {
        coinText.text = "Coin : " + GameMng.I.coinCount;
    }
    
}

