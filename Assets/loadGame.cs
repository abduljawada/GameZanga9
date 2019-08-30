using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Manager manager = Manager.instance;
        manager.LoadNextScene();
    }
}
