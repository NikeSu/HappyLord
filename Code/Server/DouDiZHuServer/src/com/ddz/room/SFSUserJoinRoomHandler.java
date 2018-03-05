package com.ddz.room;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Random;
import java.util.Set;

import com.ddz.Global;
import com.smartfoxserver.v2.api.CreateRoomSettings;
import com.smartfoxserver.v2.core.ISFSEvent;
import com.smartfoxserver.v2.core.SFSEventParam;
import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.SFSRoomRemoveMode;
import com.smartfoxserver.v2.entities.SFSRoomSettings;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.Zone;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.exceptions.SFSException;
import com.smartfoxserver.v2.extensions.BaseServerEventHandler;
import com.smartfoxserver.v2.extensions.ISFSExtension;
/*
 *处理用户进入房间及判断房间是否满人 
 */
public class SFSUserJoinRoomHandler extends BaseServerEventHandler {

	private static CreateRoomSettings room = null;
	

	@Override
	public void handleServerEvent(ISFSEvent event) throws SFSException {
		// TODO Auto-generated method stub
		trace("用户进入房间");
		User u=(User)event.getParameter(SFSEventParam.USER);//拿到用户
        Room r = (Room)event.getParameter(SFSEventParam.ROOM);//拿到房间
        
        if(r.getName()=="lobby")
        {
        	trace("！---等待房间---！");
        }
        else
        {
        	trace("!------!");
        }
        r.setMaxUsers(3);
        List<User> list = r.getUserList();
        trace(list);
        int player_num = list.size();
	 	trace("!-----"+player_num+"-----!");
	 	if(player_num<=3)
	 	{
	 		SFSObject obj=new SFSObject();
			obj.putInt("Multi", 3);
			obj.putInt("Grade", 100);
			obj.putUtfString("MatchName", "欢乐——初级场");
			this.send("GetRoomMassage", obj, u);
			SFSObject wobj=new SFSObject();
			this.send("WaitForGame", wobj, u);
			trace("房间信息发送客户端成功,等待游戏开始");
			
	 	}
	 	if(player_num==3)
	 	{
	 			trace("房间名："+r.getName());
	 			Zone zone = this.getParentExtension().getParentZone();
				trace(zone);
				room=new CreateRoomSettings();
				room.setName(u.getName()+"room");
				//room.setGroupId("GameGroup1");
				room.isDynamic();
				room.setMaxUsers(3);
				trace(room);
				trace("---rm---");
				this.getApi().createRoom(zone, room, u);		
				Room rm=zone.getRoomByName(u.getName()+"room");
				ISFSExtension roomextension = this.getParentExtension();
				rm.setExtension(roomextension);
				SFSRoomRemoveMode arg0 = SFSRoomRemoveMode.WHEN_EMPTY_AND_CREATOR_IS_GONE;
				rm.setAutoRemoveMode(arg0);
				trace(rm);
				
				SFSObject obj=new SFSObject();
		 		this.send("RoomIsFull",obj,list);
		 		trace("游戏即将开始");
	 		
		 		InitCards.getInstance().InitCardList();//通过单例创建一副本场游戏的牌
		 		trace(InitCards.getInstance().dipai);		
	 	}
	 	
		
	
	}
}
