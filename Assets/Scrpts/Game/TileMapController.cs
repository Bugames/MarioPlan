using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileMapController : MonoBehaviour {

	#region public Member
	/// <summary>
	/// 当前地图
	/// </summary>
	public Tilemap tilemap;
	/// <summary>
	/// 初始化长度
	/// </summary>
	public int initlength = 20;
	/// <summary>
	/// 天空物体
	/// </summary>
	public GameObject sky;
	/// <summary>
	/// 当前人物
	/// </summary>
	public GameObject role;
	/// <summary>
	/// 道具管理实例
	/// </summary>
	[HideInInspector]
	public PropManager propManager;
	bool isPlay=false;
	#endregion

	#region 单例实现
	public static TileMapController Instance 
	{
		get
		{
			if(m_Instance!=null)
			{
				return m_Instance;
			}
			TileMapController instance = FindObjectOfType<TileMapController>();

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
	private static TileMapController m_Instance;
	/// <summary>
	/// 地图种类
	/// </summary>
	private Dictionary<string, Tile> arrTiles;
	/// <summary>
	/// 上次的位置
	/// </summary>
	private Vector3 lastpostion0;
	/// <summary>
	/// 第一块瓦片长度
	/// </summary>
	private int firstLength = 0;
	/// <summary>
	/// 天空长度
	/// </summary>
	private float skyLength = 18;
	/// <summary>
	/// 当天天空位置x
	/// </summary>
	private float currSkyX = 0.0f;
	/// <summary>
	/// 用于天空更新次数
	/// </summary>
	private int updatecount = 0;
	/// <summary>
	/// 临时存储sky对象的队列
	/// </summary>
	private Queue<GameObject> skyQueue;
	/// <summary>
	/// 游戏控制实例
	/// </summary>
	private GameController gameController;
	/// <summary>
	/// 最大索引
	/// </summary>
	private int maxIndex;
	#endregion

	private void Awake()
	{
		if(m_Instance!=null)
		{
			Destroy(gameObject);
			return;
		}
		m_Instance = this;
		arrTiles = new Dictionary<string, Tile>();
		skyQueue = new Queue<GameObject>();
	}

    private void Start()
    {
		gameController = GameController.Instance;
		propManager = PropManager.Instance;
		InitTile();
		InitSky();
		lastpostion0 = role.transform.position;
		maxIndex = propManager.noteController.currNoteMap.two.Count;
	}

    private void Update()
    {

		if (gameController.currGameState == GameController.GameState.Playing)
		{
            if (propManager.initPositionX - role.transform.position.x > propManager.interval)
            {
                MusicController.Instance.BGMGameObject.GetComponent<AudioSource>().Play();
            }
            float maxPositionX = propManager.initPositionX + maxIndex * propManager.interval;
			if (role.transform.position.x - maxPositionX > 10.0f)
			{
				gameController.currGameState = GameController.GameState.End;
				return;
			}


			UpdataMap();
		}
	}

    #region public Method
	public void UpdataMap()
    {
		if (role.transform.position.x - lastpostion0.x > skyLength)
		{
			lastpostion0 = role.transform.position;
			for (int i = 0; i < skyLength; ++i)
			{
				UpdateInitTile();
			}
			for (int i = 0; i < skyLength * 2; i++)
			{
				propManager.UpdateProp();
			}
			if(updatecount==0)
            {
				updatecount++;
			}
			else
            {
				UpdateSky();
			}
		}
    }



    /// <summary>
    /// 初始化tile
    /// </summary>
    public void InitTile()
    {
		//创建类型
		AddTile("grass","Tile/Grass");
		AddTile("muddy","Tile/Muddy");

		//初始化Map
		for (int i = 0; i < initlength; ++i)
		{
			SetTile(i, 0, "grass");
			SetTile(i, -1, "muddy");
		}
	}
	/// <summary>
	/// 跟新tile
	/// </summary>
	public void UpdateInitTile()
    {
		SetTile(initlength, 0, "grass");
		SetTile(initlength++, -1, "muddy");
		SetTile(firstLength, 0, "");
		SetTile(firstLength++, -1, "");
	}
	/// <summary>
	/// 初始化天空
	/// </summary>
	public void InitSky()
    {
		PoolManager.WarmPool(sky, 3);
		for (int i = 0; i < 3; ++i)
		{
			currSkyX = i * skyLength;
			Vector3 position = new Vector3(currSkyX, sky.transform.position.y, 0.0f);
			var go = PoolManager.CreateObject(sky, position, Quaternion.identity);
			skyQueue.Enqueue(go);
		}
	}
	/// <summary>
	/// 更新天空
	/// </summary>
	public void UpdateSky()
    {
		var toDestroy = skyQueue.Dequeue();
		Destroy(toDestroy);
		currSkyX += skyLength;
		Vector3 position = new Vector3(currSkyX, sky.transform.position.y, 0.0f);
		var go = PoolManager.CreateObject(sky, position, Quaternion.identity);
		skyQueue.Enqueue(go);
	}


	/// <summary>
	/// 根据位置设置tile位置
	/// </summary>
	/// <param name="i"></param>
	/// <param name="j"></param>
	/// <param name="tileType"></param>
	public void SetTile(int i,int j,string tileType)
    {
		Vector3Int position = new Vector3Int(i,j, 0);
		if(arrTiles.ContainsKey(tileType))
        {
			tilemap.SetTile(position, arrTiles[tileType]);
		}
		else
        {
			tilemap.SetTile(position, null);
		}
    }
    #endregion

    #region private Method
	/// <summary>
	/// 添加tile
	/// </summary>
	/// <param name="tilename"></param>
	/// <param name="spritepath"></param>
	private void AddTile(string tilename,string spritepath)
    {
		Tile tmp = Resources.Load<Tile>(spritepath);
		arrTiles.Add(tilename, tmp);
    }
    #endregion

}