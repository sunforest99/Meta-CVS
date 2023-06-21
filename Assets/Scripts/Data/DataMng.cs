using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class UserData
{
    string nickname;
    string name;
    string phone;
    string address;
}

public class DataMng : MonoBehaviour
{
    public UserData userData;
    // json file (유저 정보) 내보내기
    [ContextMenu("To Json Data")]
    void SaveUserData() {
        string jsonData = JsonUtility.ToJson(userData, true);
        string path = Path.Combine(Application.dataPath, "userData.json");
        File.WriteAllText(path,jsonData);
    }
    
    [ContextMenu("From Json Data")]
    void LoadUserDataFromJson() {
        string path = Path.Combine(Application.dataPath, "userData.json");
        string jsonData = File.ReadAllText(path);
        userData = JsonUtility.FromJson<UserData>(jsonData);
    }
}
