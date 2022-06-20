using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterMapParentVisibility : MonoBehaviour
{
    public static OuterMapParentVisibility Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void DeActivateMap()
    {
        this.gameObject.SetActive(false);
    }
    public void ActivateMap()
    {
        this.gameObject.SetActive(true);
    }
}
