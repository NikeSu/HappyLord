using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Email_manager : MonoBehaviour {
    public UIButton Email_close;
    public GameObject Email;
	// Use this for initialization
	void Start () {
        UIEventListener.Get(Email_close.gameObject).onClick = EmailCloseOnClicked;
	}

    private void EmailCloseOnClicked(GameObject go)
    {
        Email.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
