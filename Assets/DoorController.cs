using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    //public void SetGazeAt(bool gazeAt) => gazeAt ? GetComponent<AudioSource>.Stop() : GetComponent<AudioSource>.Play();
    public AudioClip[] audios;

    void Start() {
        this.GetComponent<AudioSource>().clip = audios[0];
        this.GetComponent<AudioSource>().Play();
     
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            this.GetComponent<AudioSource>().Stop();

        }

    }






}