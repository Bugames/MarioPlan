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
	/// <summary>
	/// 歌曲名
	/// </summary>
	public string[] songs;
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
		Init();
	}
    #region public Method
    public void Init()
    {
        for (int i = 0; i < songs.Length; i++)
        {
			songDic.Add(i + 1, songs[i]);
		}
    }
	/// <summary>
	/// 得到歌曲名
	/// </summary>
	/// <returns></returns>
	public string GetSongName()
    {
		if (songDic.Count == 0|| !songDic.ContainsKey(songIndex))
			return "";

		return songDic[songIndex];
    }
	#endregion

	#region private Method

	#endregion

}