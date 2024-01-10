using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;

    [Range(0, 10)][SerializeField] int occurAfterVelocity; //speed needed to spawn the particle

    [Range(0, 0.2f)][SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playerRb;

    float counter;

    [SerializeField] ParticleSystem dodgeParticle;

    private void Update()
    {
        counter += Time.deltaTime;

        if (Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity || Mathf.Abs(playerRb.velocity.y) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }

    public void PlayDodgeParticle()
    {
        dodgeParticle.Play();
    }
}
