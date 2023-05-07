using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviorOnStart : MonoBehaviour
{
    public float maxspeed = 3;
    public static bool isChasing = false;

    public bool isWalking = false;
    [SerializeField] Mission ranaway;

    public static PlayerBehaviorOnStart Instance;


    private void Awake()
    {
        Instance = this;

        PlayerBehavior.canmove = false;

    }
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "goToMap01")
        {
            Destroy(this);
            Icon_Mission.Instance.deleteMission(ranaway);
            SceneManager.LoadScene("Main_map01");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "SnakeyNeck" && isChasing)
        {
            collision.otherRigidbody.velocity = Vector2.zero;
            StarterHandler.startAgain = true;
            isChasing = false;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        UIEffect.Instance.enableCanvas(999);
        UIEffect.Instance.setColor(0, 0, 0, 0);
        UIEffect.Instance.Fade(1, 1);
        
        yield return new WaitForSeconds(1.5f);
        Icon_Mission.Instance.deleteMission(ranaway);
        SceneManager.LoadScene("Main_starter");
    }

    public void startChasing()
    {
        isChasing = true;
        PlayerBehavior.canmove = true;
        SnakeyneckBehavior.Instance.startChasing();
    }

    public void doBump()
    {
        isChasing = false;
        SnakeyneckBehavior.Instance.doBump();
    }

    public void Die()
    {
        Debug.Log("�÷��̾ �׾����ϴ�...");
    }
}
