using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_game : MonoBehaviour {
    public UIButton test;

    public UIButton talk;

    public GameObject talk_container;
	// Use this for initialization
	void Start () {
        UIEventListener.Get(test.gameObject).onClick = TestHandler;

        Global.AddEventListener("GameStart", GameStartHandler);

        Global.AddEventListener("GameExt", GameExtHandler);

        UIEventListener.Get(talk.gameObject).onClick = TalkOnClick;
    }

    public void TalkOnClick(GameObject go)
    {
        talk_container.SetActive(true);
    }

    private void GameExtHandler(NEvent evt)
    {
        print("收到GameExt");
    }

    private void GameStartHandler(NEvent evt)
    {
        Debug.Log("收到GameStart");
    }

    public void TestHandler(GameObject go)
    {//Global.NetMgr.SFS.Send(new ExtensionRequest("StartGame", obj, room));
        //Debug.Log("发送StartGame");
        SFSObject obj = new SFSObject();
        Room room = Global.NetMgr.SFS.LastJoinedRoom;
        

        Global.SFS.Send(new ExtensionRequest("GameOverExtends", obj,room));
        print("GameOverExtends");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
