using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;


public class JsonItemList {
	public List<Item> Items;
	public int total;
}

[Serializable]
public class Item {
	public int id;
	public string name;
	public int relpos_x;
	public int relpos_z;
	public int angle;
	public Item(int a,string b,int c,int d,int e)
    {
		id = a;
		name = b;
		relpos_x = c;
		relpos_z = d;
		angle = e;
    }
}


public class Floor : MonoBehaviour
{

	public GameObject cube;
	public int floorWidth = 8;
	public int floorLength = 12;
	public MeshFilter mf;
	public GameObject floor;
	public bool infiniteTime;
	public GameObject SchoolBag, Chair, Desk, LongDesk, Podium;
	public List<List<GameObject>> FloorList = new List<List<GameObject>>();

	public List<GameObject> ItemList = new List<GameObject>();
	public Dictionary<int, GameObject> indexDict = new Dictionary<int, GameObject>();

	public GameObject objectitemList;
	private int actionMove = 10;
	private float tile_height = 2;
	private int timeInterval = 3;
	private int beginWaitTime = 7;
	private int unitLength = 20;
	private int count = -5;
	private static int delta = 5;
	private GameObject tmp;

	//用作地板的后续生成和销毁
	public static int floorPo;
	private int floorCreV = 3;
	private float floorTime = 5.0f;
	private float totalTime = 6.0f;

	//用作生成道具
	private int difficulty = 13;
	private List<List<Item>> itemsLists = new List<List<Item>>();
	private Dictionary<int, int> difficultyList = new Dictionary<int, int>();
	private List<int> itemCList = new List<int>();
	private List<Item> itemsList = new List<Item>();
	private int geneCo = 0;

	//用于反馈道具效果
	public static bool mobileFlag = false;
	private float speedUpTime = 0;
	private float lastTime;

	//地板和道具整体的控制
	private int addTotalAmount = 21;
	public static int finalBallX = 10000; 

	//用于谜题生成
	private List<List<int>> puzzleList = new List<List<int>>();

	// Use this for initialization
	void Start()
	{

		initFloors();
		indexDict.Add(4, SchoolBag);
		indexDict.Add(3, Chair);
		//5
		indexDict.Add(1, Podium);
		//6
		indexDict.Add(2, Desk);
        difficultyList.Add(4,1);
		difficultyList.Add(3, 1);
		difficultyList.Add(1, 3);
		difficultyList.Add(2, 2);
		//loadItemsByJsonFile("Map1.json");

		//创建谜题
		List<int> puzzle1 = new List<int>( new int[] {
			0,0,0,0,0,0,0,0,
			0,1,3,1,3,1,3,0,
			0,1,4,1,4,1,4,0
		}
			);
		puzzleList.Add(puzzle1);

		// 标识最后的球
		finalBallX = (floorLength - 1 + floorCreV * addTotalAmount) * unitLength;
	}

