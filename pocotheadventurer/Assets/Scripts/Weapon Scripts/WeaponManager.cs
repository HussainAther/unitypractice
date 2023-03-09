using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] guns;
    private int currentGun;

    private void Start()
    {
        DeactivateAllGuns();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void ActivateGun(int newGunId)
    {
        guns[currentGun].SetActive(false);
        guns[newGunId].SetActive(true);
        currentGun = newGunId;
    }

    private void DeactivateAllGuns()
    {
        foreach (GameObject gun in guns)
            gun.SetActive(false);
    }


}
