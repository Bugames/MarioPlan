using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoteType = NoteController.NoteType;
public class PowerUp : MonoBehaviour {

	#region public Member
	/// <summary>
	/// 每个道具的得分
	/// </summary>
	public uint score;
	/// <summary>
	/// 得分系数
	/// </summary>
	public float scoreCof;
	/// <summary>
	/// 该道具的类型
	/// </summary>
	public NoteType noteType;
	/// <summary>
	/// 碰到该道具要扣掉的血量
	/// </summary>
	public int buckleBlood;
    #endregion

    #region private Member

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if(collision.gameObject.CompareTag("Player"))
        {
			GameController.Instance.UpdateBlood(buckleBlood);
		}
	}

    #region public Method

    #endregion

    #region private Method

    #endregion

}