using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] GameObject counterUI = null;

    [SerializeField] UI_Counter canvas = null;

    private void Start()
    {
        canvas = counterUI.GetComponent<UI_Counter>();
    }

    /// <summary>
    ///  플레이어가 콜라이더 안에 있을 때  결제 창 UI 표시 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Equals("<isMinePlayer>"))
        {
            counterUI.SetActive(true);
            canvas.SetUICounter();
        }
    }
    
    /// <summary>
    /// 플레이어가 콜라이더 밖에 있을 때 결제 창 UI 끄기
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name.Equals("<isMinePlayer>"))
        {
            counterUI.SetActive(false);
        }
    }
}
