using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{
    private int currentWeapon = 0;

    private void Start()
    {
        this.transform.GetChild(currentWeapon).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeapon < 0)
        {
            currentWeapon = this.transform.childCount-1;
        }
        else if(currentWeapon > this.transform.childCount-1)
        {
            currentWeapon = 0;
        }

        if(this.transform.GetChild(currentWeapon).gameObject.activeSelf == false)
        {
            this.transform.GetChild(currentWeapon).gameObject.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            this.transform.GetChild(currentWeapon).gameObject.SetActive(false);
            currentWeapon++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            this.transform.GetChild(currentWeapon).gameObject.SetActive(false);
            currentWeapon--;
        }

    }
}
