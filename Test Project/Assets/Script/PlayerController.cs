using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public bool canDrop;
	[Range (2, 7)]
	public int maxBallCount;
	[Range (1, 3)]
	public int BoomDelay = 2;
	public GameObject BallList;
	public GameObject ball;

	//用于吃了kfc之后 球的数量暂时增加
	private bool ballAmountFlag = false;
	private float ballSpeedTime = 0.0f;

	//计算总共使用的球数
	public static int ballTotalCount = 0;

	//鞋子可以加速
	private bool shoeSpeed = false;
	private float shoeSpeedTime = 0.0f;

	private Rigidbody body;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		//不允许玩家左右走出边界
		//Debug.Log("z:" + transform.position[2]);
		if (transform.position[2] < -10)
        {

			transform.position = new Vector3(transform.position[0],transform.position[2]+2,-5);
		}

		if (transform.position[2] > 150)
		{

			transform.position = new Vector3(transform.position[0], transform.position[2] + 2, 145);
		}

		//判断主角落下去：x坐标
		//Debug.Log("y:"+ transform.position[1]);
		if (transform.position[0]<(Floor.floorPo/5 - 11)*20-10)
        {
			GameLogic.dropDamage = true;
        }

		//     if (ballAmountFlag)
		//     {
		//if(ballSpeedTime == 0.0f)
		//         {
		//	maxBallCount = maxBallCount * 2;
		//	ballSpeedTime = Time.time;
		//         }

		//if((ballSpeedTime +10.0) <= Time.time)
		//         {
		//	maxBallCount = maxBallCount / 2;
		//	ballAmountFlag = false;
		//	ballSpeedTime = 0.0f;
		//}

		if (shoeSpeed)
		{
			if (shoeSpeedTime == 0.0f)
			{
				speed = speed * 2;
				shoeSpeedTime = Time.time;
			}

			if ((shoeSpeedTime + 10.0) <= Time.time)
			{
				speed = speed / 2;
				shoeSpeed = false;
				shoeSpeedTime = 0.0f;
			}
		}
            //15s后 球的威力恢复
            if (BallDestroy.kfcBall == true)
        {
			if (ballSpeedTime == 0.0f)
            {
                ballSpeedTime = Time.time;
            }

            if ((ballSpeedTime + 15.0) <= Time.time)
            {
				BallDestroy.kfcBall = false;
                ballSpeedTime = 0.0f;
            }

		     }

		if (transform.position.y>0.1) {
			transform.position = new Vector3(transform.position.x,0.1f,transform.position.z);
		}

		if (canDrop && Input.GetKeyDown("space") && GameObject.Find("BallList").transform.childCount<maxBallCount) {
			DropBall();
		}
	}

	private void FixedUpdate() {
		float moveH = Input.GetAxis("Horizontal");
		float moveV = Input.GetAxis("Vertical");

		
		Vector3 movement = new Vector3(speed *moveH,-Mathf.Abs(body.velocity.y), speed *moveV);
		if (Mathf.Abs(moveH)>0 || Mathf.Abs(moveV)>0) {
			if (Mathf.Abs(moveH)>Mathf.Abs(moveV)) {
				if (moveH>0) {
					transform.rotation = Quaternion.Euler(0, 90, 0);
				
				}
				else {
					transform.rotation = Quaternion.Euler(0,270,0);
				}
			}
			else {
				if (moveV > 0) {
					transform.rotation = Quaternion.Euler(0,0,0);
				}
				else {
					transform.rotation = Quaternion.Euler(0,180,0);
				}
			}
		}
		
		//body.AddForce(movement * speed * Time.deltaTime);
		body.velocity =  movement;
		//transform.localEulerAngles = new Vector3(transform.localRotation.x,0,transform.localRotation.z);
	}

	private void DropBall() {
		if (ball) {
			var tmp = Instantiate(ball, repairPosition(transform.position+new Vector3(10,0,10)),ball.transform.rotation);
			tmp.transform.parent = BallList.transform;
			Destroy(tmp,BoomDelay);
			ballTotalCount++;
			Debug.Log("ball"+ ballTotalCount);
		}
	}

	private Vector3 repairPosition(Vector3 pos) {
		float p_x, p_z;
		p_x = pos.x;
		p_z = pos.z;
		int pi_x = (Convert.ToInt32(p_x) / 20) * 20;
		int pi_z = (Convert.ToInt32(p_z) / 20) * 20;
		return new Vector3(pi_x,pos.y,pi_z);
	}

	void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("碰撞器已经启动");
		if (collision.gameObject.name.Equals("book(Clone)"))
		{
			Destroy(collision.gameObject);
			GameLogic.knowledge++;
		}

		//碰到手机后 地板的生成速度提升1.5倍
		if (collision.gameObject.name.Equals("smartPhone(Clone)"))
		{
			Destroy(collision.gameObject);
			Floor.mobileFlag = true;
		}

        //碰到kfc之后 球的威力提升
        if (collision.gameObject.name.Equals("takeout(Clone)"))
        {
			Destroy(collision.gameObject);
			BallDestroy.kfcBall = true;
		}

		//碰到奖章之后 游戏胜利
		if (collision.gameObject.name.Equals("medal(Clone)"))
		{
			Destroy(collision.gameObject);
			GameLogic.medalFlag = true;
		}

		if (collision.gameObject.name.Equals("shoes(Clone)"))
		{
			Destroy(collision.gameObject);
			shoeSpeed = true;
		}

		//碰到鞋子之后 15s内移动速度增加1.5倍
	}

	//void OnTriggerEnter(Collider collision)
	//{
	//	Debug.Log("碰撞器已经启动");
	//	if (collision.gameObject.name.Equals("bag(Clone)"))
	//	{
	//		Destroy(collision.gameObject);
	//		GameLogic.knowledge++;
	//	}
	//}

}
