using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpSlider : MonoBehaviour
{
    Slider slider;
    Player player;  // プレイヤーのインスタンスを取得するための変数
    PlayerTest playerTest;

    void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();

        // Playerのインスタンスを取得
        player = GameObject.Find("ジブンノウマ").GetComponent<Player>();
        playerTest = GameObject.Find("ジブンノウマ").GetComponent<PlayerTest>();

        slider.value = 1;
    }

    void Update()
    {
        if (player==true)
        {
            // プレイヤーのスタミナをスライダーに反映
            slider.value = Player.Stamina / 600f;
        }
        else
        {
            slider.value = playerTest.Stamina / 600f;
        }
    }

    public void HideSlider()
    {
        slider.gameObject.SetActive(false);
    } 
}
