using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSelector : MonoBehaviour
{
    [SerializeField] private GameObject mainPannel;
    [SerializeField] private GameObject[] MaterialPannels;
    [SerializeField] private GameObject backButton;

    public void Start()
    {
        foreach (GameObject materials in MaterialPannels)
        {
            materials.SetActive(false);
        }
    }
    public void OpenInterface(int objectToActivate)
    {
        mainPannel.SetActive(false);
        backButton.SetActive(true);
        MaterialPannels[objectToActivate].SetActive(true);
        for (int i = 0; i < MaterialPannels.Length; i++)//turns off any pannel that is not being turned on
        {
            if(i != objectToActivate)
            {
                MaterialPannels[i].SetActive(false);
            }

        }
    }
    public void CloseInterface()
    {
        for (int i = 0; i < MaterialPannels.Length; i++)//turns off all pannels
        {
            MaterialPannels[i].SetActive(false);
        }
        mainPannel.SetActive(true);
        backButton.SetActive(false);
    }
}
