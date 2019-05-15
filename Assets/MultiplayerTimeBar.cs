using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MultiplayerTimeBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SpaghettiGameManager.counter > 0)
        {
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(101.39f * (1 - (Time.time - MultiplayerTableCreator.startTime) / MultiplayerTableCreator.timeLimit), 63.7f);
        }
    }
}
