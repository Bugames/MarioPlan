using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameShow : MonoBehaviour {

	#region public Member
	/// <summary>
	/// Combo次数物体
	/// </summary>
	public GameObject comboGo;
	/// <summary>
	/// Combo消失时间
	/// </summary>
	public float outTime = 1.0f;
	/// <summary>
	/// 分数文本
	/// </summary>
	public Text gradeText;
	/// <summary>
	/// 当前成绩(显示)
	/// </summary>
	[HideInInspector]
	public uint currgrade { get; private set; } = 0;
	/// <summary>
	/// Combo文本
	/// </summary>
	public Text comboText;
	/// <summary>
	/// 当前Combocishu(显示)
	/// </summary>
	[HideInInspector]
	public int currComboCount { get; private set; } = 0;
	/// <summary>
	/// 血量进度跳
	/// </summary>
	public Slider bloodSlider;
	/// <summary>
	/// 当前血量
	/// </summary>
	[HideInInspector]
	public float blood = 0.0f;
	/// <summary>
	/// 最大血量
	/// </summary>
	public float maxBlood =250;
	/// <summary>
	/// 无双进度条
	/// </summary>
	public Slider feverSlider;
	/// <summary>
	/// 无双百分比
	/// </summary>
	public float feverper;
	/// <summary>
	/// 总分数
	/// </summary>
	public uint totalGrade;
	/// <summary>
	/// 总Combo此时
	/// </summary>
	public int totalCombo;
	#endregion

	#region private Member
	/// <summary>
	/// 间隔时间
	/// </summary>
	private float addTime;
    #endregion

    private void Start()
    {
		blood = maxBlood;
    }

    private void Update()
    {
		if(GameController.Instance.currGameState==GameController.GameState.Playing)
        {
			ShowComboCount(totalCombo);
			ShowGrade(totalGrade);
			ShowBlood(blood);
		}
		if(blood<=0)
        {
			GameController.Instance.currGameState = GameController.GameState.End;
        }
	}

#region public Method

#endregion

#region private Method
	/// <summary>
	/// 动态显示Combo次数
	/// </summary>
	/// <param name="count"></param>
	private void ShowComboCount(int count)
	{
		if (count == 0)
		{
			if (outTime > 0)
			{
				outTime -= Time.deltaTime;
			}
			else
			{
				comboGo.SetActive(false);
				outTime = 1.0f;
			}
		}
		else
		{
			outTime = 1.0f;
			comboGo.SetActive(true);
			if (addTime < 0.05f)
			{
				addTime += Time.deltaTime;
			}
			else
			{
				if (count > currComboCount)
				{
					currComboCount++;
					comboText.text = currComboCount.ToString();
					addTime = 0.0f;
					if (count - currComboCount > 10)
					{
						currComboCount += 10;
					}
					//动画效果
					comboText.GetComponent<Animator>().Play("TextScale", 0, 0);
				}
			}
		}
	}
	/// <summary>
	/// 显示分数
	/// </summary>
	/// <param name="grade"></param>
	private void ShowGrade(uint addgrade)
	{

		if (addTime < 0.05f)
		{
			addTime += Time.deltaTime;
		}
		else
		{
			if (addgrade - currgrade > 0)
			{
				currgrade += 10;
				gradeText.text = currgrade.ToString();
                addTime = 0.0f;
				if (addgrade - currgrade > 250)
				{
					currgrade += 250;
				}
			}
		}

	}
	/// <summary>
	/// 显示血量
	/// </summary>
	/// <param name="blood"></param>
	private void ShowBlood(float blood)
    {
		float bloodF = blood / maxBlood;
		bloodSlider.value = bloodF;
    }
#endregion

}