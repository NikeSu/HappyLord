using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Card M_card;
    private bool Xuanze;


    List<Card> cards;

    string[] cardvalues;
    Card D;


    public Dictionary<string, Card> XuanZeList = new Dictionary<string, Card>();
    public Card lastHitCard = null;
    public float keeptime = 0f;
    private bool isDragging = false;

    // Use this for initialization
    void Start()
    {
        //监听事件
        Global.AddEventListener("GameExt", GameExtHandler);

        GameObject button = GameObject.Find("Btn_Send");
        UIEventListener.Get(button).onClick = ButtonClick;
    }

    private void GameExtHandler(NEvent evt)
    {        
        int index = 0;
        foreach (var key in XuanZeList.Keys)
        {
            D = XuanZeList[key];
            cardvalues[index] = D.Value;
            cards.Remove(D);
            index++;
            Destroy(GameObject.Find("Card_" + XuanZeList[key].Value));       
        }
        RepositionCard();
    }
    public  void ButtonClick(GameObject go)
    {
        
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
      
        Room room = Global.NetMgr.SFS.LastJoinedRoom;
        Global.SFS.Send(new ExtensionRequest("GameExtends", obj, room));
        print("发送GameExtends");
        print(obj.ToJson());
    }
    /// <summary>
    /// 对牌重新排位置
    /// </summary>
    public void RepositionCard()
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

    private void OnCardSelectHandler(bool obj)
    {
        print(obj);
    }

    void Update()
    {
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

       
    }

}

