using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] ParticleSystem explosiveParticle;
    [SerializeField] SpriteRenderer spriteRenderer;
    Color startColor = Color.black;
    Color endColor = Color.red;
    float timeTillExplode = 2f;
    float explodeRadius = 1f;
    bool isExploding = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeColor(startColor, endColor, timeTillExplode));
    }

    IEnumerator ChangeColor(Color startColor, Color endColor, float timeToChange)
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeToChange) 
        {
            float t = elapsedTime / timeToChange;
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        spriteRenderer.color = endColor;
        isExploding = true;
        Explode();
    }

    private void Explode()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        explosiveParticle.Play();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRadius);

        foreach(Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Player player = collider.GetComponent<Player>();
                if (player != null) 
                {
                    player.TakeDamage(player.GetHP());
                }
            }
        }
    }

    private void Update()
    {
        if (!explosiveParticle.isPlaying && isExploding)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}
