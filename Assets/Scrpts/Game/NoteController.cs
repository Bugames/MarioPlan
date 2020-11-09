using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NoteController : MonoBehaviour {

	/// <summary>
	/// 音符类型
	/// </summary>
	public enum NoteType
    {
		None,				//无
		TurtleShellRed,		//红龟壳
		TurtleShellGreen,	//绿龟壳
		Shell,				//炮弹
		Bomb,				//炸弹
		StarHead,			//星星头
		StarBody,			//星星身体
		StarTail			//星星尾
    }

	/// <summary>
	/// 音符地图
	/// </summary>
	public struct NoteMap
    {
		public List<NoteType> two;
		public List<NoteType> one;
    }

	#region public Member
	/// <summary>
	/// 歌曲文件名
	/// </summary>
	public string songFileName;
	/// <summary>
	/// 当前歌曲的bpm
	/// </summary>
	public float bpm=60.0f;
	/// <summary>
	/// 当前音符地图
	/// </summary>
	public NoteMap currNoteMap;
	#endregion

	#region 单例实现
	public static NoteController Instance 
	{
		get
		{
			if(m_Instance!=null)
			{
				return m_Instance;
			}
			NoteController instance = FindObjectOfType<NoteController>();

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
	private static NoteController m_Instance;
	#endregion

	private void Awake()
	{
		if(m_Instance!=null)
		{
			Destroy(gameObject);
			return;
		}
		m_Instance = this;
		currNoteMap = new NoteMap();
		currNoteMap.two = new List<NoteType>();
		currNoteMap.one = new List<NoteType>();
	}

    private void Start()
    {
		UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
		//songFileName = SystemController.Instance.GetSongName();
		//string filepath = "Song/" + songFileName;
		//SetInitNoteMap(filepath);
		SetInitNoteMap("Song/First");
	}

    #region public Method
	/// <summary>
	/// 设置初始化音符地图
	/// </summary>
	/// <param name="fillename"></param>
    public void SetInitNoteMap(string fillename)
    {
		TextAsset text = Resources.Load<TextAsset>(fillename);
		string[] lines = Regex.Split(text.text, "\r\n", RegexOptions.IgnoreCase);
		Debug.Log(lines[0].Length);
		Debug.Log(lines[1].Length);
        for (int i = 0; i < lines[0].Length; i++)
        {
			if(!lines[0][i].Equals(' '))
            {
				currNoteMap.two.Add(GetType(lines[0][i]));
				currNoteMap.one.Add(GetType(lines[1][i]));
			}
		}
		bpm = Convert.ToInt32(lines[2]);
	}


	#endregion

	#region private Method
	/// <summary>
	/// 根据字符返回音符类型	
	/// </summary>
	/// <param name="num"></param>
	/// <returns>音符类型</returns>
	private NoteType GetType(char num)
    {
		NoteType note = NoteType.None;
		switch (num)
        {
			case '0':
				note = NoteType.None;
				break;
			case '1':
				int i = UnityEngine.Random.Range(0, 2);
				if(i==0)
                {
					note = NoteType.TurtleShellRed;
				}
				else
                {
					note = NoteType.TurtleShellGreen;
                }
				break;
			case '2':
				note = NoteType.Shell;
				break;
			case '3':
				note = NoteType.Bomb;
				break;
			case '4':
				note = NoteType.StarHead;
				break;
			case '5':
				note = NoteType.StarBody;
				break;
			case '6':
				note = NoteType.StarTail;
				break;
		}
		return note;
    }
	#endregion

}