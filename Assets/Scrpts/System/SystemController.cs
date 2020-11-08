using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
public class SystemController : MonoBehaviour {

	#region public Member
	/// <summary>
	/// 歌曲字典
	/// </summary>
	public Dictionary<int, string> songDic;
	/// <summary>
	/// 当前歌曲的索引
	/// </summary>
	public int songIndex = 0;
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
		DontDestroyOnLoad(this);
	}
	private void Start()
	{
		songDic = new Dictionary<int, string>();
	}
	#region public Method
	public void Init()
    {
		//读取歌曲信息
    }
	#endregion

	#region private Method

	#endregion

}