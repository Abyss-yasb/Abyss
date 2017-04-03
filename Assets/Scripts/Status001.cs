using UnityEngine;
using System.Collections;

public class Status001 : MonoBehaviour {
	public int HP = 10;
	public int ATK = 1;
	
	// ■■■setter,getter■■■
	public void setHP(int hp){ HP = hp; }		// HPの値を設定
	public int getHP(){ return HP; }			// HPの値を返す
	public void setATK(int atk){ ATK = atk; }	// ATKの値を設定
	public int getATK(){ return ATK; }			// ATKの値を返す
	
	// ■■■ダメージ処理■■■
	public void damage(Status001 status){
		HP = Mathf.Max(0 , HP - status.getATK());		// 0 もしくは HP-敵ATK の値で、大きい方をHPに入れる (必ず0以上)
	}
}

