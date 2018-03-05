package com.ddz.zone;

import com.ddz.Global;
import com.smartfoxserver.v2.core.SFSEventType;
import com.smartfoxserver.v2.extensions.SFSExtension;

public class ZoneExtension extends SFSExtension {

	public void init() {
		trace("==================斗地主服务器初始化==========================");
		Global.Init(this, getParentZone().getDBManager());
		//监听系统登录事件
		this.addEventHandler(SFSEventType.USER_LOGIN,SFSLoginHandler.class);
		this.addEventHandler(SFSEventType.USER_JOIN_ZONE, SFSUserJoinZoneHandler.class);
		
		//监听扩展
		//this.addRequestHandler("test", TestExt.class);
		
		this.addRequestHandler("GetUserInfo", UserInfo.class);//拿玩家个人信息
		
		this.addRequestHandler("ChangeUserInfo", ChangeInfo.class);//修改玩家个人信息
		
		this.addRequestHandler("GetEmail", GetEmailInfo.class);//邮件测试（未完成）
		
		this.addRequestHandler("GetCommodityInfo", CommodityInfoHandler.class);//拿到商品信息
		
		this.addRequestHandler("BackpackMessage", BackpackMessageHandler.class);//拿到用户背包信息
		
		try {
			//初始化扩展及数据库连接
			
			
			
			// TODO Auto-generated method stub
			trace("=======================================================");
		}catch(Exception e) {
			trace("服务器初始化失败"+e.getMessage());
		}
	}


}
