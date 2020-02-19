using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bag;      //背包显示
    bool isBagShow = false;             //背包是否打开
    // Start is called before the first frame update

    public void BtnBagClick()
    {
        if (isBagShow)
        {
            bag.SetActive(false);
            isBagShow = false;
        }
        else
        {
            bag.SetActive(true);
            isBagShow = true;
        }
    }
}
