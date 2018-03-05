using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI1G : BaseUI{

    public GameObject BeibaoGb;
    protected override void OnClickHandler(GameObject button)
    {
        base.OnClickHandler(button);


        switch (button.name)
        {
            case "btnBack":
                BeibaoGb.SetActive(false);
                break;

                // Use this for initialization

        }
    }
}
