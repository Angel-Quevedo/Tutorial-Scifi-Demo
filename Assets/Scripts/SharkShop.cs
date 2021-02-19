using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    [SerializeField] AudioClip _winSFX;

    UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Player player = other.GetComponent<Player>();

            if (player._hasCoin)
            {
                player._hasCoin = false;
                _uiManager.UpdateCoinVisibility(player._hasCoin);
                player.EnableWeapon();
                AudioSource.PlayClipAtPoint(_winSFX, Camera.main.transform.position);
            }
            else
            {
                Debug.Log("Go Away");
            }
        }
    }
}
