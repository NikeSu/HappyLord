package com.ddz.zone;

import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class CommodityInfoHandler extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest(User arg0, ISFSObject arg1) {
		// TODO Auto-generated method stub
		
		trace("返回客户端商品");
		
		SFSObject obj =new SFSObject();
		
		obj.putUtfString("coinSprite", "coin");
		obj.putInt("price", 2);
		obj.putInt("num", 1000);
		obj.putUtfString("cmd_name", "金豆");
		obj.putUtfString("gift", "送一天记牌器");
		
		this.send("GetCommodityInfo", obj, arg0);
	
	}

}
