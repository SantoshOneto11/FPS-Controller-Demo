using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<Gun> _weapons = new List<Gun>();
    public TMP_Text ammoCount, fireModeTxt;

    private int activeWeaponIndex = 0;
    public int ActiveWeaponIndex
    {
        get => activeWeaponIndex;
        set => activeWeaponIndex = value;
    }

    //Events 
    private event Action UpdateAmmoEvent;   //Just Update AmmoCount
    private event Action GetFireModeEvent;
    private void Awake()
    {
        UpdateAmmoEvent = GetRemainingAmmo;
        GetFireModeEvent = CurrentFireMode;
    }
    private void Start()
    {
        foreach (Transform child in transform)
        {
            // Check if the child has a component that implements IWeapon
            Gun weapon = child.GetComponent<Gun>();
            if (weapon != null)
            {
                _weapons.Add(weapon); // Add the weapon to the list
                Debug.Log("Weapon Added!");
            }
        }

        if (_weapons.Count > 0)
        {
            EquiqWeapon(activeWeaponIndex);
            UpdateAmmoEvent?.Invoke();
        }

        GetFireModeEvent?.Invoke();
    }

    public void EquiqWeapon(int index)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            if (i == index)
            {
                _weapons[i].gameObject.SetActive(true);
                GetFireModeEvent?.Invoke();
            }
            else
            {
                _weapons[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown(StaticValues.Fire) && _weapons != null)
        {
            _weapons[activeWeaponIndex].Shoot();
            UpdateAmmoEvent.Invoke();

            //Check it AutoRealod Is there
            if (_weapons[activeWeaponIndex].GetBulletsCount() <= 0)
                if (_weapons[activeWeaponIndex].GetFireMode() == FireMode.AUTO)
                {
                    _weapons[activeWeaponIndex].Reload();
                }
        }

        if (Input.GetKeyDown(KeyCode.R) && _weapons != null)
        {
            _weapons[activeWeaponIndex].Reload();
            UpdateAmmoEvent.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.B) && _weapons != null)
        {
            _weapons[activeWeaponIndex].ChangeFireMode();
            GetFireModeEvent?.Invoke();
        }
    }

    void GetRemainingAmmo()
    {
        ammoCount.text = _weapons[activeWeaponIndex].GetBulletsCount().ToString();
        Debug.Log("AmmoCount" + _weapons[activeWeaponIndex].GetBulletsCount());
    }

    void CurrentFireMode()
    {
        fireModeTxt.text = _weapons[activeWeaponIndex].GetFireMode().ToString();
    }
}
