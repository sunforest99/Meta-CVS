using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class LoadingScene : MonoBehaviour
{
    static string nextScene;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// 다음 씬 정해주기
    /// </summary>
    /// <param name="scene"></param>
    public static void Load(string scene)
    {
        nextScene = scene;
        SceneManager.LoadScene("LoadingScene");
    }

    /// <summary>
    /// network 방 나가고 맵 불러오기
    /// </summary>
    IEnumerator LoadScene()
    {
        NetworkMng.I.LeaveRoom();

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        while (!operation.isDone)
        {
            yield return null;
            Debug.Log(operation.progress);
        }

        SceneManager.LoadScene(nextScene);
    }
}
