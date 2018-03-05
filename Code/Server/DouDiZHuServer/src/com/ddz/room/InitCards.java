package com.ddz.room;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class InitCards {
	
	//实现牌组单例
	private InitCards() {}
	private static InitCards cards=null;
	public static InitCards getInstance()
	{
		if(cards==null) {
			cards=new InitCards();
		}
		return cards;
	}
	//-------------------
	public List<String> cardlist =new ArrayList<>();
	public List<String> player1Cards;
	public List<String> player2Cards;
	public List<String> player3Cards;
	public List<String> dipai;
	
    public int CallLordIndex=0;
    
    public int MultiIndex=3;
   
    //创建本局游戏的牌
   	public void InitCardList()
   	{
   		System.out.println("创建本场游戏的牌");
   		cardlist =new ArrayList<>();
   	        for (int i = 0; i < 4; i++)
   	        {
   	            for (int j = 1; j <= 13; j++)
   	            {
   	                if (j == 10)
   	                {
   	                    cardlist.add(i + "A");
   	                }
   	                else if (j == 11)
   	                {
   	                    cardlist.add(i + "B");
   	                }
   	                else if (j == 12)
   	                {
   	                    cardlist.add(i + "C");
   	                }
   	                else if (j == 13)
   	                {
   	                    cardlist.add(i + "D");
   	                }
   	                else
   	                {
   	                    cardlist.add(i + "" + j);
   	                }

   	            }

   	        }
   	        cardlist.add("41");
   	        cardlist.add("51");
   	        
   	        
   	        
   	        ///利用位置转换来起到打乱牌的效果
   	        for (int i = 0; i < cardlist.size(); i++)
   	        {
   	            String v = cardlist.get(i);
   	            
   	            int rid = new Random().nextInt(54);
   	            cardlist.set(i,cardlist.get(rid));
   	            cardlist.set(rid, v);
   	        };
   	        
   	        System.out.println("这副牌有"+cardlist.size()+"张牌");
   	       /* for(String card : cardlist)
   	        {
   	        	//System.out.println(card);
   	        }
   	        */

   	        if(player1Cards==null||player2Cards==null||player3Cards==null)
   	        {
   	        	 //-------------------------------
   	        	System.out.println("开始分牌");
   		        player1Cards = new ArrayList<>();
   		 	    player2Cards = new ArrayList();
   		 	    player3Cards = new ArrayList();
   		 	    dipai = new ArrayList<>();
   		 	System.out.println("分配玩家1手牌");
   		        for(int i=0;i<=16;i++)
   		        {
   		        	
   		        	player1Cards.add(cardlist.get(i));
   		        }
   		     System.out.println("玩家1手牌"+ player1Cards);
   		        
   		     System.out.println("分配玩家2手牌");
   		        for(int i=17;i<=33;i++)
   		        {
   		        	player2Cards.add(cardlist.get(i));
   		        }
   		     System.out.println("玩家2手牌"+ player2Cards);
   		        
   		     System.out.println("分配玩家3手牌");
   		        for(int i=34;i<=50;i++)
   		        {
   		        	player3Cards.add(cardlist.get(i));
   		        }
   		     System.out.println("玩家3手牌"+ player3Cards);
   		        for(int i=51;i<=53;i++)
   		        {
   		        	dipai.add(cardlist.get(i));
   		        }
   		     System.out.println("保存底牌"+dipai.get(0)+","+dipai.get(1)+","+dipai.get(2));
   		       //------------------------------
   	        }       
	}

}
