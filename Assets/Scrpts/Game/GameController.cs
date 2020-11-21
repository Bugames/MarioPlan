using System.Collections;
using System.Collections.Generic;
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
	/// 当前场景的Show
	/// </summary>
	public GameShow currGameShow;
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
		StartCoroutine("ReadyForGame");
	}

    private void Update()
    {
		if(currGameState==GameState.End)
        {
			EnterGameEnd();
		}
    }

    #region public Method
    /// <summary>
    /// 初始化按钮时间
    /// </summary>
    public void InitButtonEvent()
    {
		pauseButton.onClick.AddListener(delegate () 
		{
			PauseGame();
		});

		pauseMenu.GetComponent<TwoSelect>().first.onClick.AddListener(delegate ()
		{
			UnPauseGame();
		});

		pauseMenu.GetComponent<TwoSelect>().second.onClick.AddListener(delegate ()
		{
			Time.timeScale = 1.0f;
			SceneController.Instance.EnterMainWorldScene();
			MusicController.Instance.BGMGameObject.GetComponent<AudioSource>().Pause();
			MusicController.Instance.BGMGameObject.GetComponent<AudioSource>().clip = null;
		});

		endReturnButton.onClick.AddListener(delegate ()
		{
			SceneController.Instance.EnterMainWorldScene();
		});
	}
	/// <summary>
	/// 开始游戏
	/// </summary>
	public void StartGame()
    {
		string musicName = "Music/" + SystemController.Instance.GetSongName();
		AudioClip bgm = Resources.Load<AudioClip>(musicName);
		MusicController.Instance.BGMGameObject.GetComponent<AudioSource>().clip = bgm;
		PlayController.Instance.animator.enabled = true;
		PlayController.Instance.rigidbody2D.simulated = true;
		MusicController.Instance.BGMGameObject.GetComponent<AudioSource>().Play();
	}
	/// <summary>
	/// 游戏暂停
	/// </summary>
	public void PauseGame()
    {
		pauseMenu.SetActive(true);
		Time.timeScale = 0.0f;
		currGameState = GameState.Playing;
		MusicController.Instance.BGMGameObject.GetComponent<AudioSource>().Pause();
	}
	/// <summary>
	/// 取消游戏暂停
	/// </summary>
	public void UnPauseGame()
    {
		pauseMenu.SetActive(false);
		Time.timeScale = 1.0f;
		currGameState = GameState.Playing;
		MusicController.Instance.BGMGameObject.GetComponent<AudioSource>().Play();
	}
	/// <summary>
	/// 触发结束界面
	/// </summary>
	public void EnterGameEnd()
    {
		PlayController.Instance.rigidbody2D.simulated = false;
		endMenu.SetActive(true);
	}
	
	/// <summary>
	/// 等待三秒开始游戏
	/// </summary>
	/// <returns></returns>
	public IEnumerator ReadyForGame()
    {
		while (startTime > 0)
		{
			startTime -= Time.deltaTime;
			yield return 0;
		}
		currGameState = GameState.Playing;
		StartGame();
	}
	/// <summary>
	/// 更新GameShow
	/// </summary>
	/// <param name="score"></param>
	/// <param name="scoreCof"></param>
	/// <param name="noteType"></param>
	/// <param name="buckleBlood"></param>
	public void UpdateGameShow(uint score,float scoreCof, NoteController.NoteType noteType,NoteController.Performance per)
    {
		if (noteType != NoteController.NoteType.None && noteType != NoteController.NoteType.StarBody&&per!=NoteController.Performance.Miss)
			currGameShow.totalCombo += 1;

		currGameShow.totalGrade += NoteController.Instance.GetGrade(score, scoreCof, (uint)currGameShow.totalCombo, per);
    }
	/// <summary>
	/// 更新血量
	/// </summary>
	/// <param name="buckleBlood"></param>
	public void UpdateBlood(int buckleBlood)
    {
		currGameShow.blood -= buckleBlood;
	}
	#endregion

	#region private Method

	#endregion

}