package com.ddz.room;

import java.util.ArrayList;
import java.util.Collection;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class MaskInfoHandler extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest(User arg0, ISFSObject arg1) {
		// TODO Auto-generated method stub
		
		SFSObject obj=new SFSObject();
		
		obj.putUtfString("mask", "获取一场胜利");
		
		Collection<String> coll=new ArrayList<>();
		coll.add("mask_one");
		coll.add("连续赢得两场胜利");
		coll.add("GoodLuck!");
		
		obj.putUtfStringArray("mask1", coll);
		
		this.send("GetMaskInfo", obj, arg0);
		
		trace("发送任务到客户端成功");

	}

}
