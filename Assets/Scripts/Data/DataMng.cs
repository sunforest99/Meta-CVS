using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class UserData
{
    string name;
    string phone;
    string address;
    string cardnum;
    string exp;
    string cvc;
    string pin;

    public UserData(string name, string phone, string address, string cardnum, string exp, string cvc, string pin)
    {
        this.name = name;
        this.phone = phone;
        this.address = address;
        this.cardnum = cardnum;
        this.exp = exp;
        this.cvc = cvc;
        this.pin = pin;
    }
}

public class DataMng : MonoBehaviour
{
    // public static UserData userData;
    // json file (유저 정보) 내보내기
    [ContextMenu("To Json Data")]
    public static void SaveUserData(UserData userData) {
        string jsonData = JsonUtility.ToJson(userData, true);
        string path = Path.Combine(Application.dataPath, "userData.json");
        File.WriteAllText(path,jsonData);
    }
    
    [ContextMenu("From Json Data")]
    public static UserData LoadUserDataFromJson() {
        string path = Path.Combine(Application.dataPath, "userData.json");
        string jsonData = File.ReadAllText(path);
        return JsonUtility.FromJson<UserData>(jsonData);
    }
}
