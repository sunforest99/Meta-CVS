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

    public static void Load(string scene)
    {
        nextScene = scene;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        NetworkMng.I.LeaveRoom();
        NetworkMng.I.JoinRoom(nextScene);

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        while (!operation.isDone)
        {
            yield return null;
            Debug.Log(operation.progress);
        }

        SceneManager.LoadScene(nextScene);
    }
}
