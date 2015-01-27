using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GyroRot : MonoBehaviour {

	void Update () {
	
		//デバイスの傾きを取得     
		//iOSはテーブルに置いた状態でattitudeが回転なし
		//回転軸と向きをunity世界に変換
		var gyroRH = Input.gyro.attitude;
		var gyro = new Quaternion(-gyroRH.x, -gyroRH.z, -gyroRH.y, gyroRH.w) * Quaternion.Euler(90f,0f,0f);

		transform.localRotation = gyro;
	}
}
