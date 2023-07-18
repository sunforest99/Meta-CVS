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

    private void OnGUI()
    {

    }

    public static void MailSend()
    {
        MailMessage mail = new MailMessage();
        try
        {
            mail.From = new MailAddress(Config.address, Config.addressName, System.Text.Encoding.UTF8);
            mail.To.Add("수신자 이메일");
            mail.Subject = "Test";      // 메일 제목
            mail.Body = $"<html><body><img src='{Config.imgAddress}'> </img> hello wrold </body></html>";    // 내용
            
            // Attachment inline = new Attachment("./metaCVSreceipt.png");
            // inline.ContentDisposition.Inline = true;
            // inline.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
            // // inline.ContentId = 
            // inline.ContentType.MediaType = "image/png";
            // inline.ContentType.Name = "metaCVSreceipt.png";

            // mail.Attachments.Add(inline);
            mail.IsBodyHtml = true;

            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            SmtpClient smtpServer = new SmtpClient(Config.smtpAddress);
            smtpServer.UseDefaultCredentials = false;
            smtpServer.Port = Config.port;
            smtpServer.Credentials = new System.Net.NetworkCredential(Config.address, Config.pwd);
            smtpServer.EnableSsl = true;
            smtpServer.Send(mail);

            Debug.Log("Send Mail");
        }
        catch(System.Exception e)
        {
            Debug.LogError(e);
        }
    }
}
