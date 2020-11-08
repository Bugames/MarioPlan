using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolContainer<T>
{
    #region public members
    /// <summary>
    ///容器内对象
    /// </summary>
    public T Item { get; set; }
	/// <summary>
	///  容器内对象是否已被使用的标记
	/// </summary>
	public bool Used { get; private set; }
    #endregion

    #region public methods
    /// <summary>
    /// 标记已使用
    /// </summary>
    public void Consume()
    {
        Used = true;
    }
    /// <summary>
    /// 标记未被使用
    /// </summary>
    public void Release()
    {
        Used = false;
    }
    #endregion

}