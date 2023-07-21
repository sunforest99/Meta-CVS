using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Photon.Pun.PhotonView))]
[RequireComponent(typeof(Photon.Pun.PhotonTransformView))]
[RequireComponent(typeof(XRGrabInteractable))]
public class ProductObject : MonoBehaviour
{
    #region 네트워크
    PhotonView view;
    XRGrabInteractable garbInteractable;
    #endregion

    Vector3 firstPos = Vector3.zero;        // 시작(생성) 위치

    /// <summary>
    /// 들고있는지 확인
    /// </summary>
    public bool isPicked
    {
        get{
            return garbInteractable.isSelected;
        }
    }

    private void Start()
    {
        view = gameObject.GetComponent<PhotonView>();
        garbInteractable = gameObject.GetComponent<XRGrabInteractable>();

        firstPos = this.transform.position;
    }

    private void Update()
    {
        if (isPicked)
            view.RPC("GrabNetworkMove", RpcTarget.AllViaServer, transform.position, transform.rotation);
    }

    [PunRPC]
    void GrabNetworkMove(Vector3 pos, Quaternion rotation)
    {
        transform.position = pos;
        transform.rotation = rotation;
    }

    /// <summary>
    ///  떨어졌을때 위치 초기화
    /// </summary>
    void ResetPosition()
    {
        this.transform.position = firstPos;
        this.transform.rotation = Quaternion.identity;
    }
}