	// Update is called once per frame
	void Update()
	{




		if (!infiniteTime)
		{
			for (int i = 0; i < floorLength; i++)
			{
				for (int j = 0; j < floorWidth; j++)
				{
					if (FloorList[i][j] != null)
					{
						if (Time.timeSinceLevelLoad > beginWaitTime + i * timeInterval)
						{
							Vector3 obj_p = FloorList[i][j].transform.position;
							FloorList[i][j].transform.DOMove(obj_p - new Vector3(0, 60, 0), Random.Range(0f, 4f));
						}
					}
				}

			}
		}
        /*if (Time.time>5) {
	        foreach (Transform child in floor.transform) {
				Vector3 obj_p = child.position;
	            child.transform.DOMove(obj_p-new Vector3(0,60,0), Random.Range(0f, 5f));

	        }
	    }*/
     //   for (int i = 0; i < floorLength; i++)
     //   {
     //       for (int j = 0; j < floorWidth; j++)
     //       {
     //           if (FloorList.Count > i && FloorList[i].Count > j && FloorList[i][j] != null)
     //           {
     //               if (FloorList[i][j].GetComponent<Rigidbody>().velocity.x != 0.0f)
     //               {
					//	//Debug.Log("vy:" + FloorList[i][j].GetComponent<Rigidbody>().velocity.x);
					//	//Debug.Log("count:" + FloorList.Count);
					//}
     //               float f_y = FloorList[i][j].GetComponent<Rigidbody>().velocity.x;
     //               FloorList[i][j].GetComponent<Rigidbody>().velocity = new Vector3(0.0f, f_y, 0.0f);
     //           }
     //       }

     //   }


        //如果在使用手机的状态 地板的生成速度增加
        if (mobileFlag)
        {
			//开启计时器 10s后过期
			if(lastTime == 0)
            {
				Debug.Log("开始加速");
				floorTime = floorTime * 2;
				lastTime = Time.time;
			}

            if ((Time.time - 10.0) > lastTime)
            {
				Debug.Log("完成加速");
				mobileFlag = false;
				lastTime = 0.0f;
				floorTime = floorTime / 2;
			}

        }

		//地板的后续生成和销毁
		if (Time.time > totalTime && addTotalAmount > 0)
		{
			totalTime = floorTime + Time.time;
			Debug.Log(FloorList.Count);
			deleteFloors();


			addFloors();

            //难度调整
			difficulty++;
            
			if (addTotalAmount  == 21)
			{
				difficulty -= 3;
			}
			if (addTotalAmount == 11)
			{
				difficulty -= 3;
			}

			if (addTotalAmount > 1)
            {
				addItems();
            }
            else
            {
				addPuzzle(puzzleList[0]);
				//帮助标识那三个
				
			}
			addTotalAmount--;

			//在知识达到一定量的时候细心查找 回忆以往来确定位置：扔出球的数量
			//可以根据addPuzzle来生成
		}

	}

	private void FixedUpdate()
	{

	}

	private void initFloors()
	{
		for (int count = 0; count < floorLength * delta; count++)
		{
			if (count % delta == 0 && count < floorLength * delta)
			{
				int i = count / delta;
				List<GameObject> temlist = new List<GameObject>();
				for (int j = 0; j < floorWidth; j++)
				{
					var randomHeight = Random.Range(5f, 15f);
					tmp = GameObject.Instantiate(cube,
						new Vector3(unitLength * i, -(actionMove + randomHeight), unitLength * j),
						Quaternion.identity);
					tmp.transform.parent = floor.transform;
					tmp.transform.DOMove(new Vector3(unitLength * i, 0, unitLength * j), Random.Range(0f, 4f));
					temlist.Add(tmp);
				}
				FloorList.Add(temlist);
			}
			floorPo = count;
		}
	}

