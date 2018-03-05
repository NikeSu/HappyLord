using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour {
    //GetComponent<BoxCollider>().enabled = false;


    void Awake()
    {
        OnAwake();
        UIButton[] btns = this.transform.GetComponentsInChildren<UIButton>();
        foreach (var item in btns)
        {
            UIEventListener.Get(item.gameObject).onClick = OnClickHandler;
            
        }
    }

    protected virtual void OnClickHandler(GameObject button)
    {
        
    }
    protected virtual void OnAwake()
    {

    }

	void Start () {
        OnStart();

    }
    protected virtual void OnStart()
    {

    }
	
	void Update () {
        OnUpdate();
    }
    protected virtual void OnUpdate() { }
}
