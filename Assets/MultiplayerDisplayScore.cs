using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerDisplayScore : MonoBehaviour
{
    public Font enteredFont;
    public void MultiplayerdisplayScoreVariable()
    {
        gameObject.GetComponent<Text>().font = enteredFont;
        gameObject.GetComponent<Text>().text = "CURRENT SCORE: " + MultiplayerTableCreator.score.ToString();
    }
    void Start()
    {
        MultiplayerdisplayScoreVariable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
