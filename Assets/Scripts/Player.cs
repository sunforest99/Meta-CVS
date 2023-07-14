using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Player : MonoBehaviour
{
#region 컨트롤러
    [SerializeField] ActionBasedController leftTriggerInput;
    [SerializeField] ActionBasedController rightTriggerInput;

    [SerializeField] Transform leftTransform;
    [SerializeField] Transform rightTransform;
#endregion

    [SerializeField] UI_ProductInfo productUI;

    [SerializeField] GameObject mainCamera;

    PhotonView view = null;

    void Start()
    {
        
    }


    private void OnEnable()
    {
        // TODO : 인풋 이벤트 넣기
        leftTriggerInput.uiPressActionValue.action.started += started;
        rightTriggerInput.uiPressActionValue.action.started += started;
        

        view = GetComponent<PhotonView>();

        if(view.IsMine)
        {
            this.gameObject.name = "<isMinePlayer>";
            mainCamera.SetActive(true);
        }
        else
        {
            leftTransform.gameObject.SetActive(false);
            rightTransform.gameObject.SetActive(false);
            this.gameObject.name = "<OtherPlayer>";
        }
    }

    private void Update()
    {
        // if (view.IsMine)
        // {

        // }
    }

    void started(InputAction.CallbackContext context)
    {
        if(context.action.activeControl.path.Equals("/OculusTouchControllerRight/trigger"))
            Debug.Log("right Trigger");

        else
            Debug.Log("left Trigger");
        
        // Debug.Log(rightTriggerInput.);
        // if (GameMng.I.Raycast(tras) != null)
        // {
        //     productUI.gameObject.SetActive(true);
        //     productUI.transform.parent = testinput.transform;
        //     productUI.transform.position = testinput.transform.position + new Vector3(0, 2.0f, 2.0f);
        //     productUI.AddUiInfo(GameMng.I.Raycast(tras)?.transform.name);
        //     // Debug.Log("is clicked");
        // }
    }
}
