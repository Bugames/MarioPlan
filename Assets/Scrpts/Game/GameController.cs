using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

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
	/// <summary>
	/// 暂停按钮
	/// </summary>
	public Button pauseButton;
	/// <summary>
	/// 暂停菜单
	/// </summary>
	public GameObject pauseMenu;
	/// <summary>
	/// 借宿菜单
	/// </summary>
	public GameObject endMenu;
	/// <summary>
	/// 结束返回按钮
	/// </summary>
	public Button endReturnButton;
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
	/// <summary>
	/// 开始时间
	/// </summary>
	private float startTime = 3.0f;
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

	private void Start()
	{
		InitButtonEvent();
		currGameState = GameState.Ready;
	}

    private void Update()
    {
    }

    #region public Method
    /// <summary>
    /// 初始化按钮时间
    /// </summary>
    public void InitButtonEvent()
    {
		pauseButton.onClick.AddListener(delegate () 
		{
			pauseMenu.SetActive(true);
			Time.timeScale = 0.0f;
			currGameState = GameState.Pause;
		});

		pauseMenu.GetComponent<TwoSelect>().first.onClick.AddListener(delegate ()
		{
			pauseMenu.SetActive(false);
			Time.timeScale = 1.0f;
			currGameState = GameState.Playing;
		});

		pauseMenu.GetComponent<TwoSelect>().second.onClick.AddListener(delegate ()
		{
			SceneController.Instance.EnterMainWorldScene();
		});

		endReturnButton.onClick.AddListener(delegate ()
		{
			SceneController.Instance.EnterMainWorldScene();
		});
	}
	/// <summary>
	/// 触发结束界面
	/// </summary>
	public void EnterGameEnd()
    {
		endMenu.SetActive(true);
	}
	
	/// <summary>
	/// 等待三秒开始游戏
	/// </summary>
	/// <returns></returns>
	public IEnumerator ReadyForGame()
    {
		Time.timeScale = 0.0f;
		while(startTime>0)
        {
			startTime -= Time.deltaTime;
			//TODO:倒计时效果
			yield return 0;
		}
		Time.timeScale = 1.0f;
		currGameState = GameState.Playing;
    }
	#endregion

	#region private Method

	#endregion

}