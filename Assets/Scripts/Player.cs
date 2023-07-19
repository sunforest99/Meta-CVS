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

    [SerializeField] UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets.DynamicMoveProvider movement;
    #endregion

    [SerializeField] UI_ProductInfo productUI;

    [SerializeField] GameObject mainCamera;

    PhotonView view = null;

    void Start()
    {
        productUI.baseParent = this.transform;
    }

    /// <summary>
    /// 네트워크 동기화 + 컨트롤러 동기화
    /// </summary>
    private void OnEnable()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {
            this.gameObject.name = "<isMinePlayer>";
            leftTriggerInput.uiPressAction.action.started += Started;
            rightTriggerInput.uiPressAction.action.started += Started;

            mainCamera.SetActive(true);
        }
        else
        {
            movement.enabled = false;
            leftTransform.gameObject.SetActive(false);
            rightTransform.gameObject.SetActive(false);
            this.gameObject.name = "<OtherPlayer>";
        }
    }

    private void OnDisable()
    {
        leftTriggerInput.uiPressActionValue.action.started -= Started;
        rightTriggerInput.uiPressActionValue.action.started -= Started;
    }

    private void Update()
    {

    }

    /// <summary>
    /// Trigger 버튼 눌렀을 때 호출될 이벤트 함수
    /// </summary>
    /// <param name="context"></param>
    void Started(InputAction.CallbackContext context)
    {
        if (context.action.activeControl.path.Equals("/OculusTouchControllerRight/triggerpressed"))     // 오른손 컨트롤러
        {
            Debug.Log("right");
            SetProductUI(rightTransform);
        }
        else    // 왼손 컨트롤러
        {
            Debug.Log("left");
            SetProductUI(leftTransform);
        }
    }

    /// <summary>
    /// 상품정보 표시 UI 세팅
    /// </summary>
    /// <param name="constrollTrans">컨트롤러 Transform</param>
    void SetProductUI(Transform constrollTrans)
    {
        if (GameMng.I.Raycast(constrollTrans) != null)
        {
            productUI.gameObject.SetActive(true);
            productUI.transform.parent = constrollTrans;
            productUI.transform.position = constrollTrans.position + new Vector3(0, 2.0f, 2.0f);
            productUI.AddUiInfo(GameMng.I.Raycast(constrollTrans)?.transform.name);
        }
    }
}
