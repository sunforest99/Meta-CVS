using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net.Mail;
using MetaCVSConfig;

public class Util : MonoBehaviour
{
    
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    /// <summary>
    /// 이메일 보내기
    /// </summary>
    public static void MailSend()
    {
        // UI_Counter_Item ci = new UI_Counter_Item();
        MailMessage mail = new MailMessage();
        
        try
        {
            mail.From = new MailAddress(Config.address, Config.addressName, System.Text.Encoding.UTF8);
            mail.Subject = "MetaCVS 구매를 해주셔서 감사합니다!";      // 메일 제목
            mail.Body = Config.GetContext();    // 내용
            mail.IsBodyHtml = true;

            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            SmtpClient smtpServer = new SmtpClient(Config.smtpAddress);
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Port = Config.port;
            smtpServer.Credentials = new System.Net.NetworkCredential(Config.address, Config.pwd);
            smtpServer.EnableSsl = true;
            smtpServer.SendAsync(mail, mail);

            Debug.Log("Send Mail");
        }
        catch(System.Exception e)
        {
            Debug.LogError(e);
        }
    }
}
