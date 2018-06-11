using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalPanel : MonoBehaviour {

    public Sprite blueCrystal;
    public Sprite greenCrystal;
    public Sprite redCrystal;

    public Image[] crystals;

    void Awake()
    {
    }

   public void UpdateCrystalPanel(bool [] pickedCrystals)
    {
        if(pickedCrystals[0])
            crystals[0].sprite = blueCrystal;
        if (pickedCrystals[1])
            crystals[1].sprite = greenCrystal;
        if (pickedCrystals[2])
            crystals[2].sprite = redCrystal;
    }

    public void AddCrystal(string type)
    {
        switch (type)
        {
            case "BlueCrystal":
                crystals[0].sprite = blueCrystal;
                break;
            case "GreenCrystal":
                crystals[1].sprite = greenCrystal;
                break;
            case "RedCrystal":
                crystals[2].sprite = redCrystal;
                break;
            default:
                break;
        }
    }
}
