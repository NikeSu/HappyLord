using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xinxihuidiao : BaseUI
{

    public GameObject XinxiG;
   
    public GameObject Setting_PlaneG;

    protected override void OnClickHandler(GameObject button)
    {
        base.OnClickHandler(button);


        switch (button.name)
        {
            case "Btn_Close":
                XinxiG.SetActive(false);               
                break;
         
            //case "btnClose":
            //    Setting_PlaneG.SetActive(false);
            //    break;
        }
    }
}



