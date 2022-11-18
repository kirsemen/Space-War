using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponry : DefaultType
{
    public enum DamageType
    {
        pinpoint,
        penetrating
    }
    [HideInInspector] public Parametrs parametrs;

    public float damage = 0;
    public float distance = 30;
    public float SpeedGunRotation = 1;

    public float delay = 1;
    private float _delay = 0;

    public DamageType damageType = DamageType.pinpoint;

    private void Start()
    {
        parametrs = transform.parent.parent.GetComponent<Modules>().parametrs;
        _delay = Time.realtimeSinceStartup;
    }

    private void Update()
    {

        if (parametrs.target != null)
        {
            LookAtTarget();
            if (Vector3.Distance(parametrs.transform.position, parametrs.target.transform.GetChild(0).position) <= distance && Time.realtimeSinceStartup - _delay >= delay)
            {
                Shoot();
                _delay = Time.realtimeSinceStartup;

            }

        }
    }
    public void Shoot()
    {
        parametrs.target.GetDamage(damage, damageType);
    }

    public void LookAtTarget()
    {
        var _MeshCurentAngle = transform.GetChild(1).GetChild(0).rotation.eulerAngles;
        var targetAngle = Quaternion.LookRotation(parametrs.target.transform.GetChild(0).position - transform.GetChild(1).GetChild(0).position).eulerAngles;
        var deltaMeshAngle = (Quaternion.Inverse(Quaternion.Euler(_MeshCurentAngle)) * Quaternion.Euler(targetAngle)).eulerAngles;

        float x = deltaMeshAngle.x % 360 <= 180 ? deltaMeshAngle.x % 360 : -180 + deltaMeshAngle.x % 180;
        float y = deltaMeshAngle.y % 360 <= 180 ? deltaMeshAngle.y % 360 : -180 + deltaMeshAngle.y % 180;

        x = Mathf.Abs(x) < SpeedGunRotation ? x : SpeedGunRotation * (Mathf.Abs(x) / x);
        y = Mathf.Abs(y) < SpeedGunRotation ? y : SpeedGunRotation * (Mathf.Abs(y) / y);

        Vector3 eulerRotation = (transform.GetChild(1).GetChild(0).rotation * Quaternion.Euler(x, y, 0)).eulerAngles;
        transform.GetChild(1).GetChild(0).rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

}
