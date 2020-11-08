using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	#region public Enum
	/// <summary>
	/// 游戏状态
	/// </summary>
	public enum GameState
    {
		Ready,
		Playing,
		Fever,
		Pause,
		End
    }
	#endregion

	#region public Member
	/// <summary>
	/// </summary>
	public GameState currGameState;
    #endregion

    #region 单例实现
    public static GameController Instance 
	{
		get
		{
			if(m_Instance!=null)
			{
				return m_Instance;
			}
			GameController instance = FindObjectOfType<GameController>();

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
	private static GameController m_Instance;
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
		currGameState = GameState.Ready;

    }
    #endregion

    #region private Method

    #endregion

}