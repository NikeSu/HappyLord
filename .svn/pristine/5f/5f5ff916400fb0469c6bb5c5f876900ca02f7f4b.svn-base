using Sfs2X.Entities.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login_UserMessage : MonoBehaviour
{
    public UISprite sprite;
    public UIButton btn;
    // Use this for initialization
    void Start()
    {
        //Global.AddEventListener("test", CallBack);
        //UIEventListener.Get(btn.gameObject).onClick = BtnClicked;

        Global.AddEventListener("GetUserInfo", UserInfoGet);

        SFSObject obj = new SFSObject();
        obj.PutUtfString("name", Global.me.Name);
        Global.SendExtMessage("GetUserInfo", obj);

    }
    /// <summary>
    /// GetUserInfo拓展的回调，获取到用户信息
    /// </summary>
    /// <param name="evt"></param>
    private void UserInfoGet(NEvent evt)
    {
        SFSObject obj = evt.Data;
        print("金币数量为："+obj.GetUtfString("gold"));
        print("用户昵称为:"+obj.GetUtfString("usernick"));
        print("用户VIP等级为："+obj.GetUtfString("viplv"));
        print("用户礼券数量为："+obj.GetUtfString("quan"));
    }

    private void CallBack(NEvent evt)
    {
        SFSObject obj = evt.Data;
        string info = obj.GetUtfString("info");
        Debug.Log(info);
    }

    private void BtnClicked(GameObject go)
    {
        SFSObject obj = new SFSObject();

        obj.PutUtfString("nick", "nicky");

        Global.SendExtMessage("test", obj);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
