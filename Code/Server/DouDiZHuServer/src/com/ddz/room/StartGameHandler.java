package com.ddz.room;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;
import java.util.Random;

import com.ddz.DBManager;
import com.smartfoxserver.v2.core.SFSEventParam;
import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.entities.variables.SFSUserVariable;
import com.smartfoxserver.v2.exceptions.SFSVariableException;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

import scala.Int;

public class StartGameHandler extends BaseClientRequestHandler {

	
	@Override
	public void handleClientRequest(User user, ISFSObject iobj) {
		// TODO Auto-generated method stub
		
		trace("StartGameHandler");
		
		
		//trace("这副牌有"+cardlist.size()+"张牌");
		//SendPokers();
		
		Room room = user.getLastJoinedRoom();
		this.send("TheGameRoom", (ISFSObject) room, user);
		trace("STARTGAMEHANDLER_ROOM:"+room);
		
		List<User> users = room.getUserList();
		
		int index=0;
		for (User user2 : users) {
			SFSUserVariable suv=new SFSUserVariable("index", index);
			try {
				user2.setVariable(suv);
			} catch (SFSVariableException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
		SFSObject p1 =new SFSObject();
	    p1.putUtfStringArray("PlayersHandCard",InitCards.getInstance().player1Cards);
	
		SFSObject p2 =new SFSObject();
		p2.putUtfStringArray("PlayersHandCard",InitCards.getInstance().player2Cards);
		
		
		SFSObject p3 =new SFSObject();
		p3.putUtfStringArray("PlayersHandCard", InitCards.getInstance().player3Cards);
		
		SFSObject ExtCard=new SFSObject();
		ExtCard.putUtfStringArray("dipai", InitCards.getInstance().dipai);
		
		trace("STARTGAMEHANDLER_USER.GETNAME:"+user.getName());
		
		
		//告诉玩家自己相邻的玩家的昵称
		if(user==users.get(0))
		{
			
			p1.putUtfString("other1", GetOtherNick(users.get(1).getName()));
			p1.putUtfString("other2", GetOtherNick(users.get(2).getName()));
			this.send("GameStart", p1, user);
			
			
			trace("给玩家 1 发牌");
			trace("玩家1手牌"+InitCards.getInstance().player1Cards);
			this.send("ExtCardIs", ExtCard, user);
			
			this.send("CallLordTurn", new SFSObject(), user);
			
			/*
			SFSObject obj=new SFSObject();
			obj.putUtfStringArray("ExtCards", InitCards.getInstance().dipai);
			this.send("GetExtCards", obj, user);trace("GetExtCards");
			this.send("OnTurn", obj, user);trace("OnTurn");
			*/
			
		}
		if(user==users.get(1))
		{
			
			p2.putUtfString("other1",GetOtherNick(users.get(2).getName()));
			
			p2.putUtfString("other2",GetOtherNick(users.get(0).getName()));
			this.send("GameStart", p2, user);
			trace("给玩家 2 发牌");
			trace("玩家2手牌"+InitCards.getInstance().player2Cards);
			
			this.send("ExtCardIs", ExtCard, user);
		}
		if(user==users.get(2))
		{
			p3.putUtfString("other1",GetOtherNick(users.get(0).getName()));
			p3.putUtfString("other2",GetOtherNick(users.get(1).getName()));
			this.send("GameStart", p3, user);
			trace("玩家3手牌"+InitCards.getInstance().player3Cards);
			trace("给玩家 3 发牌");
			this.send("ExtCardIs", ExtCard, user);
		}
		
		/*
		//随机选择一个人，开始叫地主
		Random rnd=new Random();
        User u=users.get(rnd.nextInt(users.size()));
        
        this.send("qiangdizhu", new SFSObject(), u);	
        */
	}
	
	public String GetOtherNick(String othername)
	{
		List<HashMap> nick= DBManager.GetInstance().doQuery("select usernick from user where username = '"+ othername +"'");
		HashMap h = nick.get(0);
		String usernick = (String) h.get("usernick");
		return usernick;
	}
	
	public Int GetOtherGold(String othername)
	{
		List<HashMap> nick= DBManager.GetInstance().doQuery("select gold from user where username = '"+ othername +"'");
		HashMap h = nick.get(0);
		Int othergold=(Int) h.get("gold");
		return othergold;
	}
	
	public String GetOtherInfo(String cmd,String othername)
	{
		List<HashMap> nick= DBManager.GetInstance().doQuery("select * from user where username = '"+ othername +"'");
		HashMap h = nick.get(0);
		String otherinfo=(String) h.get(cmd);
		return otherinfo;
	}
}
