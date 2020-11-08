using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SystemController : MonoBehaviour {

	#region public Member
	#endregion

	#region 单例实现
	public static SystemController Instance 
	{
		get
		{
			if(m_Instance!=null)
			{
				return m_Instance;
			}
			SystemController instance = FindObjectOfType<SystemController>();

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
	private static SystemController m_Instance;
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
    private void Start()
    {
    }
    #endregion

    #region private Method

    #endregion

}