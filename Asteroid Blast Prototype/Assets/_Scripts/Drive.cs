using UnityEngine;
using UnityEngine.UI;

public class Drive : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject impactFX;
    [SerializeField] GameObject explodeFX;

    //[SerializeField] GameEvent addScore;
    private float score = 0f;
    private float scoreMultiplier = 10f;
    ScoreDisplay scoreDisplay;

    public bool gameOver = false;

    private void Start()
    {
        scoreDisplay = GameObject.FindWithTag("Score").GetComponent<ScoreDisplay>();
    }

    void Update()
    {
        float translation = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Pool.instance.GetObject("Bullet");
            if (bullet != null)
            {
                bullet.transform.position = this.transform.position;
                bullet.SetActive(true);
            }
        }

        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position) + new Vector3 (0, -140, 0);
        healthBar.transform.position = screenPos;

        if (!gameOver)
        {
            score += scoreMultiplier * Time.deltaTime;
            scoreDisplay.UpdateScoreDisplay((int)score);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Instantiate(impactFX, other.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
            healthBar.value -= 10;
            if (healthBar.value <= 0)
            {
                Instantiate(explodeFX, this.transform.position, Quaternion.identity);
                Destroy(healthBar.gameObject, 0.1f);
                Destroy(this.gameObject, 0.1f);
            }
        }
    }
}