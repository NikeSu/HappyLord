using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定义的资源类型
/// </summary>
public enum ResType
{
    UI, Role, Weapon
}
public class ResManager : MonoBehaviour
{
    //根据类型来缓存资源，类型==>(资源路径,预设)
    private Dictionary<ResType, Dictionary<string, Object>> mDic = new Dictionary<ResType, Dictionary<string, Object>>();
    public void Init()
    {
        ///根据枚举数量初始各类型
        mDic[ResType.UI] = new Dictionary<string, Object>();
        mDic[ResType.Role] = new Dictionary<string, Object>();
        mDic[ResType.Weapon] = new Dictionary<string, Object>();

        Global.Log("【ResManager】初始化成功!");
    }

    /// <summary>
    /// 通过资源类型和资源名称加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type">资源类型</param>
    /// <param name="resName">资源名称</param>
    /// <param name="isNew">是否返回资源实例</param>
    /// <param name="cache">是否缓存</param>
    /// <returns></returns>
    public T GetRes<T>(ResType type, string resName,bool isNew=true,bool cache = false) where T:Object
    {
        T prefab = null;
        ///如果之前没存储过该资源
        if (!mDic[type].ContainsKey(resName))
        {
            //加载此资源
            prefab = Resources.Load<T>(type + "/" + resName);
            //如果开启缓存 ，则会进行缓存 
            if (cache)
            {
                mDic[type].Add(resName, prefab);
            }
          
        }else
        {
            //从字典中获取到资源
            prefab = mDic[type][resName] as T;
         }
        return isNew ? Instantiate<T>(prefab) : prefab;
    }

}
