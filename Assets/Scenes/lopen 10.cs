using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lopen10 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }
    [SerializeField] private float moveSpeed = 7f;
    // Update is called once per frame
    void Update()
    {

        Vector3 inputVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            inputVector.z = 1;
        }
        if (Input.GetKey(KeyCode.F))
        {
            inputVector.z = -1;
        }
        inputVector = inputVector.normalized;
        Vector3 direction = new Vector3(inputVector.x, inputVector.z , inputVector.y);
        //transform.position += direction * Time.deltaTime * moveSpeed;

        //transform.forward = direction;

        float rotatespeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotatespeed);



        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, direction, moveDistance);
        Debug.Log(canMove);

        if (!canMove)
        {
            Vector3 directionX = new Vector3(direction.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, directionX, moveDistance);

            if (canMove)
            {
                direction = directionX;
            }
            else
            {
                Vector3 directionZ = new Vector3(0, 0, direction.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, directionZ, moveDistance);
                if (canMove)
                {
                    direction = directionZ;
                }
                else
                {
                    Vector3 directionY = new Vector3(0, direction.y, 0).normalized;
                    canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, directionY, moveDistance);
                    if (canMove)
                    {
                        direction = directionY;
                    }
                }
            }
        }
        if (canMove)
        {
            transform.position += direction * moveDistance;
        }

    }
}

