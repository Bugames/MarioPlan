using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour {

	#region public Member
	/// <summary>
	/// 按的类型
	/// </summary>
	public enum Press
    {
		None,
		Quick,
		Leng
    }
	/// <summary>
	/// 当前人物所在行数
	/// </summary>
	public enum Hang
    {
		Up,
		Mid,
		Down
    }

	/// <summary>
	/// 人物向前的速度
	/// </summary>
	public float forwordspeed;
	/// <summary>
	/// 人物向上的速度
	/// </summary>
	public float upSpeed;
	/// <summary>
	/// 当前人物动作
	/// </summary>
	[HideInInspector]
	public Animator animator;
	/// <summary>
	/// 当前人物的物理系统
	/// </summary>
	[HideInInspector]
	public new Rigidbody2D rigidbody2D;
	/// <summary>
	/// 是否长按
	/// </summary>
	public Press pressType;
	/// <summary>
	/// 是否快速攻击
	/// </summary>
	public bool isQuickAttack=false;
	/// <summary>
	///是否长攻击
	/// </summary>
	public bool isAttack = false;
	/// <summary>
	/// 当前所在行数
	/// </summary>
	public Hang hang;
	/// <summary>
	/// 是否是第一行
	/// </summary>
	public bool isOneHang = true;
	#endregion

	#region 单例实现
	public static PlayController Instance 
	{
		get
		{
			if(m_Instance!=null)
			{
				return m_Instance;
			}
			PlayController instance = FindObjectOfType<PlayController>();

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
	private static PlayController m_Instance;
	int count = 0;
	private float start;
	private float end;
	private new Camera camera;
	private float offset;
	private float lastX;
	private float screenWidth;
	private bool newTouch;
     #endregion

	private void Awake()
	{
		if(m_Instance!=null)
		{
			Destroy(gameObject);
			return;
		}
		m_Instance = this;
	}

#region public Method
    private void Start()
    {
		camera = Camera.main;
		lastX = transform.position.x;
		animator = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.simulated = false;

		screenWidth = Screen.width;
	}

    private void Update()
    {
		isQuickAttack = false;
		isAttack = false;
		animator.SetBool("QuickAttack", isQuickAttack);
		animator.SetBool("Attack", isAttack);

		upSpeed = -3.0f;
		isOneHang = true;
		pressType = Press.None;
		hang = Hang.Down;
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			start = Time.time;
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			hang = Hang.Up;
			count++;
			if (count > 30)
			{
				isQuickAttack = false;
				isAttack = true;
				animator.SetBool("QuickAttack", isQuickAttack);
				animator.SetBool("Attack", isAttack);
				Debug.Log("长按");
				upSpeed = 3.0f;
			}

		}
		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			end = Time.time;
			count = 0;
			if ((end - start) < 0.3)
			{
				isQuickAttack = true;
				isAttack = false;
				animator.SetBool("QuickAttack", isQuickAttack);
				animator.SetBool("Attack", isAttack);
				Debug.Log("短按");
			}
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			start = Time.time;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			if(hang==Hang.Up)
            {
				hang = Hang.Mid;
            }
			count++;
			if (count > 30)
			{
				isQuickAttack = false;
				isAttack = true;
				animator.SetBool("QuickAttack", isQuickAttack);
				animator.SetBool("Attack", isAttack);
				Debug.Log("长按");
			}

		}
		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			end = Time.time;
			count = 0;
			if ((end - start) < 0.3)
			{
				isQuickAttack = true;
				isAttack = false;
				animator.SetBool("QuickAttack", isQuickAttack);
				animator.SetBool("Attack", isAttack);
				Debug.Log("短按");
			}

		}

		if(hang!=Hang.Down)
        {
			Debug.Log(hang);
		}
#else
		if(Input.GetMouseButton(0))
		{
			if(Input.touchCount==1)
            {
				Touch touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began)
				{
					newTouch = true;
					start = Time.time;
				}
				else if(touch.phase==TouchPhase.Stationary)
                {
					if (newTouch && Time.time - start <= 0.4f)
					{
						isQuickAttack = false;
						isAttack = true;
						animator.SetBool("QuickAttack", isQuickAttack);
						animator.SetBool("Attack", isAttack);
						Debug.Log("长按");
					}
				}
				else
                {
					newTouch = false;
					isQuickAttack = true;
					isAttack = false;
					animator.SetBool("QuickAttack", isQuickAttack);
					animator.SetBool("Attack", isAttack);
					Debug.Log("短按");
				}

				Vector3 touchPosition = touch.position;
				hang = Hang.Down;
				if(touchPosition.x> screenWidth/2)
                {
					hang = Hang.Up;
                }
            }
			else if(Input.touchCount==2)
            {
				Touch touch1 = Input.GetTouch(0);
				Touch touch2 = Input.GetTouch(1);


				Vector3 touchPosition1 = touch1.position;
				Vector3 touchPosition2 = touch2.position;

				if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
				{
					newTouch = true;
					start = Time.time;
				}
				else if (touch1.phase == TouchPhase.Stationary && touch1.phase == TouchPhase.Stationary)
				{
					if (newTouch && Time.time - start <= 0.4f)
					{
						isQuickAttack = false;
						isAttack = true;
						animator.SetBool("QuickAttack", isQuickAttack);
						animator.SetBool("Attack", isAttack);
						Debug.Log("长按");
					}
					else
                    {
						newTouch = false;
						isQuickAttack = true;
						isAttack = false;
						animator.SetBool("QuickAttack", isQuickAttack);
						animator.SetBool("Attack", isAttack);
						Debug.Log("短按");
					}
				}

			}
		}
#endif


		if(rigidbody2D.simulated)
        {
			forwordspeed = 1.0f / (60.0f / NoteController.Instance.bpm / 4.0f);
			rigidbody2D.velocity = new Vector3(forwordspeed, 0.0f);
			Debug.Log(rigidbody2D.velocity);
        }




        //更新摄像机位置
        offset = transform.position.x - lastX;
        lastX = transform.position.x;
        Vector3 position = camera.transform.position;
        position.x += offset;
        camera.transform.position = position;

    }
#endregion

#region private Method

#endregion

}