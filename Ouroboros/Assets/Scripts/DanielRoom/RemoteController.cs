using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteController : MonoBehaviour
{

    public delegate void TVStateChangeHandler();
    public static event TVStateChangeHandler OnButtonClick;
    
    public void Click()
    {
        OnButtonClick?.Invoke();
    }
}
