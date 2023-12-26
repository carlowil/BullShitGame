using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health = 100f;
    public float speed = 3f;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private static readonly int WalkState = Animator.StringToHash("walk");
    private static readonly int PunchState = Animator.StringToHash("punch");


    protected void Walk(float horizontalInput)
    {
        var transform1 = transform;

        var pos = transform1.position;
        var x = pos.x + horizontalInput * Time.deltaTime * speed;
        pos = new Vector3(x, pos.y);
        transform1.position = pos;

        _spriteRenderer.flipX = horizontalInput < 0;

        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
        {
            _animator.Play("walk");
        }
    }

    protected void Punch()
    {
        _animator.Play("punch");
    }

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
}