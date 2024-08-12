using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustForTest : MonoBehaviour
{

    public ScoreScriptableObject ScoreScriptableObject;
    // Start is called before the first frame update
    void Start()
    {
        ScoreScriptableObject.score = 0;
        ScoreScriptableObject.deerBought = false;
    }

}
