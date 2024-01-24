using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnowCloud : MonoBehaviour
{
    [SerializeField] ParticleSystem slowCloudParticle;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float slowRadius = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayParticleEffect());
    }

    private void PlayParticle()
    {
        slowCloudParticle.Play();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
    }

    IEnumerator PlayParticleEffect()
    {
        PlayParticle();
        yield return new WaitForSeconds(slowCloudParticle.main.duration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetColor(new Color32(0, 255, 248, 255));
            other.GetComponent<Player>().ChangePlayerSpeed(3f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ResetPlayerSpeed();
            other.GetComponent<Player>().SetColor(new Color32(255, 255, 255, 255));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, slowRadius);
    }
}
