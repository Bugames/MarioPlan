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
	/// <summary>
	/// 确定按钮
	/// </summary>
	public Button okButton;
	/// <summary>
	/// 开关音量
	/// </summary>
	public Toggle enAbleVolumeToggle;
	/// <summary>
	/// 音量进度条
	/// </summary>
	public Slider volumeSlider;
	/// <summary>
	/// 音量图标
	/// </summary>
	public Sprite[] volumeSprites;
	/// <summary>
	/// 设置物体
	/// </summary>
	public GameObject setting;
	#endregion

	#region private Member

	#endregion


	private void Start()
	{
		volumeSlider.value = MusicController.Instance.volume;
		enAbleVolumeToggle.isOn = MusicController.Instance.isMute;
		startButton.onClick.AddListener(delegate ()
		{
			SceneController.Instance.EnterMainWorldScene();
		});
		endButton.onClick.AddListener(delegate ()
		{
			SceneController.Instance.OnApplicationQuit();
		});
		setButton.onClick.AddListener(delegate ()
		{
			EnableSetting(true);
		});
		enAbleVolumeToggle.onValueChanged.AddListener(delegate (bool isenable) 
		{
			EnableVolume(isenable);
		});
		volumeSlider.onValueChanged.AddListener(delegate (float value)
		{
			MusicController.Instance.SetAllSound(value);
		});
		okButton.onClick.AddListener(delegate ()
		{
			EnableSetting(false);
		});
	}

	private void Update()
	{
		
	}

	#region public Method
	public void EnableVolume(bool enable)
    {
		if(enable)
        {
			MusicController.Instance.StopAllMusic();
			enAbleVolumeToggle.GetComponent<ChildSprite>().childSprite.sprite = volumeSprites[1];
		}
		else
        {
			MusicController.Instance.OpenAllMusic();
			enAbleVolumeToggle.GetComponent<ChildSprite>().childSprite.sprite = volumeSprites[0];
		}
	}
	#endregion

	#region private Method
	/// <summary>
	/// 是否开启设置
	/// </summary>
	/// <param name="isActive"></param>
	private void EnableSetting(bool isActive)
    {
		startButton.gameObject.SetActive(!isActive);
		endButton.gameObject.SetActive(!isActive);
		setButton.gameObject.SetActive(!isActive);
		setting.SetActive(isActive);
	}
	#endregion

}