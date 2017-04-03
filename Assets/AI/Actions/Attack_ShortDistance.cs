using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class Attack_ShortDistance : RAINAction
{
	public Transform turret;
	public Transform muzzle;
	public GameObject bulletPrefab;
	
	private float attackInterval = 2f;
	private float turretRotationSmooth = 0.8f;
	private float lastAttackTime;
	
	private Transform player;
	
	private void Start()
	{
		// 始めにプレイヤーの位置を取得できるようにする
		player = GameObject.FindWithTag("person").transform;
	}
	
	private void Update()
	{
		// 砲台をプレイヤーの方向に向ける
		Quaternion targetRotation = Quaternion.LookRotation(player.position - turret.position);
		turret.rotation = Quaternion.Slerp(turret.rotation, targetRotation, Time.deltaTime * turretRotationSmooth);
		
		// 一定間隔で弾丸を発射する
		if (Time.time > lastAttackTime + attackInterval)
		{
			lastAttackTime = Time.time;
		}
	}
}