	private void loadItemsByJsonFile(string Jsonname)
	{
		string jsonString = File.ReadAllText(Application.dataPath + @"/Maps/" + Jsonname);
		JsonItemList itemList = JsonUtility.FromJson<JsonItemList>(jsonString);

		Debug.Log(itemList.total);
		foreach (var item in itemList.Items)
		{
			Debug.Log(item.relpos_x);
			var rangeHeight = Random.Range(20f, 40f);
			var tmp = Instantiate(indexDict[item.id],
				new Vector3(item.relpos_x * unitLength, tile_height + actionMove * 4 + rangeHeight, item.relpos_z * unitLength),
				Quaternion.Euler(0, item.angle, 0));

			tmp.transform.parent = objectitemList.transform;
			tmp.transform.DOMove(new Vector3(item.relpos_x * unitLength, tile_height,
				item.relpos_z * unitLength), Random.Range(0f, 4f));
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("物体接触了");
	}

	//添加地板
	private void addFloors()
	{
		for (int count = floorPo; count < floorCreV * delta + floorPo; count++)
		{
			if (count % delta == 0 && count < floorCreV * delta + floorPo)
			{
				int i = count / delta;
				List<GameObject> temlist = new List<GameObject>();
				for (int j = 0; j < floorWidth; j++)
				{
					var randomHeight = Random.Range(5f, 15f);
					tmp = GameObject.Instantiate(cube,
						new Vector3(unitLength * i, -(actionMove + randomHeight), unitLength * j),
						Quaternion.identity);
					tmp.transform.parent = floor.transform;
					tmp.transform.DOMove(new Vector3(unitLength * i, 0, unitLength * j), Random.Range(0f, 2f));
					temlist.Add(tmp);
				}
				FloorList.Add(temlist);
			}
		}
		floorPo = floorPo + floorCreV * delta;
	}

	//地板的销毁
	private void deleteFloors()
	{
		//FloorList.RemoveRange(floorPo / delta - floorLength, floorPo / delta - floorLength + floorCreV);
        for (int i = floorPo / delta - floorLength + 1;i< floorPo / delta - floorLength + floorCreV + 1;i++)
        {
			for(int j = 0; j < floorWidth; j++)
            {
				Destroy(FloorList[i][j]);
				//FloorList[i][j].transform.DOMove(new Vector3(unitLength * i, 0, unitLength * j), Random.Range(0f, 4f));
			}
		}
	}

	private void addItems()
    {
		int difficultyC = difficulty;
		//Debug.Log(difficultyC);
		//确定要生成的物品
		for (int i = 1; i < 5; i++)
        {
			if(i == 4)
            {
				itemCList.Add(difficultyC);
				difficultyC = 0;
				break;
			}

			itemCList.Add(Random.Range(0, difficultyC / difficultyList[i]));
			difficultyC = difficultyC - itemCList[i - 1] * difficultyList[i];

		}
		//difficulty++;


		//Debug.Log(difficultyC);


		//具体到地板位置-补充itemList
		//避免位置重叠

		List<Vector2Int> positionList = new List<Vector2Int>();
        for (int i=0;i<floorCreV;i++)
        {
			for (int j = 0; j < floorWidth; j++)
			{
				positionList.Add(new Vector2Int(i,j));
            }
        }

		itemsLists.Add(new List<Item>());
		for (int i=0;i<itemCList.Count;i++)
        {
			for(int j = 0; j < itemCList[i]; j++)
            {
				if(positionList.Count == 0)
                {
					break;
                }
				int randP = Random.Range(0, positionList.Count);
				Item temp = new Item(i+1,"", floorPo / delta - 2 + positionList[randP][0], positionList[randP][1],0);
				itemsLists[geneCo].Add(temp);
				positionList.RemoveAt(randP); 
            }
        }
		//具体生成

		//Debug.Log("count: "+ itemsLists[geneCo].Count);
		//Debug.Log("itemCList: " + itemCList.Count);
		foreach (var item in itemsLists[geneCo])
		{
			
			var rangeHeight = Random.Range(20f, 40f);
			var tmp = Instantiate(indexDict[item.id],
				new Vector3(item.relpos_x * unitLength, tile_height + actionMove * 4 + rangeHeight, item.relpos_z * unitLength),
				Quaternion.Euler(0, item.angle, 0));

			tmp.transform.parent = objectitemList.transform;
			tmp.transform.DOMove(new Vector3(item.relpos_x * unitLength, tile_height + 5,
				item.relpos_z * unitLength), Random.Range(2f, 4f));
		}

		itemCList = new List<int>();
		geneCo++;
		
	}

	//根据一个长度为18的数组来布置场景
	private void addPuzzle(List<int> p)
    {
		List<Item> tmpList = new List<Item>();
		//转化到一个itemList里面 id name x z angle
		for(int i=0;i<p.Count;i++)
        {
			if(p[i] != 0)
            {
				Item temp = new Item(p[i], "", floorPo / delta - 2 + i / floorWidth,i % 8,0);
				tmpList.Add(temp);
			}	
        }


		//场景中具体生成
		foreach (var item in tmpList)
		{

			var rangeHeight = Random.Range(20f, 40f);
			var tmp = Instantiate(indexDict[item.id],
				new Vector3(item.relpos_x * unitLength, tile_height + actionMove * 4 + rangeHeight, item.relpos_z * unitLength),
				Quaternion.Euler(0, item.angle, 0));

			tmp.transform.parent = objectitemList.transform;
			tmp.transform.DOMove(new Vector3(item.relpos_x * unitLength, tile_height,
				item.relpos_z * unitLength), Random.Range(0f, 4f));
		}
	}
}

