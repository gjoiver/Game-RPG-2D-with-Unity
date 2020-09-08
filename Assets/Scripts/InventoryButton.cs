using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
  public enum ItemType { WEAPON=0, ITEM=1, ARMOR=2, RING=3}

    public int itemIdx;
    public ItemType type;
    
    public void ActiveBotton()
    {
        switch (type)
        {
            case ItemType.WEAPON:
                FindObjectOfType<WeaponManager>().ChangeWeapon(itemIdx);
                break;
            case ItemType.ITEM:
                //FindObjectOfType<WeaponManager>().ChangeWeapon(itemIdx);
                break;
            case ItemType.ARMOR:
                //FindObjectOfType<WeaponManager>().ChangeWeapon(itemIdx);
                break;
            case ItemType.RING:
                //FindObjectOfType<WeaponManager>().ChangeWeapon(itemIdx);
                break;
        }
    }
}
