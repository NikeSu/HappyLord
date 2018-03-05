using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerHeader  {

    bool VIP { get; set; }
    bool Dizhu { get; set; }
    string PlayerName { get; set; }
    int Coin { get; set; }

    HeaderType icon { set; get; }


}
