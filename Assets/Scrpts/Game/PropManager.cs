using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoteType = NoteController.NoteType;
public class PropManager : MonoBehaviour {

	#region public Member
	/// <summary>
	/// 用来表示初始位置物体
	/// </summary>
	public GameObject initXGameObject;
	/// <summary>
	/// 道具间隔
	/// </summary>
	public float interval = 1.0f;
	/// <summary>
	/// 父物体
	/// </summary>
	public GameObject parent;
	/// <summary>
	/// 两个判定点
	/// </summary>
	public GameObject[] points;
	/// <summary>
	/// 道具初始位置x
	/// </summary>
	[HideInInspector]
	public float initPositionX;
	/// <summary>
	/// 音符控制实例
	/// </summary>
	[HideInInspector]
	public NoteController noteController;
	#endregion

	#region 单例实现
	public static PropManager Instance 
	{
		get
		{
			if(m_Instance!=null)
			{
				return m_Instance;
			}
			PropManager instance = FindObjectOfType<PropManager>();

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
	private static PropManager m_Instance;
	/// <summary>
	///音符字典
	/// </summary>
	private Dictionary<NoteType, GameObject> propDic;
	/// <summary>
	/// 道具Y位置
	/// </summary>
	private float[] initPositionY;
	/// <summary>
	/// 当前地图索引
	/// </summary>
	private int noteMapIndex = 0;
	/// <summary>
	/// 临时存储的对象列表
	/// </summary>
	private List<GameObject> tempList;
	#endregion

	private void Awake()
	{
		if(m_Instance!=null)
		{
			Destroy(gameObject);
			return;
		}
		m_Instance = this;
		propDic = new Dictionary<NoteType, GameObject>();
		tempList = new List<GameObject>();
	}

    private void Start()
    {
		noteController = NoteController.Instance;
		InitPropDic();
		initPositionX = initXGameObject.transform.position.x;
    }


	#region public Method
	/// <summary>
	/// 更新道具
	/// </summary>

	public void UpdateProp()
	{
		if(noteMapIndex==0)
        {
			initPositionY = new float[points.Length];
			for (int i = 0; i < points.Length; i++)
			{
				initPositionY[i] = points[i].transform.position.y;
			}
		}
		if(noteMapIndex < noteController.currNoteMap.two.Count)
        {
			NoteType type1 = noteController.currNoteMap.two[noteMapIndex];
			var perfeb1 = GetPropByType(type1);
			if (perfeb1 != null)
			{
				Vector3 position = new Vector3(initPositionX + noteMapIndex * interval, initPositionY[0], 0.0f);
				var go = Instantiate(perfeb1, position, Quaternion.identity, parent.transform);
				tempList.Add(go);
			}

			NoteType type2 = noteController.currNoteMap.one[noteMapIndex];
			var perfeb2 = GetPropByType(type2);
			if (perfeb2 != null)
			{
				Vector3 position = new Vector3(initPositionX + noteMapIndex * interval, initPositionY[1], 0.0f);
				var go = Instantiate(perfeb2, position, Quaternion.identity, parent.transform);
				tempList.Add(go);
			}
			noteMapIndex++;
		}
	}
	/// <summary>
	///初始化道具
	/// </summary>
	public void InitPropDic()
    {
		var bomb = Resources.Load<GameObject>("Prop/Bomb");
		propDic.Add(NoteType.Bomb, bomb);

		var greenT = Resources.Load<GameObject>("Prop/GreenT");
		propDic.Add(NoteType.TurtleShellGreen, greenT);

		var redT = Resources.Load<GameObject>("Prop/redT");
		propDic.Add(NoteType.TurtleShellRed, redT);

        var StarHead = Resources.Load<GameObject>("Prop/StarHead");
        propDic.Add(NoteType.StarHead, StarHead);

        var StarBody = Resources.Load<GameObject>("Prop/StarBody");
        propDic.Add(NoteType.StarBody, StarBody);

        var StarTail = Resources.Load<GameObject>("Prop/StarTail");
        propDic.Add(NoteType.StarTail, StarTail);
    }
	/// <summary>
	/// 根据音符类型返回预制体
	/// </summary>
	/// <param name="type"></param>
	/// <returns>预制体</returns>
	public GameObject GetPropByType(NoteType type)
    {
		if (propDic.ContainsKey(type))
			return propDic[type];
		return null;
    }
    #endregion

    #region private Method

    #endregion

}