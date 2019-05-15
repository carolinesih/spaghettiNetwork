using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerEnter : MonoBehaviour
{
    public GameObject tableCreator;

    public static bool sendInv = false;
    public static bool needClear = false;
    public static string temp;
    public static LinkedList<string> acceptedInv = new LinkedList<string>();

    public void MultiEnter()
    {
        needClear = true;
        var tableCreatorInstance = tableCreator.GetComponent<MultiplayerTableCreator>();
        Debug.Log("Entered");
        for (int i = 0; i < tableCreatorInstance.rows; i++)
        {
            if (tableCreatorInstance.textArray[i, tableCreatorInstance.cols - 1].GetComponent<Text>().text != "True")
            {
                Debug.Log("returning");
                return;
            }
        }
        sendInv = true;
        temp = GetInputExpression.exp;
    }
}
