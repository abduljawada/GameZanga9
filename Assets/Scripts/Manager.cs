using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public Vector2 targetResolution;
    int currentSceneIndex;

    void Awake(){
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start(){
        Camera camera = GetComponent<Camera>();

        float targetAspect = targetResolution.x / targetResolution.y;
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if(scaleHeight < 1f){
            camera.orthographicSize /= scaleHeight;
        }
    }

    public void Restart(){
        SceneManager.LoadScene(currentSceneIndex);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void LoadNextScene(){
        Time.timeScale = 1f;
        int nextSceneIndex = currentSceneIndex + 1;
        if(currentSceneIndex == 0){
            SceneManager.LoadScene(2);
            currentSceneIndex = 2;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
        else if(nextSceneIndex < SceneManager.sceneCountInBuildSettings){
            SceneManager.LoadScene(nextSceneIndex);
            currentSceneIndex = nextSceneIndex;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
        else
        {
            currentSceneIndex = 0;
            SceneManager.LoadScene(0);
        }
    }
}
