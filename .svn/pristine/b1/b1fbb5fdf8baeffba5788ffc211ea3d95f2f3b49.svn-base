using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpPostRequest : MonoBehaviour {

    /// <summary>
    /// 请求结束后委托 
    /// </summary>
    /// <param name="result">请求后，服务器返回的内容</param>
    public delegate void Complete(string result);

    public event Complete OnComplete = null;

    private string url = "";
    /// <summary>
    /// 指定请求页面名称
    /// </summary>
    /// <param name="pagename">页面名称</param>
    public void SetURL(string path)
    {
        url = path;
    }

    //创建表单
    private WWWForm form;

    void Awake()
    {
        form = new WWWForm();
    }

    /// <summary>
    /// 向表单中添加字段
    /// </summary>
    /// <param name="key">字段名</param>
    /// <param name="value">字段值</param>
    public void PutString(string key, string value)
    {
        form.AddField(key, value);
    }

    public void PutInt(string key, int value)
    {
        form.AddField(key, value);
    }
    /// <summary>
    /// 开始进行http请求
    /// </summary>
    public void StartRequest()
    {
        StartCoroutine(doRequest());
    }

    private IEnumerator doRequest()
    {
        //进行请求，
        WWW www = new WWW(url, form);
    
        //得到服务器反馈
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            if (OnComplete != null)
            {
                OnComplete(www.text);
            }
        }
        else
        {
            print("Http请求出错" + www.error);
        }
        Destroy(this.gameObject, 5);
    }
 
}
