using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip _pickUpSFX;

    UIManager _uiManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.I))
        {
            Player player = other.GetComponent<Player>();
            player._hasCoin = true;
            _uiManager.UpdateCoinVisibility(true);
            AudioSource.PlayClipAtPoint(_pickUpSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
