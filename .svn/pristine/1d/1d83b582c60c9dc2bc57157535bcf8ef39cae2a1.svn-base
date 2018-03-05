using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Activity : BaseUI
{
    public GameObject Lijuan;
    public GameObject Niaochao;
    protected override void OnClickHandler(GameObject button)
    {
        base.OnClickHandler(button);
        switch (button.name)
        {
        
            case "btnXiang":
                Lijuan.SetActive(true);
                break;
            case "btnXiang1":
                Niaochao.SetActive(true);
                break;
            case "back":

                SceneManager.LoadScene("GameSelect");


                break;

        }
    }
}



        
