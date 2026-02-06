using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class asyncLoad : MonoBehaviour
{

    public float timer = 3;
    public bool go;
    public bool go1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadScene());
    }
    private void Update()
    {
        if (go)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) go1 = true;
        }

    }
    IEnumerator LoadScene()
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(1);
        loadScene.allowSceneActivation = false;

        while (!loadScene.isDone)
        {
            if (loadScene.progress >= 0.9f)
            {
                go = true;
                if (go1) loadScene.allowSceneActivation=true;
            }
            yield return null;
        }
    }
}
