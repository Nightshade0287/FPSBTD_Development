using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Health : MonoBehaviour
{
    public int health = 1;
    public GameObject[] ChildBloons;
    public GameObject dart;
    public PlayerUI playerUI;
    public AudioClip shootSound;
    private AudioSource audioSource;
    void Awake()
    {
        playerUI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUI>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {   
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Popped(-health);
        }
    }

    public void Popped(int damage)
    {
        foreach(GameObject child in ChildBloons)
        {
            GameObject bloon = Instantiate(child, transform.position, transform.rotation);
            Health bloonHealth = bloon.GetComponent<Health>();
            GetComponent<BloonMovement>().UpdateChild(bloon);
            bloonHealth.dart = dart;
            dart.GetComponent<DartBehavior>().bloonHitList.Add(bloon);
            bloonHealth.TakeDamage(damage);
        }
        playerUI.UpdateMoney(1);
        Destroy(gameObject);
    }
}
