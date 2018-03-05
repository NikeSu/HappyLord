using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeaderType
{
    pz_head_man, pz_head_man1, pz_head_man2, pz_head_woman, pz_head_woman1, pz_head_woman2
}
public class PlayerHeader : MonoBehaviour
{
    public UISprite vipSprite;
    public UISprite dizhuSprite;
    public UISprite touxiangSprite;
    public UILabel coinLabel;
    public UILabel PlayerNameLabel;



    /// <summary>
    /// 是否VIP
    /// </summary>
    /// 
    void Start()
    {
        VIP = true;
        DiZhu = true;
        Header = HeaderType.pz_head_woman;
        PlayerName = "陆志明";
        Coin = 999999;
    }
    private bool _vip = false;
    public bool VIP
    {
        set
        {
            _vip = value;
            if (_vip)
            {
                vipSprite.spriteName = "pic_vipicon";
                vipSprite.gameObject.SetActive(true);
            }
            else
            {
                vipSprite.gameObject.SetActive(false);
            }

            }
        get
        {
            return _vip;
        }
    }

    private bool _dizhu = false;
    public bool DiZhu
    {
        set
        {
            _dizhu = value;
            if (_dizhu)
            {
                dizhuSprite.spriteName = "pz_Landlord";

            }
            else
            {
          dizhuSprite.spriteName = "pz_Peasant";
            }
        }
        get {
            return _dizhu;
        }
    }

    private int _Coin;
    public int Coin {
        set
        {
            _Coin = value;
            coinLabel.text = _Coin.ToString();
        }
        get
        {
            return _Coin;
        }
    }



    private  HeaderType _icon= HeaderType.pz_head_man1;
    public HeaderType Header
    {
        set
        {
            _icon = value;
            touxiangSprite.spriteName = _icon.ToString();
        } 
        get
        {
            return _icon;
        }
    }
    private string _PlayerName;
    public string PlayerName
    {

        set
        {
            _PlayerName = value;
            PlayerNameLabel.text = _PlayerName.ToString();





        }

        get
        {
            return _PlayerName;

        }

    }
}
