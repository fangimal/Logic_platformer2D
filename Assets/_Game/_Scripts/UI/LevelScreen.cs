using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class LevelScreen : MonoBehaviour
{
    [SerializeField] private ExitDoor _exitDoor;

    [SerializeField] private Button _exitButton;

    void Start()
    {
        _exitButton.gameObject.SetActive(false);

        _exitDoor.OnDoorOpened += (() => 
        {
            Debug.Log("Enter Hero!");
            _exitButton.gameObject.SetActive(true);
        });

        _exitDoor.OnDoorClosed += (() =>
        {
            _exitButton.gameObject.SetActive(false);
            Debug.Log("NO!");
        });

    }

    

    void Update()
    {
        
    }
}
