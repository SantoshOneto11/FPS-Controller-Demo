using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [Header("General")]
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _nextFireTime = 0.01f;
    [SerializeField] private int _ammoCount = 30;
    public int totalAmmo = 30;

    [Header("VFX's")]
    [SerializeField] private Transform muzzelPos;
    [SerializeField] private ParticleSystem _muzzelFlash;
    [SerializeField] private ParticleSystem _hitFlash;

    private Camera _camera;

    public FireMode fireMode = FireMode.MANUAL;     //Firemode to autoReload Gun
    private void Start()
    {
        _camera = Camera.main;
    }

    public float GetFireRate() => _fireRate;

    public int GetBulletsCount() => _ammoCount;

    public FireMode GetFireMode() => fireMode;

    public void Shoot()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        RaycastHit _hit;
        if (Time.time > _nextFireTime && _ammoCount > 0)
        {
            _ammoCount--;
            Ray ray = _camera.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out _hit))
            {
                Transform objectHit = _hit.transform;

                var muzzelEffect = Instantiate(_muzzelFlash, muzzelPos.position, muzzelPos.rotation);
                muzzelEffect.transform.parent = muzzelPos;

                var hitEffect = Instantiate(_hitFlash, _hit.point, Quaternion.LookRotation(_hit.normal));
                hitEffect.transform.parent = objectHit.transform;

            }
            Debug.Log("Gun Fired!, Ammo left " + _ammoCount);
        }
    }

    public void ChangeFireMode()
    {
        fireMode = fireMode == FireMode.MANUAL ? FireMode.AUTO : FireMode.MANUAL;
    }

    public void Reload()
    {
        Debug.Log("Gun Reloaded!");
        _ammoCount = totalAmmo;
    }    
}
