using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private Transform towerTop;
    private Transform target;
    
    void Start()
    {
        target = FindObjectOfType<MoverForSimplePaths>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        //TODO restrict to y rotation?
        towerTop.LookAt(target);
    }
}
