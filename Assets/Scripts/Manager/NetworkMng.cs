using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkMng : MonoBehaviourPunCallbacks
{

    // TODO : 씬 변경시 전씬에 있던 오브젝트 파괴 안됨
    
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


    [SerializeField] GameObject playerPrefab;

    private void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(this);
        ConnectToServer();
    }

    void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
        if (pool != null && this.playerPrefab != null)
        {
            pool.ResourceCache.Add(playerPrefab.name, playerPrefab);
        }
    }

    /// <summary>
    /// 인터넷 연결 체크 후 서버 연결 시도하기
    /// </summary>
    public void ConnectToServer()
    {
        if (Application.internetReachability.Equals(NetworkReachability.NotReachable))
        {
            GameMng.I.LogError("Network Disconnected", "NetworkMng");
            Debug.LogError("Network Disconnected");
        }
        else
        {
            PhotonNetwork.GameVersion = "1.0";      // 게임 버전
            PhotonNetwork.ConnectUsingSettings();   // 서버 연결
        }
    }

    #region CallBack

    /// <summary>
    /// Master권한으로 서버 연결 callback 함수
    /// </summary>
    public override void OnConnectedToMaster()
    {
        GameMng.I.Log("Joined, Lobby", "NetworkMng");

        if (SceneManager.GetActiveScene().name == "IntroScene")
            PhotonNetwork.CreateRoom("IntroRoom");
        else if (SceneManager.GetActiveScene().name == "MainScene")
            PhotonNetwork.CreateRoom("MainRoom");
        else if (SceneManager.GetActiveScene().name == "ConvienceStoreScene")
            PhotonNetwork.CreateRoom("ConvienceStoreRoom");
        // PhotonNetwork.JoinRoom("IntroRoom");      // 렌덤 room 들어가는곳
    }

    /// <summary>
    /// 뱅 들어가기 실패했을 때(존재하지 않는 방 접근) callback
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        GameMng.I.Log("Room Join Failed", "NetworkMng");
        if (SceneManager.GetActiveScene().name == "IntroScene")
            PhotonNetwork.CreateRoom("IntroRoom");
        else if (SceneManager.GetActiveScene().name == "MainScene")
            PhotonNetwork.CreateRoom("MainRoom");
        else if (SceneManager.GetActiveScene().name == "ConvienceStoreScene")
            PhotonNetwork.CreateRoom("ConvienceStoreRoom");
    }

    /// <summary>
    /// 방 생성 되었을때 callback함수
    /// </summary>
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }

    /// <summary>
    /// 방 들어간거 성공 했을때 callback 함수
    /// </summary>
    public override void OnJoinedRoom()
    {
        GameMng.I.Log($"{PhotonNetwork.CurrentRoom.Name}", "NetworkMng");
        Debug.Log($"<color=red>{PhotonNetwork.CurrentRoom.Name}</color>");

        StartCoroutine(this.CreatePlayer());
    }

    /// <summary>
    /// 에러가 발생핬을 때 호출되는 callback함수
    /// </summary>
    /// <param name="errorInfo"></param>
    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        GameMng.I.LogError($"OnErrorInfo : {errorInfo}", "NetworkMng");
        Debug.LogError($"OnErrorInfo : {errorInfo}");
    }

    #endregion

    /// <summary>
    /// 서버(room 접속) 성공시 player생성
    /// </summary>
    IEnumerator CreatePlayer()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(141.7f, 3.1f, -212.2f), Quaternion.identity, 0);
        yield return null;
    }

    /// <summary>
    /// 룸 나가기 (씬 바꿀때 사용)
    /// </summary>
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }
}