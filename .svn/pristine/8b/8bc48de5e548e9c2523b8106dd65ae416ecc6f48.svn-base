using Sfs2X.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sfs2X.Requests;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;

public class UILogin : MonoBehaviour {
    public UIInput user;
    public UIInput pwd;
    public UIButton btnLogin;
    private string UserLoginId;
    private string UserLoginPwd;

    public UIButton btnReg;

    public GameObject Reg;
    public GameObject log;
    public UIButton close;
    public UIButton RegSure;


    public UIInput Reg_name;
    public UIInput Reg_pwd;

    public string url = "http://172.18.23.160/juedi/reg.php";
    // Use this for initialization
    void Start () {
        Global.EvtMgr.AddEventListener(SFSEvent.CONNECTION,OnConnection);//连接服务器监听
        
        Global.EvtMgr.AddEventListener(SFSEvent.LOGIN,UserLogin);//用户登录成功监听
        Global.EvtMgr.AddEventListener(SFSEvent.LOGIN_ERROR,UserLoginFailed);//用户登录出错监听

        Global.EvtMgr.AddEventListener("FetchUserInfo",OnFetchUserInfoHandler);//FetchUserInfo监听
        Global.AddEventListener("login1", OnLogin1Handler);//login1监听

        Global.AddEventListener("login2",OnLogin2Handler);//login2监听



        //以下为按钮点击事件监听
        UIEventListener.Get(btnLogin.gameObject).onClick = OnLoginClicked;

        UIEventListener.Get(btnReg.gameObject).onClick = OnRegClicked;

        UIEventListener.Get(close.gameObject).onClick = OnCloseClicked;

        UIEventListener.Get(RegSure.gameObject).onClick = OnRegSureClicked;
    }


    /// <summary>
    /// 发送Http请求，建立一个账户
    /// </summary>
    /// <param name="go"></param>
    private void OnRegSureClicked(GameObject go)
    {
        //获取一个http请求的对象
        HttpPostRequest hp = Global.NetMgr.GetHttpRequest();
        //设置地址
        hp.SetURL(url);
        hp.PutString("username", Reg_name.value);
        hp.PutString("password", Reg_pwd.value);

        hp.OnComplete += Hp_OnComplete;
        //发出请求
        hp.StartRequest();
    }
    /// <summary>
    /// 请求结果反馈
    /// </summary>
    /// <param name="result"></param>
    private void Hp_OnComplete(string result)
    {
        print("得到结果:" + result);
    }

    /// <summary>
    /// 关闭注册弹窗
    /// </summary>
    /// <param name="go"></param>
    private void OnCloseClicked(GameObject go)
    {
        Reg.SetActive(false );
        log.SetActive(true );
    }


    /// <summary>
    /// 打开注册弹窗
    /// </summary>
    /// <param name="go"></param>
    private void OnRegClicked(GameObject go)
    {
        Reg.SetActive(true);
        log.SetActive(false);
    }

    /// <summary>
    /// 用户登录验证
    /// </summary>
    /// <param name="go"></param>
    private void OnLoginClicked(GameObject go)
    {
        print("用户登录验证");

        UserLoginId = user.value;
        UserLoginPwd = pwd.value;

        Global.NetMgr.SendSFSMessage(new LoginRequest(UserLoginId, UserLoginPwd, Global.m_defaultZone));
    }


    /// <summary>
    /// 测试用  实际中未用到
    /// </summary>
    /// <param name="evt"></param>
    private void OnLogin2Handler(NEvent evt)
    {
        print(evt.Data.GetUtfString("message"));
    }


    /// <summary>
    /// 用户进入大区回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnLogin1Handler(NEvent evt)
    {
       
        print(evt.Data.GetUtfString("id"));
        print("尊敬的用户 "+evt.Data.GetUtfString("id")+" 您好，欢迎进入大区");

    }



    private void OnFetchUserInfoHandler(NEvent evt)
    {
        SFSObject obj = evt.BaseEvt.Params["params"] as SFSObject;
        print(obj.GetUtfString("zone"));
        print(obj.GetInt("id"));
    }

    /// <summary>
    /// 游戏大区连接失败回调
    /// </summary>
    /// <param name="evt"></param>
    private void UserLoginFailed(NEvent evt)
    {
        Global.Log("游戏大区连接失败");
    }


    /// <summary>
    /// 游戏大区连接成功回调
    /// </summary>
    /// <param name="evt"></param>
    private void UserLogin(NEvent evt)
    {

        Global.Log("用户登录连接成功");
        //储存自己
        Global.me = evt.BaseEvt.Params["user"] as User;
        SceneManager.LoadScene("ModeSelect");

    }

   
    /// <summary>
    /// 服务器连接成功回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnConnection(NEvent evt)
    {
        Global.Log("服务器连接成功");
    }


    // Update is called once per frame
    void Update () {
		
	}
}
