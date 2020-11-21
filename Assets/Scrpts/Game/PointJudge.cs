using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PointJudge : MonoBehaviour {

    /// <summary>
    /// 中心源碰撞盒
    /// </summary>
    public CircleCollider2D cencircle;
    /// <summary>
    /// 点的颜色
    /// </summary>
    public string color;
    /// <summary>
    /// 是否能击打星星(第一行)
    /// </summary>
    private bool downCanStar = false;
    /// <summary>
    ///  是否能击打星星(第二行）
    /// </summary>
    private bool upCanStar = false;
    /// <summary>
    /// 是否能击打星星(中间）
    /// </summary>
    private bool midCanStar = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(111111);
        if(collision.gameObject.CompareTag("Power"))
        {
            if(PlayController.Instance.hang==PlayController.Hang.Mid)
            {
                if (PlayController.Instance.isQuickAttack == true)
                {
                    if (!IsStar(collision.GetComponent<PowerUp>().noteType))
                    {
                        PowerUp up = collision.GetComponent<PowerUp>();
                        float r2 = collision.bounds.size.x * 0.5f;
                        NoteController.Performance per = GetPer(collision.transform.position, r2);
                        Debug.Log(per);
                        GameController.Instance.UpdateGameShow(up.score, up.scoreCof, up.noteType, per);
                        Destroy(collision.gameObject);
                    }
                }
                else if (PlayController.Instance.isAttack == true)
                {
                    if (collision.GetComponent<PowerUp>().noteType == NoteController.NoteType.StarHead)
                    {
                        upCanStar = true;
                        Destroy(collision.gameObject);
                    }
                    else if (upCanStar == true &&
                        (collision.GetComponent<PowerUp>().noteType == NoteController.NoteType.StarBody || collision.GetComponent<PowerUp>().noteType == NoteController.NoteType.StarTail))
                    {
                        PowerUp up = collision.GetComponent<PowerUp>();
                        GameController.Instance.UpdateGameShow(up.score, up.scoreCof, up.noteType, NoteController.Performance.Perfect);
                        Destroy(collision.gameObject);
                    }
                }
                else
                {
                    upCanStar = false;
                    GameController.Instance.currGameShow.totalCombo = 0;
                }
            }
            else if(PlayController.Instance.hang == PlayController.Hang.Down&& color.Equals("Blue"))
            {
                if (PlayController.Instance.isQuickAttack == true)
                {
                    if (!IsStar(collision.GetComponent<PowerUp>().noteType))
                    {
                        Debug.Log("下轻按");
                        PowerUp up = collision.GetComponent<PowerUp>();
                        float r2 = collision.bounds.size.x * 0.5f;
                        NoteController.Performance per = GetPer(collision.transform.position, r2);
                        Debug.Log(per);
                        GameController.Instance.UpdateGameShow(up.score,up.scoreCof,up.noteType,per);
                        Destroy(collision.gameObject);
                    }
                }
                else if (PlayController.Instance.isAttack == true)
                {
                    Debug.Log(downCanStar);
                    if (collision.GetComponent<PowerUp>().noteType == NoteController.NoteType.StarHead)
                    {
                        downCanStar = true;
                        Destroy(collision.gameObject);
                    }
                    else if(downCanStar==true &&
                        (collision.GetComponent<PowerUp>().noteType == NoteController.NoteType.StarBody|| collision.GetComponent<PowerUp>().noteType == NoteController.NoteType.StarTail))
                    {
                        PowerUp up = collision.GetComponent<PowerUp>();
                        GameController.Instance.UpdateGameShow(up.score, up.scoreCof, up.noteType, NoteController.Performance.Perfect);
                        Destroy(collision.gameObject);
                    }
                }
                else
                {
                    downCanStar = false;
                    GameController.Instance.currGameShow.totalCombo = 0;
                }
            }
            else if (PlayController.Instance.hang == PlayController.Hang.Up && color.Equals("Red"))
            {
                if (PlayController.Instance.isQuickAttack == true)
                {
                    if (!IsStar(collision.GetComponent<PowerUp>().noteType))
                    {
                        PowerUp up = collision.GetComponent<PowerUp>();
                        float r2 = collision.bounds.size.x * 0.5f;
                        NoteController.Performance per = GetPer(collision.transform.position, r2);
                        Debug.Log(per);
                        GameController.Instance.UpdateGameShow(up.score, up.scoreCof, up.noteType, per);
                        Destroy(collision.gameObject);
                    }
                }
                else if (PlayController.Instance.isAttack == true)
                {
                    if (collision.GetComponent<PowerUp>().noteType == NoteController.NoteType.StarHead)
                    {
                        upCanStar = true;
                        Destroy(collision.gameObject);
                    }
                    else if (upCanStar == true &&
                        (collision.GetComponent<PowerUp>().noteType == NoteController.NoteType.StarBody || collision.GetComponent<PowerUp>().noteType == NoteController.NoteType.StarTail))
                    {
                        PowerUp up = collision.GetComponent<PowerUp>();
                        GameController.Instance.UpdateGameShow(up.score, up.scoreCof, up.noteType, NoteController.Performance.Perfect);
                        Destroy(collision.gameObject);
                    }
                }
                else
                {
                    upCanStar = false;
                    GameController.Instance.currGameShow.totalCombo = 0;
                }
            }
        }
    }

    public bool IsStar(NoteController.NoteType noteType)
    {
        if (noteType == NoteController.NoteType.StarHead || noteType == NoteController.NoteType.StarBody || noteType == NoteController.NoteType.StarTail)
            return true;
        return false;
    }

    public NoteController.Performance GetPer(Vector3 p2,float r2)
    {
        float distance = Vector3.Distance(p2, transform.position);
        float r1 = cencircle.radius * transform.lossyScale.x;
        if (distance < r1)
        {
            return NoteController.Performance.Perfect;
        }
        else if (distance < r1 + r2)
        {
            return NoteController.Performance.Great;
        }
        else if (distance > r1 + r2)
        {
            return NoteController.Performance.Miss;
        }
        return NoteController.Performance.Miss;
    }
}