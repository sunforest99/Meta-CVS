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
    [SerializeField] InputActionProperty debugBtn;
    #endregion

    [SerializeField] UI_ProductInfo productUI;

    [SerializeField] Camera mainCamera;

    PhotonView view = null;

    void Start()
    {
        productUI.baseParent = this.transform;
        debugBtn.action.started += OpenDebug;
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
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ConvienceStoreScene")
            {
                leftTriggerInput.uiPressAction.action.started += Started;
                rightTriggerInput.uiPressAction.action.started += Started;
            }

            mainCamera.gameObject.SetActive(true);
            GameMng.I.mainCamera = this.mainCamera;
        }
        else
        {
            movement.enabled = false;
            leftTransform.gameObject.SetActive(false);
            rightTransform.gameObject.SetActive(false);
            this.gameObject.name = "<OtherPlayer>";
        }
    }

    /// <summary>
    /// 오브젝트가 꺼질때 이벤트 삭제해주기
    /// </summary>
    private void OnDisable()
    {
        if (view.IsMine && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ConvienceStoreScene")
        {
            leftTriggerInput.uiPressActionValue.action.started -= Started;
            rightTriggerInput.uiPressActionValue.action.started -= Started;
        }
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

    void OpenDebug(InputAction.CallbackContext context)
    {
        GameMng.I.ShowLog();
        Debug.Log("아아아아아");
    }

    /// <summary>
    /// 상품정보 표시 UI 세팅
    /// </summary>
    /// <param name="controllerTrans">컨트롤러 Transform</param>
    void SetProductUI(Transform controllerTrans)
    {
        if (GameMng.I.Raycast(controllerTrans) != null)
        {
            productUI.gameObject.SetActive(true);
            productUI.transform.parent = controllerTrans;
            productUI.transform.position = controllerTrans.position + (controllerTrans.up + controllerTrans.forward);
            productUI.transform.rotation = controllerTrans.rotation;
            productUI.AddUiInfo(GameMng.I.Raycast(controllerTrans)?.transform.name);
        }
    }
}
