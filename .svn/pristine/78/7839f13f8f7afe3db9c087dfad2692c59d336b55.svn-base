using Sfs2X.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X.Entities.Data;

public class NEvent  {

    /// <summary>
    /// 事件发送者
    /// </summary>
    private object target = null;

    /// <summary>
    /// 事件类型
    /// </summary>
	private string  type = null;

    /// <summary>
    /// 事件信息包括(Ext和Sys)
    /// </summary>
	private SFSObject data = null;

	/// <summary>
	/// 事件对象
	/// </summary>
	private BaseEvent baseEvt = null;


	public NEvent(object target,string type,SFSObject data,BaseEvent evt)
    {
		this.target = target;
        this.type = type;
        this.data = data;
		this.baseEvt = evt;
    }
	/// <summary>
	/// 事件触发对象
	/// </summary>
	/// <value>The target.</value>
	public object Target{
		get{ return this.target;}
	}
	/// <summary>
	/// 事件的类型
	/// </summary>
	/// <value>The type.</value>
	public string Type{
		get{ return this.type;}
	}
	/// <summary>
	/// 事件的数据
	/// </summary>
	/// <value>The data.</value>
	public SFSObject Data{
		get{ return this.data;}
	}
	/// <summary>
	/// Base事件对象
	/// </summary>
	/// <value>The evt.</value>
	public BaseEvent BaseEvt{
		get{ return this.baseEvt;}
	}

}
