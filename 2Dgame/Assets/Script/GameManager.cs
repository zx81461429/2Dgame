using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject bag;      //背包显示
    public GameObject shop;     //商店
    public GameObject equip;    //装备栏

    bool isBagShow = true;     //背包是否打开
    bool isShopShow = false;     //背包是否打开
    bool isPlayerShow = true;     //背包是否打开
    // Start is called before the first frame update

    public void BtnBagClick()
    {
        if (isBagShow)
        {
            bag.GetComponent<Image>().rectTransform.DOLocalMoveX(280, 1.5f);
            isBagShow = false;
        }
        else
        {
            bag.GetComponent<Image>().rectTransform.DOLocalMoveX(785, 1.5f);
            isBagShow = true;
        }
    }

    public void BtnShopClick()
    {
        if (isShopShow)
        {
            shop.SetActive(false);
            isShopShow = false;
        }
        else
        {
            shop.SetActive(true);
            isShopShow = true;
        }
    }

    public void BtnPlayerClick()
    {
        if (isPlayerShow)
        {
            equip.GetComponent<Image>().rectTransform.DOLocalMoveX(-85, 1.5f);
            isPlayerShow = false;
        }
        else
        {
            equip.GetComponent<Image>().rectTransform.DOLocalMoveX(-850, 1.5f);
            isPlayerShow = true;
        }
    }
}
