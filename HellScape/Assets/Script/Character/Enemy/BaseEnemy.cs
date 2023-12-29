using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    private Transform player;
    Vector3 defaultScale;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        defaultScale = transform.localScale;
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        if (transform.position.x < player.position.x) //enemy on the left
        {
            transform.localScale = defaultScale;
        }
        else if (transform.position.x > player.position.x) //enemy on the right
        {
            transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);
        }
    }

    protected override void Die()
    {
        base.Die();
        
        moveSpeed = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            moveSpeed = 0;
            UIManager.Instance.losePanel.SetActive(true);
            other.gameObject.GetComponent<Player>().OnDisable();
            UIManager.Instance.loseScore.text = $"Score: {ScoreManager.score.ToString()}";
            UIManager.Instance.loseTimer.text = UIManager.Instance.timerText.text;
        }
    }

    public void UpdateScoreWhenDead(int score)
    {
        ScoreManager.UpdateScoreText(score);
    }
}
