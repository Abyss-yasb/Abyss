using UnityEngine;
using System.Collections;

public class C01_Player : MonoBehaviour {
	private	CharacterController charaCon;		// キャラクターコンポーネント用の変数
	private Vector3	move = Vector3.zero;		// キャラ移動量.
	private float		speed = 5.0f;

	private const float	GRAVITY = 30f;	
	private float		jumpPower = 10.0f;		// 跳躍力.

	private float		rotationSpeed = 120.0f;	// プレイヤーの回転速度

	public GameObject targetEnemy = null;			// ターゲット格納用の変数.

	public	GameObject	prefab_hitEffect1;

	void Start(){
		charaCon = GetComponent< CharacterController >();
	}

	void Update () {
		playerMove_1rdParson();
		setTargetEnemy();							// ターゲット情報を取得
		attack_LeftClick();							// 左クリックで敵を攻撃
	}
	
	// ■■■１人称視点の移動■■■
	private void playerMove_1rdParson(){
		// ▼▼▼移動量の取得▼▼▼
		float y = move.y;
		move = new Vector3(0.0f , 0.0f , Input.GetAxis("Vertical"));		// 上下のキー入力を取得し、移動量に代入.
		move = transform.TransformDirection(move);							// プレイヤー基準の移動方向へ修正する.
		move *= speed;				// 移動速度を乗算.
		
		// ▼▼▼重力／ジャンプ処理▼▼▼
		move.y += y;
		if(charaCon.isGrounded){					// 地面に設置していたら
			if(Input.GetKeyDown(KeyCode.Space)){	// ジャンプ処理.
				move.y = jumpPower;
			}
		}
		move.y -=  GRAVITY * Time.deltaTime;	// 重力を代入.
		
		// ▼▼▼プレイヤーの向き変更▼▼▼
		Vector3 playerDir = new Vector3(Input.GetAxis("Horizontal") , 0.0f , 0.0f);		// 左右のキー入力を取得し、移動方向に代入.
		playerDir = transform.TransformDirection(playerDir);					// プレイヤー基準の向きたい方向へ修正する.
		if(playerDir.magnitude > 0.1f){
			Quaternion q = Quaternion.LookRotation(playerDir);			// 向きたい方角をQuaternionn型に直す .
			transform.rotation = Quaternion.RotateTowards(transform.rotation , q , rotationSpeed * Time.deltaTime);	// 向きを q に向けてじわ～っと変化させる.
		}
		
		// ▼▼▼移動処理▼▼▼
		charaCon.Move(move * Time.deltaTime);	// プレイヤー移動.
	}
	
	// ■■■３人称視点の移動■■■
	private void playerMove_3rdParson(){
		// ▼▼▼移動量の取得▼▼▼
		float y = move.y;
		move = new Vector3(Input.GetAxis("Horizontal") , 0.0f , Input.GetAxis("Vertical"));		// 左右上下のキー入力を取得し、移動量に代入.
		Vector3 playerDir = move;	// 移動方向を取得.
		move *= speed;				// 移動速度を乗算.
		
		// ▼▼▼重力／ジャンプ処理▼▼▼
		move.y += y;
		 if(charaCon.isGrounded){					// 地面に設置していたら
			if(Input.GetKeyDown(KeyCode.Space)){	// ジャンプ処理.
				move.y = jumpPower;
			}
		  }

		move.y -=  GRAVITY * Time.deltaTime;	// 重力を代入.
		
		// ▼▼▼プレイヤーの向き変更▼▼▼
		if(playerDir.magnitude > 0.1f){
			Quaternion q = Quaternion.LookRotation(playerDir);			// 向きたい方角をQuaternionn型に直す .
			transform.rotation = Quaternion.RotateTowards(transform.rotation , q , rotationSpeed * Time.deltaTime);	// 向きを q に向けてじわ～っと変化させる.
		}
		
		// ▼▼▼移動処理▼▼▼
		charaCon.Move(move * Time.deltaTime);	// プレイヤー移動.


	}

	// ■■■ターゲット情報を取得■■■
	private void setTargetEnemy(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);	// クリックした位置から真っ直ぐ奥に行く光線.
		RaycastHit hitInfo;												// ヒット情報を格納する変数を作成
		
		if(Physics.Raycast(ray , out hitInfo , 10)){			// カメラから距離10の光線を出し、もし何かに当たったら
			if(hitInfo.collider.gameObject.tag == "Enemy"){		// その当たったオブジェクトのタグ名が Enemy なら
				targetEnemy = hitInfo.collider.gameObject;		// 当たったオブジェクトを、参照。
				return;											// ターゲットが見つかったので、処理を抜ける
			}
		}
		
		targetEnemy = null;		// Enemyが見つからないなら、null(空)にする
	}


	// ■■■左クリックで敵を攻撃■■■
	private void attack_LeftClick(){
		if(Input.GetMouseButtonDown(0)){	// 左クリックが押されたら
			if(targetEnemy != null){		// 的に敵が入っていたら (変数が空じゃないなら)
				Instantiate(prefab_hitEffect1 , targetEnemy.transform.position , Quaternion.identity);		// エフェクト発生
				Destroy(targetEnemy);		// 敵を消滅させる。
			}
		}
	}

}
