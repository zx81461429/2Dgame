﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().GetHurt(10);
        }
    }
}
