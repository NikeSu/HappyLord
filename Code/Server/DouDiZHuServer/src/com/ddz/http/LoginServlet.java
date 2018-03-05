package com.ddz.http;

import java.io.IOException;
import java.util.HashMap;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.ddz.DBManager;
import com.smartfoxserver.v2.SmartFoxServer;
import com.smartfoxserver.v2.entities.Zone;
import com.smartfoxserver.v2.entities.data.SFSObject;

public class LoginServlet extends HttpServlet {

	//arg0   请求
	//arg1 回复
	@Override
	protected void service(HttpServletRequest arg0, HttpServletResponse arg1) throws ServletException, IOException {
		// TODO Auto-generated method stub
		arg1.setContentType("text/html;charset=utf-8");
		
		//创建一个SFSObject
				SFSObject outData=new SFSObject();
				
		//一、获取请求信息
		//获取客户端请求时，传递过来的username
		String username=arg0.getParameter("username");
		username=username==null?"":username;
		//获取客户端请求时，传递过来的password
		String password=arg0.getParameter("password");
		password=password==null?"":password;
		
		
		List<Zone> zones=SmartFoxServer.getInstance().getZoneManager().getZoneList();
		for (Zone zone : zones) {
			System.out.println(zone.getName()+""+zone.getUserCount());
		}
		
		System.out.println(username+","+password);
		
		if(username.equals("")||password.equals(""))
		{
			outData.putBool("success", false);
			outData.putUtfString("username", "");
			outData.putUtfString("info", "请输入正确账号密码");
		}
		
		//二、根据信息进行DB查询
		DBManager db= DBManager.GetInstance();
		//获取区然后获取区的DBManager对象
		Zone zone=SmartFoxServer.getInstance().getZoneManager().getZoneByName("DouDiZhu");
		//初始化数据库连接对象
		db.Init(zone.getDBManager());
		
		
		List<HashMap> users=db.doQuery("select * from user where username='"+username+"'");
		
		
		
		if(users.size()<=0)
		{
			outData.putBool("success", false);
			outData.putUtfString("username", username);
			outData.putUtfString("info", "此账号不存在！");
		}
		else{
			//获取查找到的记录
			HashMap user=users.get(0);
			
			if(user.get("username").equals(password))
			{
				//登录成功
				outData.putBool("success", true);
				outData.putUtfString("username", username);
				outData.putUtfString("info", "登录成功！");
			}
			else {
				//登录失败
				outData.putBool("success", false);
				outData.putUtfString("username", username);
				outData.putUtfString("info", "密码不存在！");
			}
		}
		
		
		
		//三、向客户端回复内结果
		//向请求用户回复信息
		arg1.getWriter().write(outData.toJson());
	}

}
