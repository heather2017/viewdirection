using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class rt : MonoBehaviour
{
    Collider my_collider;
    GameObject myobject;
    GameObject mycam;

    static int line = 0;
    string myscenename;



    public float totalTime = 2;

    System.DateTime starttime;

    int gvrTimer;


    // Use this for initialization
    void Start()
    {


        myobject = GameObject.Find("door");
        my_collider = myobject.GetComponent<Collider>();
        mycam = GameObject.Find("Camera");
        myscenename = Application.loadedLevelName.ToString();


        gvrTimer = 0;
        //imgCircle.fillAmount = 0;

    }

    void print(string info)

    {

        line++;

        string path = Application.dataPath + "/" + myscenename + "_LogFile.txt";

        StreamWriter sw;

        //Debug.Log (path);

        if (line == 1)

        {

            sw = new StreamWriter(path, false);

            string fileTitle = "logfile create time: " + System.DateTime.Now.ToString();

            sw.WriteLine(fileTitle);

        }

        else

        {

            sw = new StreamWriter(path, true);

        }

        string lineInfo = line + "\t" + "Time" + Time.time + ": ";

        sw.WriteLine(lineInfo);

        sw.WriteLine(info);

        Debug.Log(info);

        sw.Flush();

        sw.Close();

    }


    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(mycam.GetComponent<Transform>().position, mycam.GetComponent<Transform>().forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity);

        if (hit.collider == my_collider)
        {
            if (gvrTimer == 0)
            {
                starttime = System.DateTime.Now;
                gvrTimer = 1;
            }

            else
            {
                gvrTimer = GetSubSeconds(starttime, System.DateTime.Now);


                if (gvrTimer > totalTime)
                {

                    Application.Quit();

                }
            }


        }




    }
    public int GetSubSeconds(System.DateTime startTimer, System.DateTime endTimer)
    {
        System.TimeSpan startSpan = new System.TimeSpan(startTimer.Ticks);

        System.TimeSpan nowSpan = new System.TimeSpan(endTimer.Ticks);

        System.TimeSpan subTimer = nowSpan.Subtract(startSpan).Duration();

        //返回相差时长（返回值是相差的秒数）
        return subTimer.Seconds;
    }
}
