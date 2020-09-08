using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    private List<GameObject> weapons;

    public int activeWeapon;
    // Start is called before the first frame update
    public List<GameObject> GetAllWeapons()
    {
        return weapons;
    }

    void Start()
    {
        weapons = new List<GameObject>();
        foreach(Transform weapon in transform)
        {
            weapons.Add(weapon.gameObject);
        }
        for(int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(i == activeWeapon);
        }
    }
    //metodo para cmbiar de arma
    public void ChangeWeapon(int newWeapon)
    {
        //se desactiva la arma actual
        weapons[activeWeapon].SetActive(false);
        //se activa la nueva arma
        weapons[newWeapon].SetActive(true);
        //la nueva arma se convierte en la arma actual
        activeWeapon = newWeapon;

        FindObjectOfType<UIManager>().ChangeAvatarImage(weapons[activeWeapon].GetComponent<SpriteRenderer>().sprite);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
