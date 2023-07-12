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
        Debug.Log(GameMng.I.dataMng.TryGetObjectValue("새우깡").NUTR_CONT1);
    }

    public DataMng dataMng = new DataMng();

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
}
