using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object=UnityEngine.Object;

public class ObjectPool<T>
{
    #region public members
    /// <summary>
    /// 对象池中的总数量
    /// </summary>
    public int Count { get { return gameobjectPool.Count; } }
    /// <summary>
    /// 对象池中已使用的对象的数量
    /// </summary>
    public int UsedItemCount { get {return  usedGameobjectPool.Count; } }
    #endregion

    #region private member
    /// <summary>
    /// 对象池
    /// </summary>
    private List<ObjectPoolContainer<T>> gameobjectPool;
    /// <summary>
    /// 已被使用的对象池(用于查询)
    /// </summary>
    private Dictionary<T, ObjectPoolContainer<T>> usedGameobjectPool;
    /// <summary>
    /// 用于生成一个对象的实例
    /// </summary>
    private Func<T> factoryFunc;
    /// <summary>
    /// 最后一个索引
    /// </summary>
    private int lastIndex=-1;
    #endregion
    /// <summary>
    /// 构造函数,构造一个对象池
    /// </summary>
    /// <param name="factoryfunc"></param>
    /// <param name="initialsize"></param>
    public ObjectPool(Func<T> factoryfunc,int initialsize)
    {
        factoryFunc = factoryfunc;
        initialsize = initialsize < 0 ? 0 : initialsize;
        gameobjectPool = new List<ObjectPoolContainer<T>>(initialsize);
        usedGameobjectPool = new Dictionary<T, ObjectPoolContainer<T>>(initialsize);

        InitObjects(initialsize);
    }
    
    #region public methods;
    /// <summary>
    /// 从对象池中获得一个对象,若对象池中全部被使用,则生成一个新的对象并加入到已使用对象池中
    /// </summary>
    /// <returns>返回一个对象</returns>
    public T Get()
    {
        ObjectPoolContainer<T> container = null;
        //从对象池列表中寻找未被使用的
        for (int i = 0; i < gameobjectPool.Count; i++)
        {
            lastIndex++;

            if(lastIndex>gameobjectPool.Count-1)
            {
                lastIndex = 0;
            }

            if (gameobjectPool[lastIndex].Used)
            {
                continue;
            }
            else
            {
                container = gameobjectPool[lastIndex];
                break;
            }
        }
        //若对象池中对象全被使用
        if(container==null)
        {
            container = CreateObjectContainer();
        }
      
        container.Consume();
       // gameobjectPool.Remove(container);
        usedGameobjectPool.Add(container.Item, container);
        return container.Item;
    }
    /// <summary>
    /// 释放一个对象
    /// </summary>
    /// <param name="item"></param>
    public void ReleaseItem(object item)
    {
        ReleaseItem((T)item);
    }
    /// <summary>
    /// 释放一个对象
    /// </summary>
    /// <param name="item"></param>
    public void ReleaseItem(T item)
    {
        if(usedGameobjectPool.ContainsKey(item))
        {
            var container = usedGameobjectPool[item];
            container.Release();
            usedGameobjectPool.Remove(item);
        }
    }
    #endregion

    #region private methods;
    /// <summary>
    /// 初始化对象
    /// </summary>
    /// <param name="initialsize"></param>
    private void InitObjects(int initialsize)
    {
        for (int i = 0; i < initialsize; i++)
        {
            CreateObjectContainer();
        }
    }
    /// <summary>
    /// 创建对象容器
    /// </summary>
    /// <returns></returns>
    private ObjectPoolContainer<T> CreateObjectContainer()
    {
        var container = new ObjectPoolContainer<T>
        {
            Item = factoryFunc()
        };
        gameobjectPool.Add(container);
        return container;
    }
    #endregion

}