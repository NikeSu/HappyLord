using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanel : MonoBehaviour {

    public GameObject StartObj;
    public UIButton btnStartLogin;
    public UILogin LoginObj;
	// Use this for initialization
	void Start () {
        StartObj.SetActive(true);
        LoginObj.gameObject.SetActive(false);
        UIEventListener.Get(btnStartLogin.gameObject).onClick=OnStartLoginClicked;
	}

    private void OnStartLoginClicked(GameObject go)
    {
        StartObj.SetActive(false);
        LoginObj.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
