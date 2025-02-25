using UnityEngine;
using System.Collections;
using System;
using Unity.VisualScripting;
public class WeaponExplode : Weapon
{

    public GameObject ExplosionPrefab;
    public GameObject ExplosionRange;
    public GameObject ExplosionCharge;
    public GameObject ExplosionFX;
    [SerializeField] private float _chargeDuration = 2;
    private Vector3  newScale;
    private float damageRadius;
    private bool _isCharging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newScale = ExplosionCharge.transform.localScale;
        damageRadius = ExplosionRange.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isCharging)
        {
            newScale.x = Mathf.Lerp(newScale.x, ExplosionRange.transform.localScale.x, _chargeDuration*Time.deltaTime);
            newScale.z = Mathf.Lerp(newScale.z, ExplosionRange.transform.localScale.z, _chargeDuration*Time.deltaTime);
            ExplosionCharge.transform.localScale = newScale;
        }
    }

    protected override void Attack(Vector3 aimPosition, GameObject instigator, int team)
{
    base.Attack(aimPosition, instigator, team);

    CreateChargeEffect();

    // wait for charge duration to finish
    StartCoroutine(ExplosionAfterCharge(instigator));


}
private IEnumerator ExplosionAfterCharge(GameObject instigator)
{
    // Wait for charge duration
    yield return new WaitForSeconds(_chargeDuration);
   

    // Check for damage logic using OverlapSphere
    Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, damageRadius);
    foreach (var hitCollider in hitColliders)
    {
        // Apply damage to the player and self
        if (hitCollider.TryGetComponent(out Health health))
        {
            health.Damage(new DamageInfo(Damage, hitCollider.gameObject, gameObject, instigator, DamageType));
        }
    }
    StartCoroutine(StartExplosionVFX());



}
private IEnumerator StartExplosionVFX()
{
     ExplosionFX.SetActive(true);
    yield return new WaitForSeconds(_chargeDuration);
    ExplosionFX.SetActive(false);

}
private void CreateChargeEffect()
{
    ExplosionRange.SetActive(true);
    ExplosionCharge.SetActive(true);
    _isCharging = true;

}


}