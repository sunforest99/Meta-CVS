using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("LoadScene");
    }

    IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
