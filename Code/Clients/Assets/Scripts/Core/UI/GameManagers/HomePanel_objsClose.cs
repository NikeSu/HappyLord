using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel_objsClose : MonoBehaviour {
    public UIButton btn_shopclose;
    public GameObject shop;

    public UIButton btn_emailclose;
    public GameObject email;

    public UIButton btn_bagclose;
    public GameObject bag;

    public UIButton btn_settingclose;
    public GameObject setting;

    public UIButton btn_infoclose;
    public GameObject info;
    // Use this for initialization
    void Start () {
        UIEventListener.Get(btn_shopclose.gameObject).onClick = ShopCloseHandler;
        UIEventListener.Get(btn_emailclose.gameObject).onClick = EmailCloseHandler;
        UIEventListener.Get(btn_bagclose.gameObject).onClick = BagCloseHandler;
        UIEventListener.Get(btn_settingclose.gameObject).onClick = SettingCloseHandler;
        UIEventListener.Get(btn_infoclose.gameObject).onClick = InfoCloseHandler;
    }

    /// <summary>
    /// 关闭信息
    /// </summary>
    /// <param name="go"></param>
    private void InfoCloseHandler(GameObject go)
    {
        print("closeinfo");
        info.gameObject.SetActive(false);
    }

    /// <summary>
    /// 关闭设置
    /// </summary>
    /// <param name="go"></param>
    private void SettingCloseHandler(GameObject go)
    {
        print("closeesetting");
        setting.gameObject.SetActive(false);
    }


    /// <summary>
    /// 关闭背包
    /// </summary>
    /// <param name="go"></param>
    private void BagCloseHandler(GameObject go)
    {
        print("closeebag");
        bag.gameObject.SetActive(false);
    }


    /// <summary>
    /// 关闭邮件
    /// </summary>
    /// <param name="go"></param>
    private void EmailCloseHandler(GameObject go)
    {
        print("closeemail");
        email.gameObject.SetActive(false);
    }

    /// <summary>
    /// 关闭商城
    /// </summary>
    /// <param name="go"></param>
    private void ShopCloseHandler(GameObject go)
    {
        print("closeshop");
        shop.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
