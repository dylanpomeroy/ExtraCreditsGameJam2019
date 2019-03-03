using ExtensionMethods;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MoneyController MoneyController;
    public HealthBar HealthBar;
    public GameObject GameOverText;
    public GameObject RestartButton;

    public AudioSource SoundPlayer;
    public List<AudioClip> HurtSounds;
    public AudioClip GameOverSound;

    public float speed;
    public bool DisableMovement;

    public int CurrentHealth = 100;

    bool alreadyDied = false;
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0 && !alreadyDied)
        {
            SoundPlayer.PlayOneShot(GameOverSound, 5f);
            Time.timeScale = 0.1f;
            GameOverText.SetActive(true);
            Invoke("ShowRestartButton", 0.5f);
            alreadyDied = true;
        }
        else
        {
            SoundPlayer.PlayOneShot(HurtSounds.GetRandom());
        }
    }

    private void ShowRestartButton()
    {

        Time.timeScale = 0f;
        RestartButton.SetActive(true);
    }

    void Start()
    {
        if (speed == 0f) speed = 1;
    }

    void Update()
    {
        HealthBar.UpdateHealth(CurrentHealth / 100f);

        if (DisableMovement)
            return;

        var moveVector = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            moveVector += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            moveVector += Vector2.down;
        if (Input.GetKey(KeyCode.A))
            moveVector += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            moveVector += Vector2.right;

        moveVector = moveVector.normalized * speed * Time.deltaTime;

        GetComponent<CharacterController>().Move(moveVector);
    }

    private void LateUpdate()
    {
        objectIdsAlreadyHit.Clear();
    }

    private List<int> objectIdsAlreadyHit = new List<int>();
    public void HandleCollision(Collider2D collision)
    {
        if (objectIdsAlreadyHit.Contains(collision.gameObject.GetInstanceID()))
            return;

        objectIdsAlreadyHit.Add(collision.gameObject.GetInstanceID());

        if (collision.gameObject.CompareTag("Coin"))
        {
            MoneyController.AddMoney(collision.gameObject.GetComponent<CoinController>().Value);
            CoinInstantiator.DestroyCoin(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<CharacterController>().Move((transform.position - collision.transform.position).normalized * Time.deltaTime * 10);
            TakeDamage(10);
        }
    }
}
