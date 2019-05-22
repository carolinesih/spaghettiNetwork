using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkSpaghetti : NetworkBehaviour
{
    GameObject feedbackBox;
    [SyncVar]
    public string playername;
    [SyncVar]
    public int score = 0;
    [SyncVar(hook = "OnTempChange")]
    string temp = "";
    [SyncVar(hook = "OnJsonChange")]
    string Json = "";
    // Start is called before the first frame update
    void Start()
    {
        feedbackBox = GameObject.Find("Feedback Box");
    }

    [ClientCallback]
    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        MultiplayerTableCreator.score = score;
        if(MultiplayerEnter.sendInv == true)
        {
            MultiplayerEnter.sendInv = false;
            CmdEnter(GetInputExpression.exp, MultiplayerTableCreator.LogMessage);
        }
        if(MultiplayerEnter.needClear == true)
        {
            MultiplayerEnter.needClear = false;
            Clear();
        }

    }
    [Command]
    public void CmdEnter(string exp, string message)
    {
        string responseJSONS = CallServer.Sim(exp, MultiplayerTableCreator.variableJSON);
        responseJSONS = CallServer.ToSimplify(responseJSONS);
        string responseJSON = CallServer.CallServerOnTautology(responseJSONS, MultiplayerTableCreator.variableJSON);

        int before = responseJSON.IndexOf("\"result\": ") + "result\": ".Length + 1;
        int after = responseJSON.IndexOf("\n}");
        string sampleString = responseJSON.Substring(before, after - before);
        if (sampleString == "true")
        {
            Debug.Log("Tautology, not accepted");
            RpcupdateFeedBack("Tautology not accepted. Try again!", false);
            return;
        }
        if (MultiplayerEnter.acceptedInv.Count > 0)
        {
            if (CallServer.Implied(responseJSONS, MultiplayerEnter.acceptedInv, MultiplayerTableCreator.variableJSON) == true)
            {
                Debug.Log("Implied statement, not accepted");
                RpcupdateFeedBack("Implied statement not accepted. Try again!",false);
                return;
            }
        }
        Json = responseJSONS;
        RpcupdateFeedBack("Invariant accepted. Nice work!", true);
        temp = exp;
        //tableCreatorInstance.counter++;

        //text = panel.transform.Find("Text (1)").gameObject;
        //text.GetComponent<Text>().text += GetInputExpression.exp + '\n';
        RpcupdateScore();
    }

    [ClientRpc]
    public void RpcupdateScore()
    {
        SpaghettiGameManager.counter++;
        MultiplayerTableCreator.startTime = Time.time;
        if(isLocalPlayer)
        {
            score++;
        }
        
    }
    void OnTempChange(string newTemp)
    {
        temp = newTemp;
        UpdateInv.Inv += temp + "\n";
    }
    void OnJsonChange(string newJson)
    {
        Json = newJson;
        MultiplayerEnter.acceptedInv.AddLast(Json);
    }
    public void Clear()
    {
        GetInputExpression.numbers.Clear();
        GetInputExpression.exp = "";
    }
    [ClientRpc]
    public void RpcupdateFeedBack(string message, bool scoring)
    {
        if(isLocalPlayer)
        {
            feedbackBox.GetComponent<Text>().text = message;
        }
        else
        {
            if (scoring == true)
            {
                feedbackBox.GetComponent<Text>().text = "Your opponent have entered a new invarient!";
                MultiplayerTableCreator.opponent++;
            }
        }
    }
}
