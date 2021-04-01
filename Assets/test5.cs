using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class test5 : MonoBehaviour
{
    Collider my_collider;
    Collider last_collider;
    Collider now_collider;

    GameObject myobject;
    GameObject mycam;
    public Image imgCircle;
    static int line = 0;
    string myscenename;



    public float thinkTime = 5;
    public float waitTime = 2;



    public float gvrTimer;
    public float delayTimer;



    // Use this for initialization
    void Start()
    {


        myobject = GameObject.Find("door");
        my_collider = myobject.GetComponent<Collider>();
        mycam = GameObject.Find("Camera");
        myscenename = Application.loadedLevelName.ToString();
        gvrTimer = 0;
        imgCircle.fillAmount = 0;

        last_collider = my_collider;
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

        now_collider = hit.collider;

        if (now_collider == last_collider)
        {

            gvrTimer += Time.deltaTime;

            if (gvrTimer >= waitTime)
            {
                delayTimer = gvrTimer - waitTime;
                imgCircle.fillAmount = delayTimer / thinkTime;
            }

        }
        else
        {
            last_collider = now_collider;
            gvrTimer = 0;
            imgCircle.fillAmount = 0;

        }

        if (gvrTimer > (thinkTime+waitTime))
        {
           
            Application.Quit();
        }



    }
}

