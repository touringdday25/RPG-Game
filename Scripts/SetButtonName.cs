using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonName : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void OnValidate()
    {
        this.GetComponentInChildren<Text>().text = this.name;
    }
}
