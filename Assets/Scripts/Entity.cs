using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public float health = 100f;
    public float speed = 3f;

    public Slider healthBar;
    public TMP_Text healthText;

    public Entity opponent;
    public SceneAsset dieScene;


    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private static readonly int WalkState = Animator.StringToHash("walk");
    private static readonly int PunchState = Animator.StringToHash("punch");

    
    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected void Walk(float horizontalInput)
    {
        var transform1 = transform;

        var pos = transform1.position;
        var x = pos.x + horizontalInput * Time.deltaTime * speed;
        if (Mathf.Abs(x) < 7.5f)
        {
            pos = new Vector3(x, pos.y);
            transform1.position = pos;
        }

        _spriteRenderer.flipX = horizontalInput < 0;

        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
        {
            _animator.Play("walk");
        }
    }

    protected void Punch()
    {
        if (Mathf.Abs(opponent.transform.position.x - transform.position.x) < 1f)
        {
            StartCoroutine(opponent.Damage(Random.Range(7, 15)));
        }

        _animator.Play("punch");
    }

    private IEnumerator Damage(int damage)
    {
        _spriteRenderer.color = Color.red;
        
        health = Mathf.Max(0, health - damage);

        healthBar.value = health / 100f;
        healthText.text = $"{health} / 100";
        
        if (health <= 0)
        {
            Die();
        }

        yield return new WaitForSeconds(0.1f);

        _spriteRenderer.color = Color.white;
        
    }
    
    
    private void Die()
    {
        var path = AssetDatabase.GetAssetPath(dieScene);

        SceneManager.LoadScene(path, LoadSceneMode.Single);
    }


}