using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] Vector3 velocity;
    [SerializeField] GameObject impactFX;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Instantiate(impactFX, this.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        this.transform.Translate(velocity);
    }
}
