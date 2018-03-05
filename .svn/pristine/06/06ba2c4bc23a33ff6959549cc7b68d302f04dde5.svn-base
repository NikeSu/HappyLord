package com.ddz.zone;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class GetEmailInfo extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest(User arg0, ISFSObject arg1) {
		// TODO Auto-generated method stub

		trace("收到邮箱拓展");
		
		SFSObject obj =new SFSObject();
		
		obj.putUtfString("title", "系统邮件");
		obj.putUtfString("time", "2018-01-05");
		obj.putUtfString("content", "测试邮箱拓展成功");
		
		this.send("GetEmail", obj, arg0);
		
		trace("返回客户端消息");
	}

}
