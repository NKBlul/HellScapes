using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    private Transform player;
    Vector3 defaultScale;
    [SerializeField] GameObject damagePopup;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        damagePopup = Resources.Load<GameObject>("Prefab/damagePopUp");
        defaultScale = transform.localScale;
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MoveToPlayer(player);
    }

    private void MoveToPlayer(Transform player)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        AdjustEnemyLook(player);
    }

    protected void AdjustEnemyLook(Transform player)
    {
        if (transform.position.x < player.position.x) //enemy on the left
        {
            transform.localScale = defaultScale;
        }
        else if (transform.position.x > player.position.x) //enemy on the right
        {
            transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);
        }
    }

    public override void TakeDamage(float damage)
    {
        ShowDamageText(damage);
        base.TakeDamage(damage);
        AudioManager.instance.PlaySFX("Enemy_Hit");
    }

    private void ShowDamageText(float damagetext)
    {
        GameObject popup = Instantiate(damagePopup, transform.position, Quaternion.identity);
        popup.GetComponentInChildren<TextMeshPro>().text = $"-{damagetext.ToString()}";
        Destroy(popup, 0.7f);
    }

    protected override void Die()
    {
        base.Die();

        moveSpeed = 0;
    }

    //Add score when dead animation finished playing
    public void UpdateScoreWhenDead(int score)
    {
        ScoreManager.UpdateScoreText(score);
    }
}
