﻿using Sfs2X.Core;
using Sfs2X.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;
using Sfs2X.Requests;
using Sfs2X.Entities.Data;
using Sfs2X;

public class Global : MonoBehaviour {

    #region 服务器信息配置
    public static string ServerIP = "172.18.23.160";
    public static int ServerPort = 9933;
    public static string m_defaultZone = "DouDiZhu";
    public static string m_defaultRoom = "lobby";


    public static SmartFox SFS
    {
        get { return NetMgr.SFS; }
    }

    /// <summary>
    /// 是否是调试模式
    /// </summary>
    public static bool isDebugMode = true;

    public static User me;
    #endregion

    #region 公共管理类属性

    public static NetManager NetMgr = null;
    public static EventManager EvtMgr = null;
    public static ResManager ResMgr = null;
    
    #endregion

    #region 单例实现
    private static Global _instance;
    public static Global Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Global.ServerIP = ConfigReader.GetInstance().ServerIp;
            Global.ServerPort = ConfigReader.GetInstance().ServerPort;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region  通用属性

    //游戏场倍数
    public static int multi;
    //游戏场底分
    public static int grade;
    //游戏场名
    public static string matchName;
    //玩家的昵称
    public static string user_nick;
    //玩家的金币
    public static int user_gold;
    //玩家的点券
    public static int user_quan;


    //游戏是否开始
    public static bool isGameStart;

    //是否是地主
    public static bool isLand = false;

    #endregion

    void Start()
    {
        //初始化各管理器
        NetMgr = this.gameObject.AddComponent<NetManager>();
        ResMgr = this.gameObject.AddComponent<ResManager>();
        EvtMgr = this.gameObject.AddComponent<EventManager>();


        EvtMgr.Init();
        ResMgr.Init();
        NetMgr.Init(m_defaultZone);
        NetMgr.Connect(ServerIP, ServerPort);
    }

   

    #region   用于简化管理器的相关操作(包括网络，事件，资源)
    /// <summary>
    /// 向服务器端发送扩展请求
    /// </summary>
    /// <param name="cmd">扩展标识</param>
    /// <param name="data">扩展所需参数对象</param>
    public static void SendExtMessage(string cmd,SFSObject data)
    {
        NetMgr.SendExtMessage(cmd, data);
    }

    /// <param name="data">扩展所需参数对象</param>
    public static void SendExtMessage(string cmd, SFSObject data,Room room=null)
    {
        if (room == null)
        {
            NetMgr.SendExtMessage(cmd, data);
        }else
        {
            NetMgr.SendExtMessage(cmd, data, room);
        }
    }
    /// <summary>
    /// 向服务器端发送系统请求
    /// </summary>
    /// <param name="request">系统请求类型</param>
    public static void SendSFSMessage(IRequest request)
    {
        NetMgr.SendSFSMessage(request);
    }



    /// <summary>
    /// 用于向事件管理器中添加一个事件监听 
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="callback">回调函数</param>
    public static void AddEventListener(SFSEvent type, EventManager.ListenerCallBack callback)
    {
        EvtMgr.AddEventListener(type, callback);
    }
    /// <summary>
    /// 用于向事件管理器中添加一个事件监听 
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="callback">回调函数</param>
    public static void AddEventListener(String type, EventManager.ListenerCallBack callback)
    {
        EvtMgr.AddEventListener(type, callback);
    }
    /// <summary>
    /// 用于从事件管理器中移除特定事件监听 
    /// </summary>
    /// <param name="type">要移除的事件类型</param>
    /// <param name="callback">要移除事件对应的回调函数</param>
    public static void RemoveListener(SFSEvent type, EventManager.ListenerCallBack listener)
    {
        EvtMgr.RemoveListener(type, listener);
    }
    /// <summary>
    /// 用于从事件管理器中移除特定事件监听 
    /// </summary>
    /// <param name="type">要移除的事件类型</param>
    /// <param name="callback">要移除事件对应的回调函数</param>
    public static void RemoveListener(string type, EventManager.ListenerCallBack listener)
    {
        EvtMgr.RemoveListener(type, listener);
    }
    
    /// <summary>
    /// 移动指定类型的所有事件监听 
    /// </summary>
    /// <param name="type">要移除的事件类型</param>
    public static void RemoveListener(string type)
    {
        EvtMgr.RemoveListener(type);
    }
    /// <summary>
    /// 移动指定类型的所有事件监听 
    /// </summary>
    /// <param name="type">要移除的事件类型</param>
    public static void RemoveListener(SFSEvent type)
    {
        EvtMgr.RemoveListener(type);
    }


    /// <summary>
    /// 通过类型并指定资源名，加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <param name="resName"></param>
    /// <param name="isNew"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    public static T GetRes<T>(ResType type, string resName, bool isNew = true, bool cache = false) where T : UnityEngine.Object
    {
        return ResMgr.GetRes<T>(type, resName, isNew, cache);
    }
    #endregion

    public static void Log(params object[] infos)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in infos)
        {
            sb.Append(item.ToString()+"   ");
        }
        if (isDebugMode)
        {
            Debug.Log(sb);
        }
    }

   
}