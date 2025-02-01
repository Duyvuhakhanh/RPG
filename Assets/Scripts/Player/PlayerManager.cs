using System;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    public Player player;
    public static PlayerManager instance; 
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
