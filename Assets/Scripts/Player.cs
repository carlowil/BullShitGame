using UnityEngine;

public class Player : Entity
{
    public float attackSpeed;

    private float _lastPunch;
    
    
    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            horizontalInput = horizontalInput < 0 ? -1 : 1;
            Walk(horizontalInput);
        }

        if (Input.GetMouseButtonDown(0) && Time.time - _lastPunch > 1f / attackSpeed)
        {
            Punch();
            _lastPunch = Time.time;
        }
    }
}