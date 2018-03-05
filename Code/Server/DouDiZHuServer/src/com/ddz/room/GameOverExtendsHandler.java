package com.ddz.room;

import java.util.HashMap;
import java.util.List;

import com.ddz.DBManager;
import com.smartfoxserver.v2.entities.Room;
import com.smartfoxserver.v2.entities.User;
import com.smartfoxserver.v2.entities.data.ISFSObject;
import com.smartfoxserver.v2.entities.data.SFSObject;
import com.smartfoxserver.v2.entities.variables.UserVariable;
import com.smartfoxserver.v2.extensions.BaseClientRequestHandler;

public class GameOverExtendsHandler extends BaseClientRequestHandler {

	@Override
	public void handleClientRequest(User user, ISFSObject iobj) {
		// TODO Auto-generated method stub

		Room room = user.getLastJoinedRoom();
		
		List<User> users = room.getUserList();
		
		SFSObject gobj=new SFSObject();
		
		gobj.putInt("this_multi", InitCards.getInstance().MultiIndex);
		gobj.putUtfString("WinnerName", user.getName());
	
		UserVariable lo = user.getVariable("Lord?");
		if(lo!=null)
		{
			gobj.putUtfString("WhoWin", "地主获得胜利");
			trace("地主获得胜利");
			
			for(int i=0;i<users.size();i++)
			{
				trace("!---------------------");
				int gold = InitCards.getInstance().MultiIndex*100;
				String name = users.get(i).getName();
				if(users.get(i).getName()==user.getName())
				{
					trace(users.get(i).getName()+"金币加"+InitCards.getInstance().MultiIndex*100);
					UpdateUserGold(gold,name,true);
					//trace("现在用户"+users.get(i)+"的金币数量应为" + newgold);
				}
				else {
					trace(users.get(i).getName()+"金币 负"+InitCards.getInstance().MultiIndex*100);
					UpdateUserGold(gold,name,false);
				}
				trace("---------------------!");
			}
			this.send("GameOver", gobj, users);
			
		}
		else {
			trace("农民获得胜利");
			gobj.putUtfString("WhoWin", "农民获得胜利");
			
			for(int i=0;i<users.size();i++)
			{
				int gold = InitCards.getInstance().MultiIndex*100;
				String name = users.get(i).getName();
				trace("!---------------------");
				if(users.get(i).getVariable("Lord?")==null)
				{
					trace(users.get(i).getName()+"金币加"+InitCards.getInstance().MultiIndex*100);
					UpdateUserGold(gold,name,true);
				}
				if(users.get(i).getVariable("Lord?")!=null) {
					trace(users.get(i).getName()+"金币 负"+InitCards.getInstance().MultiIndex*100);
					UpdateUserGold(gold,name,false);
				}
				trace("---------------------!");
			}
			this.send("GameOver", gobj, users);
		}
		
		InitCards.getInstance().MultiIndex = 3;
	}
	/**
	 * 执行更改用户金币的SQL语句
	 * @param gold 应该增/减的金币数量
	 * @param name 用户的账户名
	 * @param won  是否为赢家
	 */
	public void UpdateUserGold(int gold,String name,boolean won)
	{
		trace("Won:"+won);
		List<HashMap> og = DBManager.GetInstance().doQuery("select gold from user where username ='"+name+"'");
		HashMap h=og.get(0);
		//-------------------
		trace("从数据库拿到"+name+"用户目前的金币数量为"+h.get("gold"));
		//-------------------
		String hg = h.get("gold").toString();
		int ig=Integer.valueOf(hg);
		int overcount=InitCards.getInstance().MultiIndex*100;
		
		int newgold;
		if(won==true)
		{
			newgold=ig + overcount;	
			DBManager.GetInstance().ExecuteSQL("update user set gold ="+ newgold +" where username='"+ name +"'", new Object());
			trace("增加用户"+name+"金币数量成功:"+newgold);
		}
		if(won==false)
		{
			newgold=ig-overcount;
			DBManager.GetInstance().ExecuteSQL("update user set gold ="+ newgold +" where username='"+ name +"'", new Object());
			trace("减少用户"+name+"金币数量成功:"+newgold);
		}
		
	}
}

