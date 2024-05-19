using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadGame : MonoBehaviour
{
    public Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager.LoadNextScene();
    }
}
