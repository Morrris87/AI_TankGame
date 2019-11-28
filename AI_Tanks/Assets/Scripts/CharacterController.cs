using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController characterController;
    public int health;
    public int speed = 5;
    public int knockBack;

    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            moveDirection = new Vector3(Input.GetAxis("Horozontal"), 0.0f, 0.0f);
            moveDirection *= speed;
        }
        //characterController.Move(moveDirection * Time.deltaTime);
    }
}
