using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Entity
{
    private void Start()
    {
        StartCoroutine(Bot());
    }

    private IEnumerator Bot()
    {
        var target = transform.position.x;

        while (true)
        {
            if (Mathf.Abs(target - transform.position.x) > 0.05)
            {
                var direction = target - transform.position.x;
                direction = direction < 0 ? -1 : 1;
                Walk(direction);
            }
            else
            {
                Walk(-1);
                target = Random.Range(-2f, 7.3f);
                yield return new WaitForSeconds(Random.Range(0f, 1f));
                Punch();
                yield return new WaitForSeconds(Random.Range(0f, 1f));
            }

            yield return null;
        }
    }
}