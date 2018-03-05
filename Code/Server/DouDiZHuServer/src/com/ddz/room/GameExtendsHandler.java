package com.ddz.room;

import java.util.List;

import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;


//用来处理对局
public class GameExtendsHandler extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest(User user, ISFSObject iobj) {
		// TODO Auto-generated method stub
		trace("GameExtends");
		
		
		//----------------------------------------
		Room room=user.getLastJoinedRoom();
		List users=room.getUserList();
		User TheNext=null;
		
		if(iobj.getUtfStringArray("cards")==null)
		{
			trace("cards=null");
			this.send("GameExt", new SFSObject(), user);
		
		}
		if(iobj.getUtfStringArray("cards")!=null)
		{
			SFSObject obj=new SFSObject();
			
			obj.putUtfStringArray("DCards", iobj.getUtfStringArray("cards"));
			trace("!---GameExt:"+ iobj.getUtfStringArray("cards")+"---!");
			this.send("GameExt", obj, user);
			this.send("CardsOnDesk", obj, users);
			trace("!-SendCardsOnDesk-!");
	
		}

		if(user==users.get(0))
		{
			TheNext=(User) users.get(1);
		}
		
		if(user==users.get(1))
		{
			TheNext=(User) users.get(2);
		}
		if(user==users.get(2))
		{
			TheNext=(User) users.get(0);
		}
		
		this.send("OnTurn", new SFSObject(), TheNext);
	}

}
