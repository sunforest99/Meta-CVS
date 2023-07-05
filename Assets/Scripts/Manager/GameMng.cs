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
        _instance = this;
        // NetworkMng.I.ConnectToServer();
        DontDestroyOnLoad(this);
        Screen.SetResolution(1280, 720, false);
    }

    public DataMng dataMng = new DataMng();
}
