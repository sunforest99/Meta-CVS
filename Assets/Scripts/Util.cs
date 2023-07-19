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
        UserData uData = new UserData();
        // UI_Counter_Item ci = new UI_Counter_Item();
        MailMessage mail = new MailMessage();
        
        try
        {
            mail.From = new MailAddress(Config.address, Config.addressName, System.Text.Encoding.UTF8);
            mail.Subject = "Test";      // 메일 제목
             mail.Body = $@"
            <html>
                <body>
                    <div class='texts' style='margin: 3%; width: 957px; border: solid #020321; background-color: #020321;'>
                        <img src='https://cdn.discordapp.com/attachments/1111202912320880744/1130800532924141648/title.png'
                            alt='title' style='vertical-align: bottom; width: 957px;'>
                            <h2 class='Title' style='width: 957px; color: #ffffff; text-align: center; text-align: center'>
                                {uData.name} / {uData.phone}
                            </h2>
                        <img src='https://media.discordapp.net/attachments/1111202912320880744/1130774886709731368/totalSum.png'
                            style='vertical-align: bottom;'>
                            <h2 class='PaySum' style='width: 957px; top: 480px; left: 80px; color: aliceblue; margin-left: auto; margin-right: auto;text-align: center'>
                                2000원
                            </h2>
                        <img src='https://media.discordapp.net/attachments/1111202912320880744/1130774886290296862/payInfo.png'
                            style='vertical-align: bottom;'>
                            <h2 class='CreditInfo' style='width: 957px; color: aliceblue; text-align: center'>
                                {uData.cardnum} 카카오뱅크
                            </h2>
                        <img src='https://media.discordapp.net/attachments/1111202912320880744/1130774885996699708/address.png'
                            style='vertical-align: bottom;'>
                            <h2 class='Address' style='width: 957px; color: aliceblue;'>
                                {uData.address}
                            </h2>
                        <!-- <a href='mailto:metaCVS@gmail.com' class='button'
                        style='position: absolute; bottom: 20px; left: 30px; width: 500px; height: 40px;'>궁금한 것이 있으신가요? 문의하기</a>-->
                    </div>
                </body>
            </html>
            ";    // 내용
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
