using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform projectileSpawn;
    public Rigidbody projectilePrefab;
    public Transform shellEjectionPoint;
    public Rigidbody shell;
 
    public enum GunType { Semi, Auto };
    public GunType gunType;

    public float rpm;
    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    private void Start()
    {
        secondsBetweenShots = 60 / rpm;
    }

    public void Shoot()
    {
        if(CanShoot())
        {
            Rigidbody newProjectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
            newProjectile.velocity = projectileSpawn.forward * 30f; 

            nextPossibleShootTime = Time.time + secondsBetweenShots;

            Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity);

            newShell.AddForce(shellEjectionPoint.forward * Random.Range(150f, 200f)
                + projectileSpawn.forward * Random.Range(-10f, 10f));
        }
    }

    private bool CanShoot()
    {
        return Time.time >= nextPossibleShootTime;
    }
}
