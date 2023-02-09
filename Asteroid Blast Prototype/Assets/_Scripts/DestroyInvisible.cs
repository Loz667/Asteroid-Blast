using UnityEngine;

public class DestroyInvisible : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
