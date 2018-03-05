using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_manager : MonoBehaviour {

    public UIButton ShopClose;

    public GameObject Shop_obj;

	// Use this for initialization
	void Start () {
        UIEventListener.Get(ShopClose.gameObject).onClick = ShopCloseClicked;
	}

    private void ShopCloseClicked(GameObject go)
    {
        Shop_obj.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
