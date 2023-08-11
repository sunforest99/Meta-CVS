using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductObjectData", menuName = "Scriptable Object/Product Object Data")]
public class ProductScriptable : ScriptableObject
{
   public new List<string> name;
}
