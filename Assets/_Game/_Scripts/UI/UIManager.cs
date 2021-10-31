using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    //[SerializeField] private GameObject StartScreen;
    //[SerializeField] private GameObject Options;

    public event Action OnStart;  //Example

    [SerializeField] private Transform content;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
