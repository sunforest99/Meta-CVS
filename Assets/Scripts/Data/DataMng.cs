using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

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

    private ProductData productData = new ProductData();
    public Dictionary<string, Product> objectdic = new Dictionary<string, Product>();

    /// <summary>
    /// json 로드하여 딕셔너리 만들기
    /// </summary>
    public void LoadObjectData()
    {
        try
        {
            TextAsset json = Resources.Load("Data/test") as TextAsset;
            productData = JsonUtility.FromJson<ProductData>(json.text);

            foreach (Product data in productData.row)
            {
                objectdic.Add(data.DESC_KOR, data);
            }
        }
        catch
        {
            Debug.LogError($"Json Load Failed");
        }
    }

    /// <summary>
    /// TODO : 나중에 더 좋게 (아이템 이름 일일히 입력함) 수정하기
    /// 딕셔너리에 있는 값 가져오기
    /// </summary>
    /// <param name="key">아이템 이름</param>
    public Product TryGetObjectValue(string key)
    {
        Product temp;
        objectdic.TryGetValue(key, out temp);
        return temp;
    }
}
