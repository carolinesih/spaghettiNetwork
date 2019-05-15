using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvariantDisplay : MonoBehaviour
{

    public GameObject panel; // this is the panel to be displayed
    public Text text;
    public GameObject table;

    public void DisplayInvariants()
    {
        
        if (!panel.activeSelf)
        {
            table.SetActive(false);
            panel.SetActive(true);
            text.text = "SHOW TABLE";
        }
        else
        {
            table.SetActive(true);
            panel.SetActive(false);
            text.text = "SHOW INVARIANTS";
        }
    }
}