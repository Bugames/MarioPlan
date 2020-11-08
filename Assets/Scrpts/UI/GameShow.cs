using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameShow : MonoBehaviour {

	#region public Member
	/// <summary>
	/// 分数文本
	/// </summary>
	public Text gradeText;
	/// <summary>
	/// 当前成绩
	/// </summary>
	public uint currgrade = 0;
	/// <summary>
	/// Combo文本
	/// </summary>
	public Text comboText;
	/// <summary>
	/// 当前Combocishu
	/// </summary>
	public int currComboCount = 0;
	/// <summary>
	/// 血量进度跳
	/// </summary>
	public Slider bloodSlider;
	/// <summary>
	/// 当前血量
	/// </summary>
	[HideInInspector]
	public float blood;
	/// <summary>
	/// 最大血量
	/// </summary>
	public float maxBlood =250;
	/// <summary>
	/// 无双进度条
	/// </summary>
	public Slider feverSlider;
	/// <summary>
	/// 无双白分比
	/// </summary>
	public float feverper;
	#endregion

	#region private Member
	/// <summary>
	/// 间隔时间
	/// </summary>
	private float addTime;
	#endregion

	private void Awake()
	{
		
	}

	private void Start()
	{
		
	}

	private void Update()
	{
		
	}

	#region public Method
	/// <summary>
	/// 设置分数
	/// </summary>
	/// <param name="grade"></param>
	public void SetGrade(uint grade)
	{
		gradeText.text = grade.ToString();
	}
	/// <summary>
	/// 设置Combo次数
	/// </summary>
	/// <param name="count"></param>
	public void SetComboCount(int count)
	{
		comboText.text = count.ToString();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="count"></param>
	public void ShowComboCount(int count)
	{
		if (count == 0)
		{
			//TODO:

		}
		else
		{
			if (addTime < 0.05f)
			{
				addTime += Time.deltaTime;
			}
			else
			{
				if (currComboCount < count)
				{
					currComboCount++;
					comboText.text = currComboCount.ToString();
					addTime = 0.0f;
					if (count - currComboCount > 10)
					{
						currComboCount += 10;
					}
				}
			}
		}
	}
	/// <summary>
	/// 显示分数
	/// </summary>
	/// <param name="grade"></param>
	public void ShowGrade(int grade)
	{
		if (grade == 0)
		{
			//TODO:
			currComboCount = 0;
			comboText.text = currComboCount.ToString();
		}
		else
		{
			if (addTime < 0.05f)
			{
				addTime += Time.deltaTime;
			}
			else
			{
				if (grade > currgrade)
				{
					currgrade++;
					comboText.text = currComboCount.ToString();
					addTime = 0.0f;
					if (grade - currComboCount > 10)
					{
						currComboCount += 10;
					}
				}
			}
		}
	}
	#endregion

	#region private Method

	#endregion

}