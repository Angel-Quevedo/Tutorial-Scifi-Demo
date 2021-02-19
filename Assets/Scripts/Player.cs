using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float _movementSpeed = 1f;
    [SerializeField] GameObject _muzzeFlash;
    [SerializeField] GameObject _hitMarkerPrefab;
    [SerializeField] GameObject _weapon;
    [SerializeField] AudioSource _weaponAudioSource;
    [SerializeField] int _currentAmmo;

    int _maxAmmo = 50;
    float _gravity = 9.81f;
    CharacterController _characterController;
    bool _canShoot = true;
    UIManager uiManager;
    public bool _hasCoin;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        uiManager = FindObjectOfType<UIManager>();


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _currentAmmo = _maxAmmo;
        uiManager.UpdateAmmoText(_currentAmmo);
        uiManager.UpdateCoinVisibility(_hasCoin);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.R) && _canShoot)
        {
            StartCoroutine(ReloadCoroutine());
        }

        HandleShoot();
    }

    IEnumerator ReloadCoroutine()
    {
        _canShoot = false;
        yield return new WaitForSeconds(1.5f);
        _canShoot = true;
        _currentAmmo = _maxAmmo;
        uiManager.UpdateAmmoText(_currentAmmo);
    }

    private void HandleShoot()
    {
        if (Input.GetMouseButton(0) && _currentAmmo > 0 && _canShoot)
        {
            if (!_weaponAudioSource.isPlaying)
                _weaponAudioSource.Play();

            _muzzeFlash.SetActive(true);
            _currentAmmo--;
            uiManager.UpdateAmmoText(_currentAmmo);

            Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(rayOrigin, out RaycastHit hitInfo, Mathf.Infinity))
            {
                Debug.Log("raycast hit " + hitInfo.transform.gameObject.name);
                var hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitMarker, 1f);

                Destructible craate = hitInfo.transform.GetComponent<Destructible>();
                if (craate)
                    craate.DestroyCrate();
            }
        }
        else
        {
            _muzzeFlash.SetActive(false);
            _weaponAudioSource.Stop();
        }
    }

    private void HandleMovement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * _movementSpeed;
        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity);

        _characterController.Move(velocity * Time.deltaTime);
    }


    public void EnableWeapon()
    {
        _weapon.SetActive(true);
        Debug.Log("weapon system enabled");
    }
}
