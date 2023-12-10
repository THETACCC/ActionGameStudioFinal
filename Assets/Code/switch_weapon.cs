using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_weapon : MonoBehaviour
{
    public int selectedWeapon = 0;
    //sounds
    private AudioSource audioSource;

    public AudioClip handgunSound;
    public AudioClip shotgunSound;
    public AudioClip RocketSound;
    public AudioClip mortalSound;
    public AudioClip droneSound;


    //ready sounds
    public AudioClip handgunready;
    public AudioClip shotgunready;
    public AudioClip rocketready;
    public AudioClip mortalready;
    public AudioClip droneready;

    private bool handgunplayed = false;
    private bool shotgunplayed = false;
    private bool rocketplayed = false;
    private bool mortalplayed = false;
    private bool droneplayed = false;
    //getmanager
    public Weaponmanage manager;

    //getweaponammo
    public hand_gun handgun;
    public shot_gun shotgun;
    public rocket_launcher rocketLauncher;
    public TNT_launcher TNTLauncher;

    //sound stuff
    private bool playsound = false;

    void Start()
    {
        handgun.ammo = 60;
        shotgun.ammo = 5;
        rocketLauncher.ammo = 3;
        TNTLauncher.ammo = 6;
        audioSource = GetComponent<AudioSource>();
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedWeapon = 3;
        }



        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            // Scroll up
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            // Scroll down
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        








        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

        





        if (selectedWeapon == 0)
        {
            if (handgun.playsound == true)
            {
                if (playsound == false)
                {
                    audioSource.PlayOneShot(handgunSound);
                    playsound = true;
                }


            }
            else
            {
                playsound = false;
            }
            /*
            if(handgun.ammo <= 0.99)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;

                SelectWeapon();
            }
            */

            if (shotgun.ammo < 5)
            {
                shotgun.ammo += 2f * Time.deltaTime;
            }
            if (TNTLauncher.ammo < 6)
            {
                TNTLauncher.ammo += 0.66f * Time.deltaTime;
            }
            if (rocketLauncher.ammo < 3)
            {
                rocketLauncher.ammo += 0.5f * Time.deltaTime;
            }

            if (Input.GetButton("Fire1") && manager.weapons[0].nextFireTime == 0 && handgun.ammo >0)
            {
                handgunplayed = false;
            }
        }
        else if (selectedWeapon == 1)
        {
            /*
            if (shotgun.ammo <= 0.99)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;

                SelectWeapon();
            }
            */

            if (handgun.ammo < 60)
            {
                handgun.ammo += 12f * Time.deltaTime;
            }
            if (rocketLauncher.ammo < 3)
            {
                rocketLauncher.ammo += 0.5f * Time.deltaTime;
            }
            if (TNTLauncher.ammo < 6)
            {
                TNTLauncher.ammo += 0.66f * Time.deltaTime;
            }

            if (Input.GetButton("Fire1") && manager.weapons[1].nextFireTime == 0 && shotgun.ammo > 0)
            {
                audioSource.PlayOneShot(shotgunSound);
                shotgunplayed = false;
            }
        }
        else if (selectedWeapon == 2)
        {
            /*
            if (rocketLauncher.ammo <= 0.99)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;

                SelectWeapon();
            }
            */
            if (handgun.ammo < 60)
            {
                handgun.ammo += 12f * Time.deltaTime;
            }
            if (shotgun.ammo < 5)
            {
                shotgun.ammo += 2f * Time.deltaTime;
            }
            if (TNTLauncher.ammo < 6)
            {
                TNTLauncher.ammo += 0.66f * Time.deltaTime;
            }

            if (Input.GetButton("Fire1") && manager.weapons[2].nextFireTime == 0 && rocketLauncher.ammo > 0)
            {
                audioSource.PlayOneShot(RocketSound);
                rocketplayed = false;
            }
        }
        else if (selectedWeapon == 3)
        {
            /*
            if (TNTLauncher.ammo <= 0.99)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;

                SelectWeapon();
            }
            */
            if (handgun.ammo < 60)
            {
                handgun.ammo += 12f * Time.deltaTime;
            }
            if (shotgun.ammo < 5)
            {
                shotgun.ammo += 2f * Time.deltaTime;
            }
            if (rocketLauncher.ammo < 3)
            {
                rocketLauncher.ammo += 0.5f * Time.deltaTime;
            }

            if (Input.GetButton("Fire1") && manager.weapons[3].nextFireTime == 0 && TNTLauncher.ammo > 0)
            {
                audioSource.PlayOneShot(mortalSound);
                mortalplayed = false;
            }
        }

        if (manager.weapons[0].nextFireTime == 0  && !handgunplayed && !Input.GetButtonDown("Fire1"))
        {
            audioSource.PlayOneShot(handgunready);
            handgunplayed = true;
        }
        else if (manager.weapons[1].nextFireTime == 0  && !shotgunplayed && !Input.GetButtonDown("Fire1"))
        {
            audioSource.PlayOneShot(shotgunready);
            shotgunplayed = true;
        }
        else if (manager.weapons[2].nextFireTime == 0  && !rocketplayed && !Input.GetButtonDown("Fire1"))
        {
            audioSource.PlayOneShot(rocketready);
            rocketplayed = true;
        }
        else if (manager.weapons[3].nextFireTime == 0  && !mortalplayed && !Input.GetButtonDown("Fire1"))
        {
            audioSource.PlayOneShot(mortalready);
            mortalplayed = true;
        }
        else if (manager.weapons[4].nextFireTime == 0 && !droneplayed && !Input.GetButtonDown("Fire1"))
        {
            audioSource.PlayOneShot(droneready);
            droneplayed = true;
        }

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
