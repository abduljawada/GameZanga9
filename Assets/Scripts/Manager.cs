using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public Vector2 targetResolution;
    public float slowMotionSpeed;
    public float timeToReload;
    int currentSceneIndex = 2;

    void Awake(){
        instance = this;
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

    public IEnumerator Die(){
        PlayerController player = PlayerController.instance;
        Time.timeScale = slowMotionSpeed;
        Destroy(player.gameObject);
        //Instantiate(Particals)
        yield return new WaitForSeconds(timeToReload);
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneIndex);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

    }

    public void LoadNextScene(){
        Time.timeScale = 1f;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex < SceneManager.sceneCount){
            SceneManager.LoadScene(nextSceneIndex);
            currentSceneIndex = nextSceneIndex;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(0);
            currentSceneIndex = 0;
        }
    }
}
