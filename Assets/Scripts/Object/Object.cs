using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;

#region 오브젝트_데이터
[System.Serializable]
public class ObjectData
{
    /// <summary>
    /// 품목명
    /// </summary>
    public string DESC_KOR;
    /// <summary>
    /// 총내용량
    /// </summary>
    public string SERVING_SIZE;
    /// <summary>
    /// 식품군
    /// </summary>
    public string GROUP_NAME;
    /// <summary>
    /// 제조사명
    /// </summary>
    public string MAKER_NAME;

    /// <summary>
    /// 열량(kcal)(1회제공량당)
    /// </summary>
    public string NUTR_CONT1;
    /// <summary>
    /// 탄수화물(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT2;
    /// <summary>
    /// 단백질(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT3;
    /// <summary>
    /// 지방(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT4;
    /// <summary>
    /// 당류(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT5;
    /// <summary>
    /// 나트륨(mg)(1회제공량당)
    /// </summary>
    public string NUTR_CONT6;
    /// <summary>
    /// 콜레스테롤(mg)(1회제공량당)
    /// </summary>
    public string NUTR_CONT7;
    /// <summary>
    /// 포화지방산(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT8;
    /// <summary>
    /// 트랜스지방(g)(1회제공량당)
    /// </summary>
    public string NUTR_CONT9;
}
#endregion

public class ObjectDatas
{
    public ObjectData[] row;
}

[RequireComponent(typeof(Photon.Pun.PhotonView))]
[RequireComponent(typeof(Photon.Pun.PhotonTransformView))]
[RequireComponent(typeof(XRGrabInteractable))]
public class Object : MonoBehaviour
{
    #region 네트워크
    PhotonView view;
    XRGrabInteractable garbInteractable;
    #endregion

    Vector3 firstPos = Vector3.zero;

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
