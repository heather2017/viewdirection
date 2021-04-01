using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class test4 : MonoBehaviour
{
    Collider my_collider;
    GameObject myobject;
    GameObject mycam;
    public Image imgCircle;
    static int line = 0;
    string myscenename;



    public float totalTime = 5;


    public float gvrTimer;



    // Use this for initialization
    void Start()
    {


        myobject = GameObject.Find("door");
        my_collider = myobject.GetComponent<Collider>();
        mycam = GameObject.Find("Camera");
        myscenename = Application.loadedLevelName.ToString();
        gvrTimer = 0;
        imgCircle.fillAmount = 0;


    }




    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(mycam.GetComponent<Transform>().position, mycam.GetComponent<Transform>().forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        if (hit.point != Vector3.zero) // Vector3 is non-nullable; comparing to null is always false
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position = ray.GetPoint(3.0f);
        }
        if (hit.collider == my_collider)
        {

            gvrTimer += Time.deltaTime;
            imgCircle.fillAmount = gvrTimer / totalTime;

        }
        if (hit.collider != my_collider)
        {

            gvrTimer = 0;
            imgCircle.fillAmount = gvrTimer / totalTime;

        }

        if (gvrTimer > totalTime)
        {
            Application.Quit();
        }



    }
}

