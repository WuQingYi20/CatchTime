using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallDestroy : MonoBehaviour {

	//public GameObject Explosion;

	public Vector3 pos;
	private float unitLength = 20;
	public GameObject objectitemListTest;

	//用于道具生成
	public GameObject book;
	public GameObject takeout;
	public GameObject smartPhone;
	public GameObject medal;
	public GameObject shoes;

	//吃了kfc之后 球可以炸毁讲台
	public static bool kfcBall = false;

	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnDestroy() {
		//var tmp = Instantiate(Explosion, pos, transform.rotation);
		var center = pos + new Vector3(0,10,0);
		RaycastHit hit;
		if (Physics.Raycast(origin:center,direction:Vector3.left,hitInfo:out hit,maxDistance:unitLength)) {
			var tmpObj = hit.collider.gameObject;
			Debug.Log("左方"+tmpObj.name);
			Destroy(gameObject);
			if (tmpObj.CompareTag("breakable")) {
				Destroy(tmpObj);
			}

			if (tmpObj.CompareTag("unbreakable") && kfcBall == true)
			{
				Destroy(tmpObj);
			}

			if (tmpObj.name.Equals("桌子(Clone)"))
			{
				var randomNum = Random.Range(0, 100);
				if (randomNum < 10)
				{
					var tmp = Instantiate(takeout, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				if (15 <= randomNum && randomNum < 20)
				{
					var tmp = Instantiate(smartPhone, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				if (20 <= randomNum && randomNum < 30)
				{
					var tmp = Instantiate(shoes, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
			}

			//如果炸弹前方有玩家 那么玩家会损失20生命
			if (tmpObj.CompareTag("Player"))
			{
				GameLogic.ballDamage = true;
			}

			if (tmpObj.name.Equals("bag(Clone)"))
			{
				//还应当有3的倍数和知识的要求
				Debug.Log("书PlayerController.ballTotalCount:" + PlayerController.ballTotalCount);
				Debug.Log("tmpObj.transform.position[2] / 20.0:" + (tmpObj.transform.position[2] / 20.0 / 2 - 1));
				Debug.Log((tmpObj.transform.position[2] / 20.0 / 2 - 1 - PlayerController.ballTotalCount) % 3 == 0.0f);
				if (tmpObj.transform.position[0] == Floor.finalBallX && ((tmpObj.transform.position[2] / 20/ 2 -1 - PlayerController.ballTotalCount) % 3 == 0) && GameLogic.knowledge >= 30)
				{
					var tmp = Instantiate(medal, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				else
				{
					var tmp = Instantiate(book, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
			}


		}
		if (Physics.Raycast(origin:center,direction:Vector3.right,hitInfo:out hit,maxDistance:unitLength)) {
			var tmpObj = hit.collider.gameObject;
			Debug.Log("右方"+tmpObj.name);
			Destroy(gameObject);
			if (tmpObj.CompareTag("breakable")) {
				Destroy(tmpObj);
			}
			if (tmpObj.CompareTag("unbreakable") && kfcBall == true)
			{
				Destroy(tmpObj);
			}

			//如果炸弹前方有玩家 那么玩家会损失20生命
			if (tmpObj.CompareTag("Player"))
			{
				GameLogic.ballDamage = true;
			}

			if (tmpObj.name.Equals("桌子(Clone)"))
			{
				var randomNum = Random.Range(0, 100);
				if (randomNum < 10)
				{
					var tmp = Instantiate(takeout, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				if (15 <= randomNum && randomNum < 20)
				{
					var tmp = Instantiate(smartPhone, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				if (20 <= randomNum && randomNum < 30)
				{
					var tmp = Instantiate(shoes, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
			}



			if (tmpObj.name.Equals("bag(Clone)"))
			{
				//还应当有3的倍数和知识的要求
				//Debug.Log("书PlayerController.ballTotalCount:" + PlayerController.ballTotalCount);
				//Debug.Log("tmpObj.transform.position[2] / 20.0:" + (tmpObj.transform.position[2] / 20.0 / 2 - 1));
				//Debug.Log((tmpObj.transform.position[2] / 20.0 / 2 - 1 - PlayerController.ballTotalCount) % 3 == 0.0f);
				if (tmpObj.transform.position[0] == Floor.finalBallX && ((tmpObj.transform.position[2] / 20 / 2 - 1 - PlayerController.ballTotalCount) % 3 == 0) && GameLogic.knowledge >= 30)
				{
					var tmp = Instantiate(medal, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				else
				{
					var tmp = Instantiate(book, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
			}
		}


		if (Physics.Raycast(origin:center,direction:Vector3.forward,hitInfo:out hit,maxDistance:unitLength)) {
			var tmpObj = hit.collider.gameObject;
			Debug.Log("前方"+tmpObj.name);
			Destroy(gameObject);
			//如果是书包，原地生成书本
			if (tmpObj.name.Equals("bag(Clone)"))
			{
				//还应当有3的倍数和知识的要求
				//Debug.Log("书PlayerController.ballTotalCount:" + PlayerController.ballTotalCount);
				//Debug.Log("tmpObj.transform.position[2] / 20.0:" + (tmpObj.transform.position[2] / 20.0 / 2 - 1));
				//Debug.Log((tmpObj.transform.position[2] / 20.0 / 2 - 1 - PlayerController.ballTotalCount) % 3 == 0.0f);
				if (tmpObj.transform.position[0] == Floor.finalBallX && ((tmpObj.transform.position[2] / 20 / 2 - 1 - PlayerController.ballTotalCount) % 3 == 0) && GameLogic.knowledge >= 30)
				{
					var tmp = Instantiate(medal, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				else
				{
					var tmp = Instantiate(book, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
			}

			//如果是桌子爆炸后可能回生成道具 百分之五十 测试 实际 10
			if (tmpObj.name.Equals("桌子(Clone)"))
			{
				var randomNum = Random.Range(0, 100);
				if (randomNum < 10)
				{
					var tmp = Instantiate(takeout, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				if (15 <= randomNum && randomNum < 20)
				{
					var tmp = Instantiate(smartPhone, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				if (20 <= randomNum && randomNum < 30)
				{
					var tmp = Instantiate(shoes, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
			}

			//如果炸弹前方有玩家 那么玩家会损失20生命
			if (tmpObj.CompareTag("Player"))
            {
				GameLogic.ballDamage = true;
			}

			if (tmpObj.CompareTag("breakable")) {
				//var tmp = Instantiate(tmpObj, tmpObj.transform);
				Destroy(tmpObj);
			}
			if (tmpObj.CompareTag("unbreakable") && kfcBall == true)
			{
				Destroy(tmpObj);
			}
		}
		if (Physics.Raycast(origin:center,direction:Vector3.back,hitInfo:out hit,maxDistance:unitLength)) {
			var tmpObj = hit.collider.gameObject;
			Debug.Log("后方"+tmpObj.name);
			Destroy(gameObject);
			if (tmpObj.CompareTag("breakable")) {
				Destroy(tmpObj);
			}
			if (tmpObj.CompareTag("unbreakable") && kfcBall == true)
			{
				Destroy(tmpObj);
			}

			if (tmpObj.name.Equals("桌子(Clone)"))
			{
				var randomNum = Random.Range(0, 100);
				if (randomNum < 10)
				{
					var tmp = Instantiate(takeout, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				if (15 <= randomNum && randomNum < 20)
				{
					var tmp = Instantiate(smartPhone, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				if (20 <= randomNum && randomNum < 30)
				{
					var tmp = Instantiate(shoes, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
			}


			//如果炸弹前方有玩家 那么玩家会损失20生命
			if (tmpObj.CompareTag("Player"))
			{
				GameLogic.ballDamage = true;
			}

			if (tmpObj.name.Equals("bag(Clone)"))
			{
				if (tmpObj.transform.position[0] == Floor.finalBallX && ((tmpObj.transform.position[2] / 20 / 2 - 1 - PlayerController.ballTotalCount) % 3 == 0) && GameLogic.knowledge >= 30)
				{
					var tmp = Instantiate(medal, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
				else
				{
					var tmp = Instantiate(book, tmpObj.transform.position + new Vector3(0, 5, 0), tmpObj.transform.rotation);
				}
			}
		}
		//Destroy(tmp,2);
	}
}
