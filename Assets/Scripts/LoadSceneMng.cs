using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMng : MonoBehaviour
{
    /// <summary>
    ///     CVS로 들어가는 함수. 로딩화면 추가해야함.
    /// </summary>
    public void EnterCVS() {
        SceneManager.LoadScene("ConvienceStoreScene");
    }
}
