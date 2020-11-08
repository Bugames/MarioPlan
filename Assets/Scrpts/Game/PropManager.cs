using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour {

	#region public Member
	
	#endregion

	#region 单例实现
	public static PropManager Instance 
	{
		get
		{
			if(m_Instance!=null)
			{
				return m_Instance;
			}
			PropManager instance = FindObjectOfType<PropManager>();

			if(instance!=null)
			{
				return instance;
			}
			return m_Instance;
		}
	}
	#endregion

	#region private Member
	/// <summary>
    /// 唯一实例
    /// </summary>
	private static PropManager m_Instance;
	#endregion

	private void Awake()
	{
		if(m_Instance!=null)
		{
			Destroy(gameObject);
			return;
		}
		m_Instance = this;
	}

	#region public Method
	
	#endregion

	#region private Method
	
	#endregion

}