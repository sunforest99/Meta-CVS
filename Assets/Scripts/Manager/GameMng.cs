using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    static GameMng _instance;

    static public GameMng I
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Instance is null");
            return _instance;
        }
    }

    private void Awake()
    {
        Screen.SetResolution(1280, 720, false);
        _instance = this;
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(this.transform.parent);
        dataMng.LoadObjectData();
        // Util.MailSend();
    }

    public DataMng dataMng = new DataMng();
    public UserDataScriptable userDataSaveFile;

    public Dictionary<string, int> basketDict = new Dictionary<string, int>();

    public int coinCount;

    /// <summary>
    /// Raycast Object레이어에서 작동
    /// </summary>
    /// <param name="pos">시작위치</param>
    /// <returns></returns>
    public RaycastHit? Raycast(Transform pos)
    {
        int layerMask = 1 << LayerMask.NameToLayer("Object");
        RaycastHit hit;
        if (Physics.Raycast(pos.position, pos.forward, out hit, 10.0f, layerMask))
        {
            return hit;
        }

        return null;
    }

    public int totalPrice = 0;

    public ProductScriptable productObjectData;

    public Camera mainCamera;

    #region Debug Console

    [SerializeField] GameObject debugConsole;
    [SerializeField] TMPro.TMP_Text logText;
    System.Text.StringBuilder logMessage = new System.Text.StringBuilder();
    [SerializeField] UnityEngine.UI.Scrollbar scroll;

    public void Log(string logMsg, string tag = "Log")
    {
        logMessage.Append($" {System.DateTime.Now:HH:mm:ss} {tag} : {logMsg}\n");
        logText.text = logMessage.ToString();
        scroll.value = 0.0f;
    }
    public void LogError(string logMsg, string tag = "Error")
    {
        logMessage.Append($"<color=red> {System.DateTime.Now:HH:mm:ss} {tag} : {logMsg}</color>\n");
        logText.text = logMessage.ToString();
        scroll.value = 0.0f;
    }
    public void LogWarning(string logMsg, string tag = "Warning")
    {
        logMessage.Append($"<color=yellow> {System.DateTime.Now:HH:mm:ss} {tag} : {logMsg}</color>\n");
        logText.text = logMessage.ToString();
        scroll.value = 0.0f;
    }

    public void ShowLog()
    {
        debugConsole.SetActive(!debugConsole.activeSelf ? true : false);
        debugConsole.transform.position = I.mainCamera.transform.position + (GameMng.I.mainCamera.transform.forward * 10.0f);
        debugConsole.transform.rotation = I.mainCamera.transform.rotation;
        scroll.value = 0.0f;
    }
    #endregion

    #region 빌트 테스트
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ShowLog();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadingScene.Load("ConvienceStoreScene");
        }
    }

    public void OnApplicationQuit()
    {
        userDataSaveFile.DataClear();
    }

    #endregion
}
