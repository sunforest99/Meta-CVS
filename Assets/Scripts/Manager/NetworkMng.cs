using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

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
        _Instance = this;
        DontDestroyOnLoad(this);
        ConnectToServer();
    }

    /// <summary>
    /// 서버(room 접속) 성공시 player생성
    /// </summary>
    IEnumerator CreatePlayer()
    {
        // TODO : 캐릭터 생성
        // PhotonNetwork.Instantiate("PlayerVR_1", new Vector3(0, 2, 0), Quaternion.identity, 0);

        yield return null;
    }

    /// <summary>
    /// 인터넷 연결 체크 후 서버 연결 시도하기
    /// </summary>
    public void ConnectToServer()
    {
        if (Application.internetReachability.Equals(NetworkReachability.NotReachable))
        {
            Debug.LogError("Network Disconnected");
        }
        else
        {
            // TODO 로그인
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
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom();      // 렌덤 room 들어가는곳
    }

    /// <summary>
    /// 랜덤 방 들어가기 실패 했을때 callback 함수
    /// </summary>
    /// <param name="retrunCode">에러 코드</param>
    /// <param name="message">에러 메시지</param>
    public override void OnJoinRandomFailed(short retrunCode, string message)
    {
        Debug.Log("No Room");
        PhotonNetwork.CreateRoom("myroom");     // 방 생성
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
        StartCoroutine(this.CreatePlayer());
    }

    /// <summary>
    /// 에러가 발생핬을 때 호출되는 callback함수
    /// </summary>
    /// <param name="errorInfo"></param>
    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        Debug.LogError($"OnErrorInfo : {errorInfo}");
    }
    #endregion

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }
}