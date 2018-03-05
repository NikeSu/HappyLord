using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZCC : BaseUI
{
    protected override void OnClickHandler(GameObject button)
    {
        base.OnClickHandler(button);
        switch (button.name)
        {
            case "btnRegister":

                SceneManager.LoadScene("Login");


                break;

        }
    }
}
