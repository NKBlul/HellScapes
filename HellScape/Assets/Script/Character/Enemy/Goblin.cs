using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : BaseEnemy
{
    public GameObject bomb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        maxHp = 6f;
        moveSpeed = 2f;
        damage = 3f;

        currentHp = maxHp;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Die()
    {
        AudioManager.instance.PlaySFX("Goblin_Hit");
        //SpawnBomb(4);

        base.Die();
    }

    private void SpawnBomb(int numOfBomb)
    {
        float xOffset = 1f;
        float yOffset = 1f;

        for (int i = 1; i < numOfBomb + 1; i++)
        {
            if (i % 2 == 0)
            {
                Instantiate(bomb, new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z), Quaternion.identity);
                yOffset -= (yOffset * 2);
            }
            else
            {
                Instantiate(bomb, new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z), Quaternion.identity);
                xOffset -= (xOffset * 2);
            }
            
        }
    }
}
