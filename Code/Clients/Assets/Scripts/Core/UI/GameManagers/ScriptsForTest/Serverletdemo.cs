using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serverletdemo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(doRequest());
        }
	}

    private IEnumerator doRequest()
    {
        string url = "http://localhost:8080/DDZServlet/Login";
        

        WWWForm form = new WWWForm();
        form.AddField("username","test");
        form.AddField("password","test");
        WWW www = new WWW(url,form);
        yield return www;
        if (String.IsNullOrEmpty(www.error))
        {
            print(www.text);
        }
    }
}
