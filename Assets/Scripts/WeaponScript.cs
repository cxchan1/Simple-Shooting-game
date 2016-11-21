using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	public Transform shotPrefab;

	// Cooldown in seconds between two shots
	public float shootingRate = 0.25f;

	private float shootCooldown;

	void Start()
	{
		shootCooldown = 0f;
	}

	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}
		
	// Create a new projectile if possible
	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;

			// Create a new shot
			var shotTransform = Instantiate(shotPrefab) as Transform;

			// Assign position
			shotTransform.position = transform.position;

			// The is enemy property
			BulletScript shot = shotTransform.gameObject.GetComponent<BulletScript>();
			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
			}

			// Make the weapon shot always towards it
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
			if (move != null)
			{
				move.direction = this.transform.right; // towards in 2D space is the right of the sprite
			}
		}
	}
		
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}
