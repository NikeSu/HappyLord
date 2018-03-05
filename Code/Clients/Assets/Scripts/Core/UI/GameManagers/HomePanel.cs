﻿using Sfs2X.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;

public class HomePanel : BaseUI
{

    public UISprite header;

    public UILabel gold;
    public UILabel quan;
    public UILabel usernick;

    public GameObject Xinxi;
    public GameObject beibao;
    public GameObject setting;

    public UILabel m_gold;
    public UILabel m_quan;
    public UILabel m_usernick;

    public UILabel signature;

    public UIInput u_nick;
    public UIInput u_personalwords;

    public UIButton btnEmail;
    public GameObject Email;


    public UILabel Email_title;
    public UILabel Email_time;
    public UILabel Email_content;

    public GameObject Shop_obj;

    public UIButton btn_commit;
    

    void Start()
    {
        //Global.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoomHandler);
        //Global.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnJoinRoomErrorHandler);

        Global.AddEventListener("GetUserInfo", UserInfoGet);//监听用户信息拓展

        //向服务器获取用户信息
        SFSObject obj = new SFSObject();
        obj.PutUtfString("name", Global.me.Name);
        Global.SendExtMessage("GetUserInfo", obj);

        Global.AddEventListener("ChangeUserInfo",UserInfoChanged);//监听用户信息修改

        Global.AddEventListener("GetEmail", EmailGetHandler);//监听邮件拓展

        Global.AddEventListener("GetCommodityInfo", GetCommodityInfoHandler);//监听商城拓展

        Global.AddEventListener("BackpackMessage", GetBackpackMessageHandler);

        UIEventListener.Get(btn_commit.gameObject).onClick = CommitChangeInfo;
        

    }


    /// <summary>
    /// 获取背包信息
    /// </summary>
    /// <param name="evt"></param>
    private void GetBackpackMessageHandler(NEvent evt)
    {
        print("拿到背包信息");

        string lv = evt.Data.GetUtfString("gamelv");

        print(lv);
    }

    /// <summary>
    /// 向服务器发送要修改的个人信息
    /// </summary>
    /// <param name="go"></param>
    private void CommitChangeInfo(GameObject go)
    {
        SFSObject sobj = new SFSObject();
        sobj.PutUtfString("name", Global.me.Name);
        print(Global.me.Name);
        sobj.PutUtfString("usernick", u_nick.value);
        print(u_nick.value);
        sobj.PutUtfString("personalwords", u_personalwords.value);
        print(u_personalwords.value);
        Global.SendExtMessage("ChangeUserInfo", sobj);
        print("修改 并 保存修改的信息");
    }



    /// <summary>
    /// 从服务器获取商城商品信息
    /// </summary>
    /// <param name="evt"></param>
    private void GetCommodityInfoHandler(NEvent evt)
    {
        print("GetCommodityInfoHandler");

        print(evt.Data.GetUtfString("coinSprite"));
        print(evt.Data.GetInt("price"));
        print(evt.Data.GetInt("num"));
        print(evt.Data.GetUtfString("cmd_name"));
        print(evt.Data.GetUtfString("gift"));

        Shop_obj.gameObject.SetActive(true);

    }

    /// <summary>
    /// 服务器响应邮件拓展回调
    /// </summary>
    /// <param name="evt"></param>
    private void EmailGetHandler(NEvent evt)
    {
        print("服务器响应邮件");

        SFSObject email = evt.Data;

        print( email.GetUtfString("title"));
        print( email.GetUtfString("time"));
        print( email.GetUtfString("content"));

        Email_title.text = email.GetUtfString("title");
        Email_time.text = email.GetUtfString("time");
        Email_content.text = email.GetUtfString("content");

    }


    /// <summary>
    /// 修改用户信息成功回调
    /// </summary>
    /// <param name="evt"></param>
    private void UserInfoChanged(NEvent evt)
    {
        print("!---------修改信息成功-----------!");

        m_usernick.text = evt.Data.GetUtfString("usernick");
        usernick.text = evt.Data.GetUtfString("usernick");
        print(m_usernick.text);
    }


    /// <summary>
    /// 从服务器拿到用户的信息
    /// </summary>
    /// <param name="evt"></param>
    private void UserInfoGet(NEvent evt)
    {
        SFSObject obj = evt.Data;
       

        Global.user_nick = obj.GetUtfString("usernick");
        Global.user_gold = int.Parse(obj.GetUtfString("gold"));
        Global.user_quan = int.Parse(obj.GetUtfString("quan"));

        print("金币数量为：" + Global.user_gold);
        print("用户昵称为:" + Global.user_nick);
        print("用户礼券数量为：" + Global.user_quan);

        gold.text = Global.user_gold.ToString();

        quan.text = Global.user_quan.ToString();

        usernick.text = Global.user_nick;

        m_gold.text = gold.text;
        m_quan.text = quan.text;
        m_usernick.text = usernick.text;

        signature.text = obj.GetUtfString("personalwords");
    }


    /// <summary>
    /// 加入房间失败的拓展回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnJoinRoomErrorHandler(NEvent evt)
    {
        print("加入房间“lobby”失败");
    }


    /// <summary>
    /// 加入房间成功的拓展回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnJoinRoomHandler(NEvent evt)
    {
        print("加入房间“lobby”成功");
        Global.SendExtMessage("test", new SFSObject(), Global.SFS.LastJoinedRoom);
        print("房间拓展消息发送成功");
    }


   
 

    /// <summary>
    /// 处理按钮点击
    /// </summary>
    /// <param name="button"></param>
    protected override void OnClickHandler(GameObject button)
    {
        base.OnClickHandler(button);
        

        switch (button.name)
        {
            case "Shangcheng_icon":
                print("打开商城Panel");
                
                SFSObject shop_obj = new SFSObject();
                Global.SendExtMessage("GetCommodityInfo", shop_obj);
                print("SendExtMessage_GetCommodityInfo");
                break;
            case "PaiHang_icon":
                SceneManager.LoadScene("paihangbang");
                break;
            case "ShuiHuan_icon":
                SceneManager.LoadScene("paihangbang");
                break;
            case "HuoDong_icon":
                SceneManager.LoadScene("Activity");
                break;
            case "email":
                Email.gameObject.SetActive(true);

                SFSObject eobj = new SFSObject();
                
                Global.SendExtMessage("GetEmail", eobj);
                print("向服务器发送GetEmail请求");
                break;
            case "touxiang":                            
                   Xinxi.SetActive(true);                         
                break;
            case "BeiBao_icon":

                SFSObject obj = new SFSObject();
                Global.SendExtMessage("BackpackMessage",obj);
                beibao.SetActive(true);
                break;
            case "Setting":
                setting.SetActive(true);
                break;
                case "Huanlei":
                print("选择欢乐场进行游戏");
                //Global.SendSFSMessage(new JoinRoomRequest("lobby"));//告诉服务器我要进哪个房间，“lobby”是房间名
                SceneManager.LoadScene("欢乐场");
                break;
            case "Bisai":
                SceneManager.LoadScene("比赛场");
                break;
            case "Laizi":
                SceneManager.LoadScene("癞子场");                
                break;
            case "Xiaoyouxi":
                SceneManager.LoadScene("Realex");
                break;
        }
    }
}
