using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour {

	#region public Member
	/// <summary>
	/// 开始按钮
	/// </summary>
	public Button startButton;
	/// <summary>
	/// 设置按钮
	/// </summary>
	public Button setButton;
	/// <summary>
	/// 结束按钮
	/// </summary>
	public Button endButton;
	#endregion

	#region private Member

	#endregion

	private void Awake()
	{
		startButton.onClick.AddListener(delegate ()
		{
			SceneController.Instance.EnterMainWorldScene();
		});
		endButton.onClick.AddListener(delegate ()
		{
			SceneController.Instance.OnApplicationQuit();
		});
	}

	private void Start()
	{
		
	}

	private void Update()
	{
		
	}

	#region public Method
	
	#endregion

	#region private Method
	
	#endregion

}