using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bets", menuName = "ScriptableObjects/BetsScriptableObject")]
public class Bets : ScriptableObject
{
    public List<float> bets;
}
