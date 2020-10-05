using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    //public delegate void OnChangeWeapon(Weapon newWeapon);
    //public OnChangeWeapon onChangeWeaponCallback;

    //public delegate void OnWeaponUsed(int currentAmmo, int totalAmmo);
    //public OnWeaponUsed onWeaponUsedCallback;

    //public Weapon defaultWeapon;
    //public int currentAmmo;
    //public int totalAmmo;
    //public ParticleSystem muzzleFlash;

    //[SerializeField]
    //Weapon currentWeapon;    
    //Animator m_Animator;
    //GameObject currentWeaponPrefab;


    //#region Singleton
    //public static WeaponManager instance;

    //void Awake()
    //{
    //    instance = this;
    //    currentAmmo = defaultWeapon.weaponAmmoRound;
    //    totalAmmo = defaultWeapon.weaponBaseAmmo;
    //    currentWeapon = defaultWeapon;

        
    //}

    //#endregion

    //private void Start()
    //{
    //    m_Animator = PlayerManager.instance.player.GetComponent<Animator>();
    //    //currentWeaponPrefab = Instantiate(defaultWeapon.weaponPrefab, PlayerManager.instance.rightHand.transform);

    //    //muzzleFlash = PlayerManager.instance.rightHand.GetComponentInChildren<ParticleSystem>();
    //}

    //public void ChangeWeapon(Weapon newWeapon)
    //{
    //    if(currentWeapon == newWeapon && currentWeapon != defaultWeapon)
    //    {
    //        totalAmmo += newWeapon.weaponBaseAmmo;
    //        if(currentAmmo == 0)
    //        {
    //            StartCoroutine(Reload());
    //        }

    //        if(onWeaponUsedCallback != null)
    //        {
    //            onWeaponUsedCallback.Invoke(currentAmmo, totalAmmo);
    //        }
    //    }
    //    else if(currentWeapon != newWeapon)
    //    {
    //        currentWeapon = newWeapon;
    //        totalAmmo = newWeapon.weaponBaseAmmo;
    //        currentAmmo = newWeapon.weaponAmmoRound;

    //        if (onChangeWeaponCallback != null)
    //        {
    //            onChangeWeaponCallback.Invoke(currentWeapon);
    //        }

    //        if (onWeaponUsedCallback != null)
    //        {
    //            onWeaponUsedCallback.Invoke(currentAmmo, totalAmmo);
    //        }

    //        StartCoroutine(ChangeWeapon());

    //    } 
    //    else if(currentWeapon == defaultWeapon && newWeapon == defaultWeapon)
    //    {
    //        currentWeapon = newWeapon;
    //        totalAmmo = newWeapon.weaponBaseAmmo;
    //        currentAmmo = newWeapon.weaponAmmoRound;

    //        if (onWeaponUsedCallback != null)
    //        {
    //            onWeaponUsedCallback.Invoke(currentAmmo, totalAmmo);
    //        }
    //    }
    //}

    //IEnumerator ChangeWeapon()
    //{
    //    m_Animator.SetBool("changedWeapon", true);

    //    IKController.instance.DeactivateIK();

    //    Destroy(currentWeaponPrefab);
    //    currentWeaponPrefab = Instantiate(currentWeapon.weaponPrefab, PlayerManager.instance.rightHand.transform);
    //    muzzleFlash = PlayerManager.instance.rightHand.GetComponentInChildren<ParticleSystem>();

    //    yield return new WaitForSeconds(3.533f);
        
    //    IKController.instance.ActivateIK();

    //    m_Animator.SetBool("changedWeapon", false);
    //}

    //public void Shoot()
    //{
    //    currentAmmo--;
    //    if (currentAmmo <= 0 && totalAmmo > 0)
    //    {
    //        StartCoroutine(Reload());
    //    }

    //    if (onWeaponUsedCallback != null)
    //    {
    //        onWeaponUsedCallback.Invoke(currentAmmo, totalAmmo);
    //    }
    //}

    //IEnumerator Reload()
    //{
    //    IKController.instance.DeactivateIK();

    //    m_Animator.SetBool("isReloading", true);

    //    float clipTime = 3.300f;

    //    Debug.Log("Reloading");
    //    yield return new WaitForSeconds(clipTime);
    //    Debug.Log("End reloading");
    //    m_Animator.SetBool("isReloading", false);
    //    IKController.instance.ActivateIK();

    //    if (totalAmmo >= currentWeapon.weaponAmmoRound)
    //    {
    //        currentAmmo = currentWeapon.weaponAmmoRound;
    //        totalAmmo -= currentWeapon.weaponAmmoRound;
    //    }
    //    else if (totalAmmo < currentWeapon.weaponAmmoRound)
    //    {
    //        currentAmmo = totalAmmo;
    //        totalAmmo = 0; 
    //    }

    //    if (onWeaponUsedCallback != null)
    //    {
    //        onWeaponUsedCallback.Invoke(currentAmmo, totalAmmo);
    //    }
    //}


}
