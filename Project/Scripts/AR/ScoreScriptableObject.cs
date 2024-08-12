using UnityEngine;

[CreateAssetMenu(fileName = "NewScoreScriptableObject", menuName = "ScoreScriptableObject")]
public class ScoreScriptableObject : ScriptableObject
{
    public int score;
    public bool deerBought;
}