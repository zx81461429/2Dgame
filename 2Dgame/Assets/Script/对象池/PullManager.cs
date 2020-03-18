using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullManager : MonoBehaviour
{
    public GameObject prefab;
    public static Queue<GameObject> PlayerShadow = new Queue<GameObject>();
    public PlaerMovement Player;

    public static PullManager _instance;

    public float creatTime;
    private float ramainTime;

    private void Start()
    {
        _instance = this;
        InitPull();
        ramainTime = creatTime;
    }

    private void Update()
    {
        if (Player.xVelocity != 0)
            OutPull();
        ramainTime -= Time.deltaTime;
        
    }

    public void InitPull()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject gameObject = Instantiate(prefab);
            PlayerShadow.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void BackPull(GameObject gameObject)
    {
        PlayerShadow.Enqueue(gameObject);
        gameObject.SetActive(false);
    }

    public void OutPull()
    {
        if (ramainTime > 0)
            return;
        print(PlayerShadow.Count);
        if (PlayerShadow.Count <= 1)
            InitPull();
        GameObject gameObject = PlayerShadow.Dequeue();
        gameObject.SetActive(true);
        ramainTime = creatTime;
    }

}
