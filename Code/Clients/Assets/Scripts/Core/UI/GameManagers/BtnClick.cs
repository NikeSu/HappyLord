using Sfs2X.Core;
using Sfs2X.Entities.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Sfs2X.Requests;
using Sfs2X.Entities;

public class BtnClick : BtnCtrl {

    void Start()
    {
        //Global.AddEventListener(ExtCmd.TEST,ExtensionHandler);
        Global.AddEventListener(SFSEvent.ROOM_JOIN, JoinRoom);
        Global.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, JoinRoomFailed);

        Global.AddEventListener("GetRoomMassage", RoomMassageHandler);

        Global.AddEventListener("WaitForGame", WaitForGameHandler);

        Global.AddEventListener("RoomIsFull", RoomIsFullHandler);
    }

    /// <summary>
    /// 房间人数满了，开始游戏
    /// </summary>
    /// <param name="evt"></param>
    private void RoomIsFullHandler(NEvent evt)
    {
        print("人数已满，可以开始游戏");
        Global.isGameStart = true;
    }


    /// <summary>
    /// 房间人数未满，等待游戏开始
    /// </summary>
    /// <param name="evt"></param>
    private void WaitForGameHandler(NEvent evt)
    {
        print("房间人数未满，等待游戏开始");
        Global.isGameStart = false;
    }

    private void RoomMassageHandler(NEvent evt)
    {
        Global.multi = evt.Data.GetInt("Multi");
        Global.grade = evt.Data.GetInt("Grade");
        Global.matchName = evt.Data.GetUtfString("MatchName");

        print(Global.matchName);
    }

    protected override void OnClickHandler(GameObject button)
    {
        base.OnClickHandler(button);

        

        switch (button.name) {
            case "Huanlei":
                //SFSObject obj = new SFSObject();
                //obj.PutUtfString("nick","超级VIP用户，您选择了欢乐场进行游戏");
                //Global.SendExtMessage("nick",obj);
                //print("欢迎来到欢乐场");
                //SceneManager.LoadScene("欢乐场");
                break;
            case"Bisai":
                
                print("欢迎来到比赛场");
                break;
            case "Laizi":
                
                print("欢迎来到癞子场");
                break;
            case "touxiang":
                
                break;
            case "btn_back":
                SceneManager.LoadScene("ModeSelect");
                break;
            case "Huanle_cj":
                Global.SendSFSMessage(new JoinRoomRequest("lobby"));
                print("发送进入lobby房间的请求");
               
                break;
        }
    }

    private void JoinRoomFailed(NEvent evt)
    {
        print("进入房间失败");
    }

    private void JoinRoom(NEvent evt)
    {
        print("进入房间成功");
        print("向服务器发送RoomExt");
        //Global.SendExtMessage("test", new SFSObject(), Global.SFS.LastJoinedRoom);
        //Global.NetMgr.SFS.Send(new ExtensionRequest("test",new SFSObject(),Global.NetMgr.SFS.LastJoinedRoom,true));

        print("我要跳啦！进入game场景");
        SceneManager.LoadScene("game");
    }

    private void ExtensionHandler(NEvent evt)
    {
        
        print(evt.Data.GetUtfString("nick"));
    }
}
