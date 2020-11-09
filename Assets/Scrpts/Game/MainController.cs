using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

	#region public Member
	/// <summary>
	/// 返回开始界面按钮
	/// </summary>
	public Button returnButton;
	/// <summary>
	/// 菜单选择蓝
	/// </summary>
	public Dropdown songDropDown;
	#endregion

	#region 单例实现
	public static MainController Instance 
	{
		get
		{
			if(m_Instance!=null)
			{
				return m_Instance;
			}
			MainController instance = FindObjectOfType<MainController>();

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
	private static MainController m_Instance;
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
		InitDrop();
		InitEvent();
	}
    #region public Method
	/// <summary>
	/// 初始化下拉框
	/// </summary>
	public void InitDrop()
    {
		foreach(var temp in SystemController.Instance.songDic)
		{
			Dropdown.OptionData optionData = new Dropdown.OptionData();
			optionData.text = temp.Value;
			songDropDown.options.Add(optionData);
		}
    }

	/// <summary>
	/// 初始化时间
	/// </summary>
	public void InitEvent()
    {
		returnButton.onClick.AddListener(delegate ()
		{
			SceneController.Instance.ReturnMenuScene();
		});

		songDropDown.onValueChanged.AddListener(delegate (int value)
		{
			EnterGameWithSong(value);
		});
    }
    #endregion

    #region private Method
	/// <summary>
	/// 进入
	/// </summary>
	/// <param name="index"></param>
	private void EnterGameWithSong(int index)
    {
		SceneController.Instance.EnterGameScene();
		SystemController.Instance.songIndex = index;
    }
    #endregion

}