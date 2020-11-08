using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用于Unity中GameObject的池子
/// </summary>
public class PoolManager : MonoBehaviour {

	#region public Member
	/// <summary>
	/// 是否输出日志
	/// </summary>
	public bool isShowLog;
	/// <summary>
	/// 父物体
	/// </summary>
	public Transform root;
	#endregion

	#region 单例实现
	public static PoolManager Instance 
	{
		get
		{
			if(m_Instance!=null)
			{
				return m_Instance;
			}
			PoolManager instance = FindObjectOfType<PoolManager>();

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
	private static PoolManager m_Instance;
	/// <summary>
	/// 预制体对象池表
	/// </summary>
	private Dictionary<GameObject, ObjectPool<GameObject>> prefabLookup;
	/// <summary>
	/// 实例对象池表
	/// </summary>
	private Dictionary<GameObject, ObjectPool<GameObject>> instanceLookup;
	/// <summary>
	/// 是否刷新
	/// </summary>
	private bool hasRefreshedLog = false;
	#endregion

	private void Awake()
	{
		if(m_Instance!=null)
		{
			Destroy(gameObject);
			return;
		}
		m_Instance = this;

		prefabLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
		instanceLookup = new Dictionary<GameObject, ObjectPool<GameObject>>();
	}
	private void Update()
	{
		if(hasRefreshedLog&& isShowLog)
		{
			ShowLog();
			hasRefreshedLog = false;
		}
	}

	#region public Method
	/// <summary>
	/// 预热对象池
	/// </summary>
	/// <param name="prefab"></param>
	/// <param name="size"></param>
	/// <param name="isActive"></param>
	public void WarmPoolNonStatic(GameObject prefab, int size, bool isActive)
	{
		var pool = new ObjectPool<GameObject>(() =>{return InstantiatePrefab(prefab, isActive); },size);

		prefabLookup.Add(prefab, pool);
		hasRefreshedLog = true;
	}
	/// <summary>
	/// 获得对象池中一个对象
	/// </summary>
	/// <param name="prefab"></param>
	/// <param name="position"></param>
	/// <param name="rotation"></param>
	/// <returns></returns>
	public GameObject CreateObjectNonStatic(GameObject prefab,Vector3 position,Quaternion rotation)
	{
		if(!prefabLookup.ContainsKey(prefab))
		{
			//创建一个新的对象池
			WarmPool(prefab, 1, true);
		}
		
		var pool = prefabLookup[prefab];
		var go = pool.Get();
		go.transform.position = position;
		go.transform.rotation = rotation;
		go.SetActive(true);
		
		instanceLookup.Add(go, pool);
		hasRefreshedLog = true;
		return go;
	}
	/// <summary>
	/// 根据初始位置(零)创建一个实例对象池
	/// </summary>
	/// <param name="prefab"></param>
	/// <returns></returns>
	public GameObject CreateObjectNonStatic(GameObject prefab)
	{
		return CreateObjectNonStatic(prefab, Vector3.zero, Quaternion.identity);
	}
	/// <summary>
	/// 释放实例对象池表中的某个对象池
	/// </summary>
	/// <param name="gameObject"></param>
	public void ReleaseObjectNonStatic(GameObject gameObject)
	{
		gameObject.SetActive(false);
		if(instanceLookup.ContainsKey(gameObject))
		{
			instanceLookup[gameObject].ReleaseItem(gameObject);
			instanceLookup.Remove(gameObject);
			hasRefreshedLog = true;
		}
	}
	/// <summary>
	/// 显示日志
	/// </summary>
	public void ShowLog()
	{
		foreach(var item in prefabLookup)
		{
			Debug.Log(string.Format("“游戏对象池” 预置体名称:{0} {1}个在被使用,共有{2}个",item.Key.name,item.Value.UsedItemCount,item.Value.Count));
		}
	}
	#endregion

	#region private Method
	/// <summary>
	/// 实例化一个预制体(源对象)
	/// </summary>
	/// <param name="prefab"></param>
	/// <param name="isActive"></param>
	/// <returns>返回一个预制体对象</returns>
	private GameObject InstantiatePrefab(GameObject prefab, bool isActive)
	{
		var go = Instantiate(prefab) as GameObject;
		go.SetActive(isActive);

		if (root != null)
			go.transform.parent = root;
		return go;
	}
	#endregion

	#region 静态函数
	/// <summary>
	/// 预热对象池(静态函数)
	/// </summary>
	/// <param name="prefab"></param>
	/// <param name="size"></param>
	/// <param name="isActive"></param>
	public static void WarmPool(GameObject prefab,int size,bool isActive=false)
	{
		Instance.WarmPoolNonStatic(prefab, size, isActive);
	}
	/// <summary>
	/// 创建一个实例对象池,并加入实例对象池表中(静态函数)
	/// </summary>
	/// <param name="prefab"></param>
	/// <param name="position"></param>
	/// <param name="rotation"></param>
	/// <returns></returns>
	public static GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		return Instance.CreateObjectNonStatic(prefab, position, rotation);
	}
	/// <summary>
	/// 根据默认位置(零)创建一个实例对象池
	/// </summary>
	/// <param name="prefab"></param>
	/// <returns></returns>
	public static GameObject CreateObject(GameObject prefab)
	{
		return Instance.CreateObjectNonStatic(prefab);
	}

	/// <summary>
	/// 释放实例对象池表中的某个对象池
	/// </summary>
	/// <param name="gameObject"></param>
	public static void ReleaseObject(GameObject gameObject)
	{
		Instance.ReleaseObjectNonStatic(gameObject);
	}
	#endregion
}