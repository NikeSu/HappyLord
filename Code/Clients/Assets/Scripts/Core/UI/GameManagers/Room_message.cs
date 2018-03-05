using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_message : MonoBehaviour {
    #region  组件，变量
    public UILabel multi;
    public UILabel grade;
    public UILabel matchname;

    public UIButton CallLord;
    public UIButton CallLordNo;

    public GameObject mask;
    public UIButton btn_mask;

    public GameObject chat;
    public UIButton btn_chat;

    public GameObject start_sign;

    public UILabel CardsIs;

    public Card M_card;
    public Card S_card;

    private bool Xuanze;
    List<Card> cards;

    string[] cardvalues;
    Card D;

    public Dictionary<string, Card> XuanZeList = new Dictionary<string, Card>();
    public Card lastHitCard = null;
    public float keeptime = 0f;
    private bool isDragging = false;

    public UIButton btn_send;
    public UIButton btn_sendNo;

    List<Card> DCards;

    public GameObject gameoversign;
    public UILabel usernick;
    public UILabel other1nick;
    public UILabel other2nick;

    public UILabel time;

    public UISprite me;
    public UISprite other1;
    public UISprite other2;

    public GameObject O1;
    public GameObject O2;
    #endregion

    void Awake()
    {
        print("!-----" + Global.multi + "-------!");
        print("!-----" + Global.grade + "-------!");
        print("!-----" + Global.matchName + "-------!");
    }

	// Use this for initialization
	void Start () {

        O1.SetActive(false);
        O2.SetActive(false);

        usernick.text = Global.user_nick;

        multi.text = Global.multi.ToString();

        grade.text = Global.grade.ToString();

        matchname.text = Global.matchName; 

        UIEventListener.Get(CallLord.gameObject).onClick = CallLordHandler;
        UIEventListener.Get(CallLordNo.gameObject).onClick = CallLordNoHandler;

        Global.AddEventListener("test",TestHandler);

        UIEventListener.Get(btn_mask.gameObject).onClick = BtnMaskClicked;

        Global.AddEventListener("GetMaskInfo",MaskInfoHandler);


        Global.AddEventListener("RoomIsFull", RoomIsFullHandler);

        Global.AddEventListener("GameStart",GameStartHandler);

        Global.AddEventListener("ExtCardIs", ExtCardHandler);
        
        IsGameOrWait();

        Global.AddEventListener("GameExt", GameExtHandler);

        Global.AddEventListener("CardsOnDesk", CardsOnDeskHandler);

        UIEventListener.Get(btn_send.gameObject).onClick = ButtonClick;
        UIEventListener.Get(btn_sendNo.gameObject).onClick = SendNoButtonClick;

        Global.AddEventListener("GetExtCards", GetExtCardsHandler);

        Global.AddEventListener("ExtCardsSended", ExtCardsSendedHandler);

        Global.AddEventListener("GameOver", GameOverHandler);

        Global.AddEventListener("OnTurn", OnTurnHandler);

        Global.AddEventListener("CallLordTurn", CallLordTurnHandler);

        Global.AddEventListener("WhoIsLand", WhoIsLandHandler);
    }
    /// <summary>
    /// 给地主特殊标记
    /// </summary>
    /// <param name="evt"></param>
    private void WhoIsLandHandler(NEvent evt)
    {
        string name = evt.Data.GetUtfString("LandIs");
        if (name == other1nick.text)
        {
            other1.spriteName = "pz_Landlord";
        }
        if (name == other2nick.text)
        {
            other2.spriteName = "pz_Landlord";
        }
        if (name == Global.me.Name)
        {
            me.spriteName = "pz_Landlord";
        }
    }

    /// <summary>
    /// 是否轮到抢地主
    /// </summary>
    /// <param name="evt"></param>
    private void CallLordTurnHandler(NEvent evt)
    {
        CallLord.gameObject.SetActive(true);
        CallLordNo.gameObject.SetActive(true);
    }


    /// <summary>
    /// 是否轮到出牌
    /// </summary>
    /// <param name="evt"></param>
    private void OnTurnHandler(NEvent evt)
    {
        print("哈哈哈！到我出牌啦，小心啦！");
        btn_send.gameObject.SetActive(true);
        btn_sendNo.gameObject.SetActive(true);
    }


    /// <summary>
    /// 游戏结束处理
    /// </summary>
    /// <param name="evt"></param>
    private void GameOverHandler(NEvent evt)
    {
        Destroy(GameObject.Find("DCardParent"));
        start_sign.gameObject.SetActive(false);
        print("本场游戏结束");
        gameoversign.SetActive(true);

        btn_send.gameObject.SetActive(false);
        btn_sendNo.gameObject.SetActive(false);
    }

    /// <summary>
    /// 底牌分派完毕可以出牌
    /// </summary>
    /// <param name="evt"></param>
    private void ExtCardsSendedHandler(NEvent evt)
    {
        btn_send.gameObject.SetActive(true);
        btn_sendNo.gameObject.SetActive(true);
    }


    /// <summary>
    /// 拿到底牌，加入手牌
    /// </summary>
    /// <param name="evt"></param>
    private void GetExtCardsHandler(NEvent evt)
    {
        print("GetExtCards");
        evt.Data.GetUtfStringArray("ExtCards");

        for (int i = evt.Data.GetUtfStringArray("ExtCards").Length - 1; i >= 0; i--)
        {
            Card go = Instantiate(M_card);

            go.Value = evt.Data.GetUtfStringArray("ExtCards")[i];
            //Debug.Log(go.Color + "," + go.Number);
            go.transform.SetParent(GameObject.Find("CardParent").transform);
            go.transform.localScale = Vector3.one;
            go.name = "Card_" + go.Value;

            go.gameObject.SetActive(false);

            go.OnClickHandler = OnCardSelectHandler;
            cards.Add(go);

        }

        cards.Sort();

        RepositionExtCard();
    }


    /// <summary>
    /// 不出牌
    /// </summary>
    /// <param name="go"></param>
    private void SendNoButtonClick(GameObject go)
    {
        Room room = Global.NetMgr.SFS.LastJoinedRoom;
        
        Global.SFS.Send(new ExtensionRequest("GameExtends", new SFSObject(), Global.NetMgr.SFS.LastJoinedRoom));
        print("不出牌");
    }

    /// <summary>
    /// 不叫地主处理
    /// </summary>
    /// <param name="go"></param>
    private void CallLordNoHandler(GameObject go)
    {
        Room room = Global.NetMgr.SFS.LastJoinedRoom;
        
        SFSObject obj = new SFSObject();

        obj.PutInt("Multi", int.Parse(multi.text));
        obj.PutBool("YON", false);
        Global.NetMgr.SFS.Send(new ExtensionRequest("test", obj, room));

        CallLord.gameObject.SetActive(false);
        CallLordNo.gameObject.SetActive(false);

    }

    /// <summary>
    /// 记录出牌，显示在桌面上
    /// </summary>
    /// <param name="evt"></param>
    private void CardsOnDeskHandler(NEvent evt)
    {
        GameObject.Find("DCardParent").transform.DestroyChildren();
        
        for (int i = 0; i < GameObject.Find("DCardParent").transform.childCount; i++)
        {
            Destroy(GameObject.Find("DCardParent").transform.GetChild(i).gameObject);
            print("Destory" + i);
        }

        print("CardsOnDesk");
        
        //foreach (var item in evt.Data.GetUtfStringArray("DCards"))
        //{
        //    print("---" + item + "---");
        //}

        DCards = new List<Card>();
        for (int i = evt.Data.GetUtfStringArray("DCards").Length - 1; i >= 0; i--)
        {
            Card go = Instantiate(S_card);

            go.Value = evt.Data.GetUtfStringArray("DCards")[i];
            
            go.transform.SetParent(GameObject.Find("DCardParent").transform);
            go.transform.localScale = Vector3.one*0.5f;
            go.name = "Card_" + go.Value;

            go.transform.localPosition = new Vector3(55 * i, go.transform.parent.position.y,0);

            go.gameObject.SetActive(true);

            go.OnClickHandler = OnCardSelectHandler;
            DCards.Add(go);

        }
        DCards.Sort();
        XuanZeList.Clear();
    }

    /// <summary>
    /// 重新给手牌排序
    /// </summary>
    private void RepositionCard()
    {
        int j = 0;
        int z = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            Card NO = cards[i];
            if (j > 5)
            {
                j = 5;
            }
            if (cards.Count <= 14 && i == 0)
            {
                z = 14 - cards.Count;
            }
            NO.GetComponent<UIPanel>().depth = i + cards.Count;
            NO.transform.localPosition = new Vector3((55 + j * 4) * (i + z / 2), NO.transform.parent.position.y, NO.transform.parent.position.z);
            NO.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 重新给地主手牌排序
    /// </summary>
    private void RepositionExtCard()
    {
        int j = 0;
        int z = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            Card NO = cards[i];
            if (j > 5)
            {
                j = 5;
            }
            if (cards.Count <= 17 && i == 0)
            {
                z = 17 - cards.Count;
            }
            NO.GetComponent<UIPanel>().depth = i + cards.Count;
            NO.transform.localPosition = new Vector3((50 + j * 4) * (i + z / 2), NO.transform.parent.position.y, NO.transform.parent.position.z);
            NO.gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// 出牌回调处理
    /// </summary>
    /// <param name="evt"></param>
    private void GameExtHandler(NEvent evt)
    {
        print("！-------"+evt.Data+"-----!");

        if (evt.Data == null)
        {
            print("-----null-----");
        }
        if (evt.Data != null)
        {
            int index = 0;
            foreach (var key in XuanZeList.Keys)
            {
                D = XuanZeList[key];
                cardvalues[index] = D.Value;
                cards.Remove(D);
                index++;
                //print("Card_" + XuanZeList[key].Value);
                Destroy(GameObject.Find("Card_" + XuanZeList[key].Value));
            }
            //print(cardvalues.Length);
            Array.Clear(cardvalues, 0, cardvalues.Length);

            RepositionCard();

            Room room = Global.NetMgr.SFS.LastJoinedRoom;


            if (cards.Count == 0)
            {
                print("SendGameOverExtends");
                SFSObject eobj = new SFSObject();
                Global.SFS.Send(new ExtensionRequest("GameOverExtends", eobj, room));
            }

        }

        btn_sendNo.gameObject.SetActive(false);
        btn_send.gameObject.SetActive(false);
    }


    /// <summary>
    /// 点击出牌处理
    /// </summary>
    /// <param name="go"></param>
    private void ButtonClick(GameObject go)
    {
        Room room = Global.NetMgr.SFS.LastJoinedRoom;

        if (XuanZeList.Count <= 0)
        {
            return;
        }
        cardvalues = new string[XuanZeList.Count];
        int index = 0;
        foreach (var key in XuanZeList.Keys)
        {
            D = XuanZeList[key];
            cardvalues[index] = D.Value;
            index++;
        }
        SFSObject obj = new SFSObject();
        obj.PutUtfStringArray("cards", cardvalues);

            
        Global.SFS.Send(new ExtensionRequest("GameExtends", obj, room));
        print("!-------发送GameExtends---------!");
    }



    /// <summary>
    /// 拿到底牌并呈现
    /// </summary>
    /// <param name="evt"></param>
    private void ExtCardHandler(NEvent evt)
    {
        print("底牌出来了！");

        print("底牌数量："+ evt.Data.GetUtfStringArray("dipai").Length);

        List<Card> extcards = new List<Card>();
        for (int i = evt.Data.GetUtfStringArray("dipai").Length - 1; i >= 0; i--)
        {
           
            Card go = Instantiate(S_card);
            go.Value = evt.Data.GetUtfStringArray("dipai")[i];
            Vector3 parentpos = GameObject.Find("ExtCard_Container").transform.position;
            go.transform.SetParent(GameObject.Find("ExtCard_Container").transform);
            go.transform.localScale = Vector3.one*0.3f;
            go.transform.localPosition = new Vector3(parentpos.x + (i * 50), parentpos.y, parentpos.z);
           
            go.gameObject.SetActive(true);
            extcards.Add(go);
        }

    }


    /// <summary>
    /// 服务器开始游戏的回调，给玩家发牌
    /// </summary>
    /// <param name="evt"></param>
    private void GameStartHandler(NEvent evt)
    {
        O1.SetActive(true);
        O2.SetActive(true);

        other1nick.text = evt.Data.GetUtfString("other1");
        other2nick.text = evt.Data.GetUtfString("other2");
        evt.Data.GetUtfString("other1");
        print("Other1:"+ evt.Data.GetUtfString("other1"));

        evt.Data.GetUtfString("other2");
        print("Other2:"+ evt.Data.GetUtfString("other2"));

        print("游戏开始，发牌给各位玩家。");
        start_sign.SetActive(true);

        int cl = evt.Data.GetUtfStringArray("PlayersHandCard").Length;

        cards = new List<Card>();
        for (int i = evt.Data.GetUtfStringArray("PlayersHandCard").Length - 1; i >= 0; i--)
        {
            Card go = Instantiate(M_card);
            
            go.Value = evt.Data.GetUtfStringArray("PlayersHandCard")[i];
            //Debug.Log(go.Color + "," + go.Number);
            go.transform.SetParent(GameObject.Find("CardParent").transform);
            go.transform.localScale = Vector3.one;
            go.name = "Card_" + go.Value;

            go.gameObject.SetActive(false);

            go.OnClickHandler = OnCardSelectHandler;
            cards.Add(go);

        }
        cards.Sort();

        for (int i = 0; i < cards.Count; i++)
        {
            Card go = cards[i];

            go.GetComponent<UIPanel>().depth = i + cards.Count;
            go.transform.localPosition = new Vector3(55 * i, go.transform.parent.position.y, go.transform.parent.position.z);
            go.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                go.transform.localPosition += Vector3.up * 10;
            }
            else
            {
                go.transform.localPosition -= Vector3.up * 10;
            }
        }
       // CallLord.gameObject.SetActive(true);
        //CallLordNo.gameObject.SetActive(true);
    }

    /// <summary>
    /// 牌否被选中状态
    /// </summary>
    /// <param name="obj"></param>
    private void OnCardSelectHandler(bool obj)
    {
        print(obj);
    }


    /// <summary>
    /// 房间人数已满调用
    /// </summary>
    /// <param name="evt"></param>
    private void RoomIsFullHandler(NEvent evt)
    {
        
        //Room gameroom = (Room)evt.Data.GetSFSObject("GameRoom");
        //Global.NetMgr.SFS.LastJoinedRoom = gameroom;
        string gameroomname = evt.Data.GetUtfString("GameRoomName");
        
        Global.SendSFSMessage(new JoinRoomRequest(gameroomname));
        
        Global.isGameStart = true;
        IsGameOrWait();

    }

    
  
    /// <summary>
    /// 判断游戏是开始状态还是等待状态
    /// </summary>
    private void IsGameOrWait()
    {
        
        switch (Global.isGameStart)
        {
            case false:
                print("房间人数未满。。。。。。请耐心等待游戏开始");
                break;
            case true:
                print("房间人数已满。。。。。。游戏即将开始，祝您游戏愉快！");
                SFSObject obj = new SFSObject();
                Room room = Global.NetMgr.SFS.LastJoinedRoom;
                print(" Global.NetMgr.SFS.LastJoinedRoom :" + Global.NetMgr.SFS.LastJoinedRoom);
                Global.NetMgr.SFS.Send(new ExtensionRequest("StartGame", obj, room));
                break;
        }
    }


    /// <summary>
    /// 服务器发给客户端任务内容
    /// </summary>
    /// <param name="evt"></param>
    private void MaskInfoHandler(NEvent evt)
    {
        print(evt.Data.GetUtfString("mask"));

        evt.Data.GetUtfStringArray("mask1");
        print(evt.Data.GetUtfStringArray("mask1")[1]);
        mask.SetActive(true);
    }


    /// <summary>
    /// 点击任务按钮的点击处理（向服务器发送获取任务请求）
    /// </summary>
    /// <param name="go"></param>
    private void BtnMaskClicked(GameObject go)
    {
        print("向服务器发送获取任务请求");

        SFSObject obj = new SFSObject();
        Room room = Global.NetMgr.SFS.LastJoinedRoom;
        Global.NetMgr.SFS.Send(new ExtensionRequest("GetMaskInfo",obj,room));
        print("------发送请求ing------");
    }


    /// <summary>
    /// 系统反馈客户端本场游戏的倍数
    /// </summary>
    /// <param name="evt"></param>
    private void TestHandler(NEvent evt)
    {
        multi.text = evt.Data.GetInt("Multi").ToString();
    }


    /// <summary>
    /// 叫地主按钮按下的点击处理（发送房间拓展消息增加倍数）
    /// </summary>
    /// <param name="go"></param>
    private void CallLordHandler(GameObject go)
    {
        print("叫地主");
        Room room = Global.NetMgr.SFS.LastJoinedRoom;

        SFSObject obj = new SFSObject();

        obj.PutInt("Multi",int.Parse( multi.text ));
        obj.PutBool("YON",true);
        Global.NetMgr.SFS.Send(new ExtensionRequest("test", obj, room));

        CallLord.gameObject.SetActive(false);
        CallLordNo.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update () {
        RaycastHit lastHit = default(RaycastHit);
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(UICamera.mainCamera.ScreenPointToRay(Input.mousePosition), out lastHit))
            {
                if (lastHit.collider.transform != null && UICamera.lastHit.collider.name != "UI Root")
                {

                    Card c = UICamera.lastHit.collider.transform.parent.GetComponent<Card>();

                    if (c != null)
                    {

                        if (lastHitCard != c)
                        {
                            lastHitCard = c;
                            if (!c.Select)
                            {
                                c.Select = true;
                                if (!XuanZeList.ContainsKey(c.name))
                                {
                                    XuanZeList.Add(c.name, c);
                                }
                            }
                            else
                            {
                                c.Select = false;
                                if (XuanZeList.ContainsKey(c.name))
                                {
                                    XuanZeList.Remove(c.name);
                                }
                            }
                        }
                    }

                }
            }


        }
        if (Input.GetMouseButtonUp(0)) lastHitCard = null;

        time.text = System.DateTime.Now.Hour.ToString() + ":" + System.DateTime.Now.Minute.ToString();
    }

}


