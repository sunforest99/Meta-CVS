using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] ActionBasedController testinput;
    [SerializeField] Transform tras;

    [SerializeField] UI_ProductInfo productUI;
    void Start()
    {
        testinput.uiPressAction.action.started += started;
    }

    private void OnEnable()
    {
        // TODO : 인풋 이벤트 넣기
    }

    void started(InputAction.CallbackContext context)
    {
        if (GameMng.I.Raycast(tras) != null)
        {
            productUI.gameObject.SetActive(true);
            productUI.transform.parent = testinput.transform;
            productUI.transform.position = testinput.transform.position + new Vector3(0,2.0f, 2.0f);
            productUI.AddUiInfo(GameMng.I.Raycast(tras)?.transform.name);
            // Debug.Log("is clicked");
        }
    }
}
