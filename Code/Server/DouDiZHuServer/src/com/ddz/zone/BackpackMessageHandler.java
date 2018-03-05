package com.ddz.zone;

import java.util.HashMap;
import java.util.List;

import com.ddz.DBManager;
import com.ddz.Global;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class BackpackMessageHandler extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest(User arg0, ISFSObject arg1) {
		// TODO Auto-generated method stub

		String name = arg0.getName();
		
		trace(name+"的背包信息");
		
		List<HashMap> result=DBManager.GetInstance().doQuery("select gamelv from user where username='"+name+"'");
		
		trace(result);
		
		HashMap h=result.get(0);
		
		SFSObject obj=new SFSObject();
		
		obj.putUtfString("gamelv", h.get("gamelv").toString());
		
		trace(h.get("gamelv").toString());
		
		this.send("BackpackMessage", obj, arg0);
	}

}
