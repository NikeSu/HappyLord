package com.ddz.zone;

import java.util.List;

import com.ddz.room.InitCards;
import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.entities.variables.SFSUserVariable;
import com.smartfoxserver.v2.exceptions.SFSVariableException;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class TestExt extends BaseClientRequestHandler {
	
	public void handleClientRequest(User user, ISFSObject iobj) {
		// TODO Auto-generated method stub

		
		
		User TheLast = null;

		Room room = user.getLastJoinedRoom();
		
		List<User> users = room.getUserList();
		
		trace("抢地主拓展回调成功");
	
		int multi=InitCards.getInstance().MultiIndex;
		
		boolean YesOrNo=iobj.getBool("YON");
		
		int N_multi=multi*2;
		
		
		int index = 0;
		
		index=InitCards.getInstance().CallLordIndex;
		if(index<=3)
		{
			trace("Index="+index);
			trace("YesOrNo="+YesOrNo);
			if(YesOrNo==true)
			{
				TheLast = user;
				
				SFSObject obj=new SFSObject();
				obj.putInt("Multi", N_multi);
				this.send("test", obj, users);
				InitCards.getInstance().MultiIndex=N_multi;
				trace("有人抢地主搞事情，倍数*2，现在倍数为："+N_multi);
			}
			else if(YesOrNo==false)
			{
				SFSObject obj=new SFSObject();
				obj.putInt("Multi", multi);
				this.send("test", obj, users);
				
				trace("这个人没抢地主，现在倍数为："+multi);
			}
			
			
			index = InitCards.getInstance().CallLordIndex++;	
			if(index<2) {
				if(user==users.get(0))
				{
					this.send("CallLordTurn", new SFSObject(), users.get(1));
				}
				
				if(user==users.get(1))
				{
					this.send("CallLordTurn",new SFSObject(), users.get(2));
				}
				
				if(user==users.get(2))
				{
					this.send("CallLordTurn",new SFSObject(), users.get(0));
				}
			}
			
		}
	
		if(index==2)
		{
			trace("----!----");
			SFSObject obj=new SFSObject();
			obj.putUtfStringArray("ExtCards", InitCards.getInstance().dipai);
			obj.putBool("IsLand", true);
			this.send("GetExtCards", obj, TheLast);
			this.send("OnTurn", obj, TheLast);
			//this.send("ExtCardsSended", obj, users);
			InitCards.getInstance().CallLordIndex=0;
			
			trace(TheLast+"拿到地主");

			//给拿到地主的玩家赋一个变量
			SFSUserVariable uv = new SFSUserVariable("Lord?",true);
			try {
				TheLast.setVariable(uv);
			} catch (SFSVariableException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			SFSObject land=new SFSObject();
			String name = TheLast.getName();
			land.putUtfString("LandIs", name);
			this.send("WhoIsLand", land, users);
			
			
		}
		
		
		
		
		
		
		/*	
		if(TheLast==users.get(0))
		{
			SFSObject obj=new SFSObject();
			obj.putBool("Land", false);
			this.send("IsLand", obj, users.get(1));
			this.send("IsLand", obj, users.get(2));
		}
		
		if(TheLast==users.get(1))
		{
			SFSObject obj=new SFSObject();
			obj.putBool("Land", false);
			this.send("IsLand", obj, users.get(0));
			this.send("IsLand", obj, users.get(2));
		}
		
		if(TheLast==users.get(2))
		{
			SFSObject obj=new SFSObject();
			obj.putBool("Land", false);
			this.send("IsLand", obj, users.get(0));
			this.send("IsLand", obj, users.get(1));
		
		for (User tmpU : users) {
				
				SFSUserVariable uv;
				if(tmpU==TheLast) {
					
					uv=new SFSUserVariable("dizhu", true);
					
				}else {
					uv=new SFSUserVariable("dizhu", false);
				}
				
				try {
					user.setVariable(uv);
				} catch (SFSVariableException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				
				
			}*/
	}

}
