using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class projectileStats : ScriptableObject
{
    public float shootRate;
    public float cooldownTimer;
    public int proDist;
    public int shootDamage;
    public string descript;
    //public Vector3 proVelocity;
    public float proSpeed;
    public float arcRange;
    public float destroyTime;
    public int DOTdamage;
    public float DOTtime;
    public float timeBetweenTicks;
    public bool stun;
    public bool isAoe;
    public float aoeRadius;
    public float slowDown;
    public float statusEffectTime_poison;
    public float statusEffectTime_slow;
    public float statusEffectTime_stun;
    public string name;
    public string type;
    //public int proCount;
    
    public GameObject projectile;
    public GameObject impactEffect;
    public GameObject muzzle;
    public AudioClip shotSound;
    //public Animator anime;
}