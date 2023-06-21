using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : UI_Base
{
    [SerializeField] UnityEngine.UI.Button test;

    public override void Init()
    {
        //Debug.Log(ui.GetEnumerator());
        test = Get<UnityEngine.UI.Button>("Button (1)");

        BindClickEvent(test.gameObject, testbtn);
    }

    public void testbtn(PointerEventData eventData)
    {
        Debug.Log("test");
    }
}
