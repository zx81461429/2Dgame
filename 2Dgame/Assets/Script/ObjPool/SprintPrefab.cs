using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class SprintPrefab : MonoBehaviour
{
    private SpriteRenderer player;
    private SpriteRenderer Pre;

    float Startime;
    public float duration = 1.5f;


    public PullManager pullManager;


    [Header("颜色参数")]
    public float colorduration;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        Pre = GetComponent<SpriteRenderer>();

        Pre.sprite = player.sprite;
        Pre.color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);

        Pre.transform.position = player.transform.position;
        Pre.transform.localScale = player.transform.localScale;
        Pre.transform.localRotation = player.transform.localRotation;
        Pre.transform.SetParent(PullManager._instance.transform);

        Startime = Time.time;
    }

    private void Update()
    {
        if (Time.time > Startime + duration)
        {
            //返回对象池
            PullManager._instance.BackPull(gameObject);
        }
    }
}
