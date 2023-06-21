using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UI_Base : MonoBehaviour
{
    /// <summary>
    /// UI 오브젝트들
    /// </summary>
    [SerializeField] private List<UnityEngine.Object> ui = new List<UnityEngine.Object>();

    [SerializeField] protected Dictionary<string, GameObject> uidic = new Dictionary<string, GameObject>();

    /// <summary>
    /// UI 초기화 부분
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 각 UI Init 실행
    /// </summary>
    private void Start()
    {
        foreach (GameObject obj in ui)
        {
            uidic.Add(obj.name, obj);
        }

        Init();
    }

    /// <summary>
    /// UI 오브젝트 에서 해당 componet 가져오기
    /// </summary>
    /// <param name="name">UI 오브젝트 이름</param>
    /// <typeparam name="T">componet 이름</typeparam>
    protected T Get<T>(string name) where T : UnityEngine.Object
    {
        GameObject obj;
        if (uidic.TryGetValue(name, out obj) == false)
            return null;

        T componet = obj.GetComponent<T>();
        if (componet != null)
            return componet;
            
        return null;
    }

    /// <summary>
    /// 클릭 이벤트 바인딩
    /// </summary>
    /// <param name="go">바인딩할 게임 오브젝트</param>
    /// <param name="action">클릭 이벤트 발생 시 호출될 함수</param>
    public void BindClickEvent(GameObject go, Action<PointerEventData> action)
    {
        Util.GetOrAddComponent<UnityEngine.XR.Interaction.Toolkit.UI.TrackedDeviceGraphicRaycaster>(go);
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        evt.OnClickHandler = null;
        evt.OnClickHandler += action;
    }
}