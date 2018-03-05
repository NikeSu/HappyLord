package com.ddz.zone;

import java.util.HashMap;
import java.util.List;

import com.ddz.DBManager;
import com.ddz.Global;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class ChangeInfo extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest(User arg0, ISFSObject arg1) {
		// TODO Auto-generated method stub

		trace("ChangeInfo");
		String name=arg1.getUtfString("name");
		String newnick=arg1.getUtfString("usernick");
		String newWords=arg1.getUtfString("personalwords");
		trace(name);
		trace(newnick);
		DBManager.GetInstance().ExecuteSQL("update user set usernick = ? where username = ?", newnick,name);
		DBManager.GetInstance().ExecuteSQL("update user set personalwords = ? where username = ?", newWords,name);
		
		List<HashMap> results=DBManager.GetInstance().doQuery("select usernick from user where username='"+name+"'");
		if(results.size()>=1) {
			//获取第一个记录
			HashMap h=results.get(0);
			
			SFSObject obj=new SFSObject();
			
			obj.putUtfString("usernick", h.get("usernick").toString());
			//obj.putUtfString("personalwords", h.get("personalwords").toString());
			
			this.send("ChangeUserInfo", obj, arg0);
		}
		else {
			trace("修改信息失败");
		}
	}

}
