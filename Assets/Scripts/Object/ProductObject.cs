using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Photon.Pun.PhotonView))]
[RequireComponent(typeof(Photon.Pun.PhotonTransformView))]
[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(BoxCollider))]

public class ProductObject : MonoBehaviour
{
    #region 네트워크
    PhotonView view;
    XRGrabInteractable garbInteractable;
    #endregion
    
    [SerializeField] ProductName productName;

    [SerializeField] Vector3 firstPos = Vector3.zero;        // 시작(생성) 위치
    [SerializeField] Quaternion firstRotation = Quaternion.identity;
    [SerializeField] Vector3 firstScale = Vector3.zero;

    Rigidbody rig = null;

    /// <summary>
    /// 들고있는지 확인
    /// </summary>
    public bool isPicked
    {
        get
        {
            return garbInteractable.isSelected;
        }
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.constraints = RigidbodyConstraints.FreezeAll;

        gameObject.name = GameMng.I.productObjectData.name[(int)productName];

        this.gameObject.layer = 3;      // 레이어 설정
        firstPos = transform.position;      // 초기 위치 설정
        firstRotation = transform.rotation;     // 초기 회전값 설정
        firstScale = transform.localScale;

        view = gameObject.GetComponent<PhotonView>();
        garbInteractable = gameObject.GetComponent<XRGrabInteractable>();

        firstPos = this.transform.position;
    }

    private void Update()
    {
        if (isPicked)
        {
            Debug.Log("Picked");
            view.RPC("GrabNetworkMove", RpcTarget.AllViaServer, transform.position, transform.rotation);
            rig.constraints = RigidbodyConstraints.None;
            this.transform.localScale = firstScale;
        }
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
    IEnumerator ResetPosition()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        rig.constraints = RigidbodyConstraints.FreezeAll;
        this.transform.position = firstPos;
        this.transform.rotation = firstRotation;
        this.transform.localScale = firstScale;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Floor"))
        {
            StartCoroutine(ResetPosition());
            // rig.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
