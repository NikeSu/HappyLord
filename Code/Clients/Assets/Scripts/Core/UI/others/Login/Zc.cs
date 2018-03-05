using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zc : BaseUI {
    protected override void OnClickHandler(GameObject button)
    {
        base.OnClickHandler(button);
        switch (button.name)
        {
            case "btnRegister (1)":

                SceneManager.LoadScene("注册");


                break;

        }
    }
}

