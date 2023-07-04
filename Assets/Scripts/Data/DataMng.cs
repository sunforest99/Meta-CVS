using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

#region  유저 데이터
[System.Serializable]
public class UserData
{
    public string name;
    public string phone;
    public string address;
    public string cardnum;
    public string exp;
    public string cvc;
    public string pin;

    public UserData() { }
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
#endregion

public class DataMng
{
    public UserData userData = new UserData();
    // json file (유저 정보) 내보내기
    public void SaveUserData()
    {
        Debug.Log(userData.name);
        string jsonData = JsonUtility.ToJson(userData, true);
        string path = Path.Combine(Application.dataPath, "userData.json");
        File.WriteAllText(path, jsonData);
    }

    public void LoadUserDataFromJson()
    {
        string path = Path.Combine(Application.dataPath, "userData.json");
        string jsonData = File.ReadAllText(path);
        userData = JsonUtility.FromJson<UserData>(jsonData);
    }

    private ObjectDatas objectDatas = new ObjectDatas();
    public Dictionary<string, ObjectData> objectdic = new Dictionary<string, ObjectData>();

    /// <summary>
    /// json 로드하여 딕셔너리 만들기
    /// </summary>
    public void LoadObjectData()
    {
        try
        {
            TextAsset json = Resources.Load("Data/test") as TextAsset;
            objectDatas = JsonUtility.FromJson<ObjectDatas>(json.text);

            foreach (ObjectData data in objectDatas.row)
            {
                objectdic.Add(data.DESC_KOR, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Json Load Failed");
        }
    }

    /// <summary>
    /// TODO : 나중에 더 좋게 (아이템 이름 일일히 입력함) 수정하기
    /// 딕셔너리에 있는 값 가져오기
    /// </summary>
    /// <param name="key">아이템 이름</param>
    public ObjectData TryGetObjectValue(string key)
    {
        ObjectData temp;
        objectdic.TryGetValue(key, out temp);
        return temp;
    }
}
