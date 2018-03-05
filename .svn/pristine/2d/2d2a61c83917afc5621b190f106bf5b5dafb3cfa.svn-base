package com.ddz.zone;

import java.util.HashMap;
import java.util.List;

import com.ddz.Global;
import com.smartfoxserver.v2.core.ISFSEvent;
import com.smartfoxserver.v2.core.SFSEventParam;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.exceptions.SFSException;
import com.smartfoxserver.v2.extensions.BaseServerEventHandler;

public class SFSUserJoinZoneHandler extends BaseServerEventHandler {

	public void handleServerEvent(ISFSEvent arg0) throws SFSException {
		// TODO Auto-generated method stub

		trace("用户进入区");
		User u=(User)arg0.getParameter(SFSEventParam.USER);
		
		    String uname = u.getName();
		    
			SFSObject obj=new SFSObject();
			
		   obj.putUtfString("id", uname);
		   
		  
		   
		   this.send("login1", obj, u);
		
		  
		
	}

}
