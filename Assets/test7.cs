using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class test7 : MonoBehaviour
{
    Collider my_collider;
    Collider last_collider;
    Collider now_collider;

    GameObject myobject;
    GameObject mycam;
   
    static int line = 0;
    string myscenename;
    string loginfo;



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
       

        last_collider = my_collider;

        DeleteFile(Application.persistentDataPath, "VRlog.txt");
        CreateFile(Application.persistentDataPath, "VRlog.txt", "LOG_record:");
    }

    void CreateFile(string path, string name, string info)
    {
        //文件流信息
        StreamWriter sw;
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
            sw = t.CreateText();
        }
        else
        {
            //如果此文件存在则打开
            sw = t.AppendText();
        }
        //以行的形式写入信息
        sw.WriteLine(info);
        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();
    }
    void DeleteFile(string path, string name)
    {
        File.Delete(path + "//" + name);

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
            loginfo = System.DateTime.Now.ToString() + "   " + this.transform.position.ToString();
            CreateFile(Application.persistentDataPath, "VRlog.txt", loginfo);
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
                
            }

        }
        else
        {
            last_collider = now_collider;
            gvrTimer = 0;
            

        }

        if (gvrTimer > (thinkTime + waitTime))
        {
            if (now_collider == my_collider)
            {
                loginfo = System.DateTime.Now.ToString() + "    Door_success";
                CreateFile(Application.persistentDataPath, "VRlog.txt", loginfo);
            }
            else
            {
                loginfo = System.DateTime.Now.ToString() + "    Door_fail";
                CreateFile(Application.persistentDataPath, "VRlog.txt", loginfo);
            }
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            // Application.Quit();
        }



    }
}


