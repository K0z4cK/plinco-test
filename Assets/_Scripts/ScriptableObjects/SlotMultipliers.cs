using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlotMultipliers", menuName = "ScriptableObjects/SlotMultipliersScriptableObject")]
public class SlotMultipliers : ScriptableObject
{
    public List<float> multipliers;
}
