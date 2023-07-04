using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Management;

public class NetworkMng : MonoBehaviourPunCallbacks
{
    private static NetworkMng _Instance;

    public static NetworkMng I
    {
        get
        {
            if (_Instance.Equals(null))
            {
                Debug.Log("Instance is null");
            }
            return _Instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _Instance = this;
    }
    public void ConnectToServer()
    {
        if (Application.internetReachability.Equals(NetworkReachability.NotReachable))
        {
            Debug.LogError("network disconnected");
        }
        else
        {
            // TODO 로그인
            PhotonNetwork.GameVersion = "1.0";      // 게임 버전
            PhotonNetwork.ConnectUsingSettings();   // 서버 연결
        }
    }

    /**
     * @brief Master권한으로 서버 연결 callback 함수
     */
    public override void OnConnectedToMaster()
    {
        //Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom();      // 렌덤 room 들어가는곳
    }

    /**
     * @brief 랜덤 방 들어가기 실패 했을때 callback 함수
     * @param short returnCode 에러 코드
     * @param string message 에러 메시지
     */
    public override void OnJoinRandomFailed(short retrunCode, string message)
    {
        //Debug.Log("no room");
        PhotonNetwork.CreateRoom("myroom");     // 방 생성
    }

    /**
     * @brief 방 생성 되었을때 callback함수
     */
    public override void OnCreatedRoom()
    {
        //Debug.Log("Created room");
    }

    /**
     * @brief 방 들어간거 성공 했을때 callback 함수
     */
    public override void OnJoinedRoom()
    {
        StartCoroutine(this.CreatePlayer());
    }

    /**
     * @brief player 생성
     */
    IEnumerator CreatePlayer()
    {
        // VR
        // if (isVR)
        // {
        //     PhotonNetwork.Instantiate("PlayerVR_1", new Vector3(0, 2, 0), Quaternion.identity, 0);
        // }
        // // Window
        // else
        // {
        //     PhotonNetwork.Instantiate("Player_1", new Vector3(0, 2, 0), Quaternion.identity, 0);
        // }

        yield return null;
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }
}