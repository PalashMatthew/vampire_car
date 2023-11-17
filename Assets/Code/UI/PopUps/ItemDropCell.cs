using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropCell : MonoBehaviour
{
    public Image imgIcon;
    public Image imgCell;

    public Sprite sprCommon, sprRare, sprEpic;

    public Sprite sprIcon;

    public string rarity;


    public void Initialize()
    {
        transform.localScale = Vector3.one;

        imgIcon.sprite = sprIcon;

        if (rarity == "common")
        {
            imgCell.sprite = sprCommon;
        }

        if (rarity == "rare")
        {
            imgCell.sprite = sprRare;
        }

        if (rarity == "epic")
        {
            imgCell.sprite = sprEpic;
        }
    }
}
