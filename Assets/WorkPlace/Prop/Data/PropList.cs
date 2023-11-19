using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PropList", fileName = "newPropList")]

public class PropList : ScriptableObject
{
    public List<PropData> list = new List<PropData>();
}
