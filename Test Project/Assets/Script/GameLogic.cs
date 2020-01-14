using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static float health = 100.0f;
    public static float knowledge = 0.0f;
    public static float pressure = 0.0f;
    public static bool medalFlag = false;
    public static bool ballDamage = false;
    public static bool dropDamage = false;

    public Slider slider;
    public Text text;
    public float knowledge2;

    public Button gameoverBut;
    

    // Start is called before the first frame update
    void Start()
    {
        gameoverBut.onClick.AddListener(delegate { Application.Quit(); });
    }

    // Update is called once per frame
    void Update()
    {
        //health -= 2;

        //更新血量
        if (dropDamage)
        {
            health = health - 2.0f;
            dropDamage = false;
        }

        if (ballDamage)
        {
            health = health - 20.0f;
            ballDamage = false;
        }
        
        //更新血量压力值
        slider.value = health / 100.0f;
        text.text = "knowledge: " + knowledge;

        //检查游戏是否终结
        if(health <= 0)
        {
            gameoverBut.transform.Find("Text").GetComponent<Text>().text = "game over!quit";
            gameoverBut.gameObject.SetActive(true);
        }

        //如果拿到奖章游戏胜利
        if (medalFlag)
        {
            gameoverBut.transform.Find("Text").GetComponent<Text>().text = "victory!quit";
            gameoverBut.gameObject.SetActive(true);
            medalFlag = false;
        }
    }
}
