using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmailF : BaseUI
{
    protected override void OnClickHandler(GameObject button)
    {
        base.OnClickHandler(button);
        switch (button.name)
        {
            case "btnBack":

                SceneManager.LoadScene("GameSelect");


                break;

        }
    }
}
