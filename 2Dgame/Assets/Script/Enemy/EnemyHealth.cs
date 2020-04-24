using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float EnemyBlood = 100;
    private SpriteRenderer EnemyRenderer;

    public float ChangeRedtime;
    public float Redtime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        EnemyRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeRedtime <= Time.time)
            EnemyRenderer.color = Color.white;

        //if (EnemyBlood == 0)
        //    Destroy(this.gameObject, 0.2f);
    }

    public void GetHurt(int HurtNum)
    {
        ChangeRedtime = Time.time + Redtime;
        EnemyBlood -= HurtNum;
        EnemyRenderer.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wuqi"))
        {
            GetHurt(10);
        }
    }
}
