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


    //no ammo sounds
    public AudioClip handgunNoAmmo;
    public AudioClip shotgunNoAmmo;
    public AudioClip rocketNoAmmo;
    public AudioClip mortalNoAmmo;
    public AudioClip droneNoAmmo;

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
    private bool noAmmoHandGun = false;

    private bool shotgunplaysound = false;
    private bool noAmmoShotGun = false;

    private bool rocketplaysound = false;
    private bool noAmmoRocket = false;


    private bool TNTplaysound = false;
    private bool noAmmoTNT = false;

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
        Debug.Log(handgun.ammo);
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



        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            // Scroll up
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
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

            if (handgun.noammosound== true)
            {
                if(noAmmoHandGun == false)
                {
                    audioSource.PlayOneShot(handgunNoAmmo);
                    noAmmoHandGun = true;
                }
            }
            else if (handgun.noammosound == false)
            {
                noAmmoHandGun = false;
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
            if (shotgun.playsound == true)
            {
                //Debug.Log(shotgunplaysound);

                if (shotgunplaysound == false)
                {
                    //Debug.Log("Play");
                    audioSource.PlayOneShot(shotgunSound);
                    shotgunplaysound = true;
                }


            }
            else if (shotgun.playsound == false)
            {
             
                shotgunplaysound = false;
            }


            if (shotgun.noammosound == true)
            {
                if (noAmmoShotGun == false)
                {
                    audioSource.PlayOneShot(handgunNoAmmo);
                    noAmmoShotGun = true;
                }
            }
            else if (shotgun.noammosound == false)
            {
                noAmmoShotGun = false;
            }



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
            if (rocketLauncher.playsound == true)
            {
                //Debug.Log(shotgunplaysound);

                if (rocketplaysound == false)
                {
                    //Debug.Log("Play");
                    audioSource.PlayOneShot(RocketSound);
                    rocketplaysound = true;
                }


            }
            else if (rocketLauncher.playsound == false)
            {
                rocketplaysound = false;
            }



            if (rocketLauncher.noammosound == true)
            {
                if (noAmmoRocket == false)
                {
                    audioSource.PlayOneShot(handgunNoAmmo);
                    noAmmoRocket = true;
                }
            }
            else if (rocketLauncher.noammosound == false)
            {
                noAmmoRocket = false;
            }


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

            if (TNTLauncher.playsound == true)
            {
                //Debug.Log(shotgunplaysound);

                if (TNTplaysound == false)
                {
                    //Debug.Log("Play");
                    audioSource.PlayOneShot(mortalSound);
                    TNTplaysound = true;
                }


            }
            else if (TNTLauncher.playsound == false)
            {
                TNTplaysound = false;
            }


            if (TNTLauncher.noammosound == true)
            {
                if (noAmmoTNT == false)
                {
                    audioSource.PlayOneShot(handgunNoAmmo);
                    noAmmoTNT = true;
                }
            }
            else if (TNTLauncher.noammosound == false)
            {
                noAmmoTNT = false;
            }





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


        }

        if (handgun.ammo >= 60)
        {
            if(handgunplayed == false)
            {
                audioSource.PlayOneShot(handgunready);
                handgunplayed = true;
            }

        }
        else if (handgun.ammo < 60)
        {
            handgunplayed = false;
        }

        if (shotgun.ammo >= 5)
        {
            if (shotgunplayed == false)
            {
                audioSource.PlayOneShot(shotgunready);
                shotgunplayed = true;
            }

        }
        else if (shotgun.ammo < 5)
        {
            shotgunplayed = false;
        }

        if (rocketLauncher.ammo >= 3)
        {
            if (rocketplayed == false)
            {
                audioSource.PlayOneShot(rocketready);
                rocketplayed = true;
            }

        }
        else if (rocketLauncher.ammo < 3)
        {
            rocketplayed = false;
        }

        if (TNTLauncher.ammo >= 6)
        {
            if (mortalplayed == false)
            {
                audioSource.PlayOneShot(mortalready);
                mortalplayed = true;
            }

        }
        else if (rocketLauncher.ammo < 6)
        {
            mortalplayed = false;
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
