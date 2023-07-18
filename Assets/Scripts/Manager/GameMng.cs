using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net.Mail;

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
        MailSend();
    }

    public DataMng dataMng = new DataMng();

    public Dictionary<string, int> basketDict = new Dictionary<string, int>();
    
    public int coinCount;

    public RaycastHit? Raycast(Transform pos)
    {
        int layerMask = 1 << LayerMask.NameToLayer("Object");
        RaycastHit hit;
        if (Physics.Raycast(pos.position, pos.forward, out hit, 1000.0f, layerMask))
        {
            return hit;
        }

        return null;
    }

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
        scroll.value = 0.0f;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            ShowLog();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LoadingScene.Load("ConvienceStoreScene");
        }
    }

    void MailSend()
    {
        MailMessage mail = new MailMessage();
        try
        {
            mail.From = new MailAddress("이메일", "재목", System.Text.Encoding.UTF8);
            mail.Body = "<html><body>hello wrold</body></html>";
            mail.IsBodyHtml = true;

            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            SmtpClient smtpServer = new SmtpClient("smtp.naver.com");
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("아이디", "비밀번호");
            smtpServer.EnableSsl = true;
            smtpServer.Send(mail);

            Debug.Log("Send Mail");
        }
        catch(System.Exception e)
        {
            Debug.LogError(e);
        }
    }

    #endregion
}
