using UnityEngine;

public class Player : Entity
{
    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            horizontalInput = horizontalInput < 0 ? -1 : 1;
            Walk(horizontalInput);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Punch();
        }
    }
}