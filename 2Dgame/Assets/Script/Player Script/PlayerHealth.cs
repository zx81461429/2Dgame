using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("人物属性")]
    public int Blood;



    public GameObject deathVFXPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //第二个场景用于 死亡 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.layer == LayerMask.NameToLayer("ATC"))
        {
            gameObject.SetActive(false);
            Instantiate(deathVFXPrefab, transform.position, transform.rotation);
        }
    }
}
