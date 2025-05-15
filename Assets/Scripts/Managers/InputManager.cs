using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance 
    {
        get
        {
            if (!_instance)
            {
                _instance = FindFirstObjectByType(typeof(InputManager)) as InputManager;
                if (!_instance) 
                {
                    Debug.Log("No InputManager Instance");
                }
            }
            return _instance;
        }
    }
    static InputManager _instance;
    public PlayerInput playerInput;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this);
        }

        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    private void Start()
    {
    }
}
