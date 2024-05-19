using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Manager manager;

    void Start(){
        manager = Manager.instance;
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            manager.Restart();
        }
        
    }
}
