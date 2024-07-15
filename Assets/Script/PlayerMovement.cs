using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isPlayer1; 

    private string horizontalAxis;
    private string verticalAxis;

    private void Awake()
    {
        if (isPlayer1)
        {
            horizontalAxis = "P1_Horizontal";
            verticalAxis = "P1_Vertical";
        }
        else
        {
            horizontalAxis = "P2_Horizontal";
            verticalAxis = "P2_Vertical";
        }
    }

    private void Update()
    {
        float moveX = Input.GetAxis(horizontalAxis);
        float moveY = Input.GetAxis(verticalAxis);

        Vector3 moveDirection = new Vector3(moveX, moveY, 0f).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
