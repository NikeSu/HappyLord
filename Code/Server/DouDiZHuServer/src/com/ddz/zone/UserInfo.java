package com.ddz.zone;

import java.util.HashMap;
import java.util.List;

import com.ddz.DBManager;
import com.ddz.Global;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class UserInfo extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest(User arg0, ISFSObject arg1) {
		// TODO Auto-generated method stub

		trace("获取用户信息");
		String name=arg1.getUtfString("name");


		List<HashMap> results=DBManager.GetInstance().doQuery("select * from user where username='"+name+"'");

		if(results.size()>=1) {
		//获取第一个记录
		HashMap h=results.get(0);

		trace("!---------"+h+"-----------!");
		
		SFSObject obj=new SFSObject();
		obj.putUtfString("gold", h.get("gold").toString());
		obj.putUtfString("usernick", h.get("usernick").toString());
		obj.putUtfString("viplv",h.get("viplv").toString());
		obj.putUtfString("quan", h.get("quan").toString());
		obj.putUtfString("personalwords",h.get("personalwords").toString() );
		obj.putUtfString("gamelv", h.get("gamelv").toString());
		obj.putUtfString("sex", h.get("sex").toString());
		obj.putUtfString("game_count", h.get("game_count").toString());
		obj.putUtfString("honor_title", h.get("honor_title").toString());
		obj.putUtfString("Max_mult", h.get("Max_mult").toString());
		//发送
		this.send("GetUserInfo", obj, arg0);


		}else {
		trace("没此用户到结果");

		}



		trace(arg1.getUtfString("name"));

	}

}