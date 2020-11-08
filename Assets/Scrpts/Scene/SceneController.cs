using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 全局跳转控制
/// </summary>
public class SceneController : MonoBehaviour
{
	#region public Member
	/// <summary>
	/// 下一个场景的名字id
	/// </summary>
	public int nextSceneindex;
	/// <summary>
	/// 上一个场景的索引
	/// </summary>
	public static int lastSceneIndex = 0;
	#endregion

	#region 单例实现
	public static SceneController Instance
	{
		get
		{
			if (m_Instance != null)
			{
				return m_Instance;
			}
			SceneController instance = FindObjectOfType<SceneController>();

			if (instance != null)
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
	private static SceneController m_Instance;

	
	#endregion

	private void Awake()
	{
		if (m_Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		m_Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	#region public Method
	/// <summary>
	/// 程序退出
	/// </summary>
	public void OnApplicationQuit()
	{
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
	/// <summary>
	/// 开始进入加载场景
	/// </summary>
	public void StartLoadingScene()

	{
		SceneManager.LoadScene(1);
	}

	/// <summary>
	/// 开始进入加载场景
	/// </summary>
	public void StartLoadingScene(int nextsceneindex)
	{
		lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
		nextSceneindex = nextsceneindex;
		SceneManager.LoadScene(1);
	}


	public void EnterMainWorldScene()
	{
		SceneManager.LoadScene(2);
	}

	public void EnterGameScene()
	{
		SceneManager.LoadScene(3);
	}

	/// <summary>
	/// 返回开始界面
	/// </summary>
	public void ReturnMenuScene()
	{
		SceneManager.LoadScene(0);
	}

	/// <summary>
	/// 进入结束场景
	/// </summary>
	public void EnterEndScene()
	{
		SceneManager.LoadScene(4);
	}

	#endregion
	#region private Method

	#endregion

}