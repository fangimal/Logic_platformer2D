using System;
using System.Collections.Generic;
using UnityEngine;

internal class ExitDoor : MonoBehaviour
{
    [SerializeField] private Color _enterColor;
    private Color _myColor;

    public event Action OnDoorOpened;
    public event Action OnDoorClosed;

    private void Start()
    {
        _myColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Hero>())
        {
            gameObject.GetComponent<SpriteRenderer>().color = _enterColor;
            OnDoorOpened?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().color = _myColor;
        OnDoorClosed?.Invoke();
    }

}
