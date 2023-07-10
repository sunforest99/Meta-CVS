using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField]
    ActionBasedController testinput;

    // Start is called before the first frame update
    void Start()
    {
        testinput.uiPressAction.action.started += started;
        testinput.uiPressAction.action.canceled += canceled;
    }

    void started(InputAction.CallbackContext context)
    {
        if(GameMng.I.Raycast(tras) != null)
        {
            // Debug.Log(GameMng.I.Raycast(tras)?.transform.GetComponent<Product>().objname);
            AddUiInfo(GameMng.I.Raycast(tras)?.transform.GetComponent<Product>());
            Debug.Log("is clicked");
        }
    }

    
    void canceled(InputAction.CallbackContext context)
    {
        Debug.Log("is canceled");
    }

    [SerializeField]
    Transform tras;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(GameMng.I.dataMng.TryGetObjectValue("새우깡").NUTR_CONT1);
        }
    }
    void AddUiInfo(Product script)
    {
        ObjectData data = GameMng.I.dataMng.TryGetObjectValue(script.objname.ToString());
        Debug.Log(script.objname);
        Debug.Log(data.DESC_KOR);
        Debug.Log(data.NUTR_CONT1);
        Debug.Log(data.NUTR_CONT2);
        Debug.Log(data.NUTR_CONT3);
        Debug.Log(data.NUTR_CONT4);
    }
}
