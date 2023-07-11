using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] ActionBasedController testinput;
    [SerializeField] Transform tras;
    void Start()
    {
        testinput.uiPressAction.action.started += started;
    }

    void started(InputAction.CallbackContext context)
    {
        if(GameMng.I.Raycast(tras) != null)
        {
            AddUiInfo(GameMng.I.Raycast(tras)?.transform.name);
            Debug.Log("is clicked");
        }
    }

    
    void canceled(InputAction.CallbackContext context)
    {
        Debug.Log("is canceled");
    }
    
    /// <summary>
    /// UI에 상품 정보 넣기
    /// </summary>
    /// <param name="name"></param>
    void AddUiInfo(string name)
    {
        ProductData data = GameMng.I.dataMng.TryGetObjectValue(name);
        Debug.Log(name);
        Debug.Log(data.DESC_KOR);
        Debug.Log(data.NUTR_CONT1);
        Debug.Log(data.NUTR_CONT2);
        Debug.Log(data.NUTR_CONT3);
        Debug.Log(data.NUTR_CONT4);
    }
}
