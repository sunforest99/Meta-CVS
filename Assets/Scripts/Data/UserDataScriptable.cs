using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "Data/UserData")]
public class UserDataScriptable : ScriptableObject
{
    public UserData userData;

    public bool CheckNullOrEmpty()
    {
        if (string.IsNullOrEmpty(userData.name)
        || string.IsNullOrEmpty(userData.phone)
        || string.IsNullOrEmpty(userData.address)
        || string.IsNullOrEmpty(userData.cardnum)
        || string.IsNullOrEmpty(userData.exp)
        || string.IsNullOrEmpty(userData.cvc)
        || string.IsNullOrEmpty(userData.pin)
        )
        {
            return false;
        }

        return true;
    }

    public void DataClear()
    {
        userData.name =null;
        userData.phone = null;
        userData.address = null;
        userData.cardnum = null;
        userData.exp = null;
        userData.cvc = null;
        userData.pin = null;
    }
}