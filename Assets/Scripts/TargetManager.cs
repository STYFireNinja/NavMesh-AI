using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    #region Singleton
    public static TargetManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject target;
}
