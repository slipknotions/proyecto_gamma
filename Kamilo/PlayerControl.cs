using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public float _speed = 5.0f;

	public GameObject groundCheck;
	public GameObject groundCheckFoodLeft;
	public GameObject groundCheckFoodRight;
	private bool _valorbool;

	public Transform _shootPos;
	public GameObject _rocketPrefab;

	private float _verticalSpeed;
	private Animator _animator;
	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
	}
	
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");

		Vector2 vectorMovimiento = new Vector2 (h, 0) * _speed;

		bool isOnGround = false;
		if (Physics2D.Linecast (transform.position, groundCheck.transform.position)) {
			isOnGround = true;
			_verticalSpeed = -0.5f;
		}

		Debug.Log ("isOnGround : "+isOnGround);
//		ChekaPiso (groundCheckFoodLeft);
//		Debug.Log ("groundCheckFoodLeft : " +  _valorbool);
//		ChekaPiso (groundCheckFoodRight);
//		Debug.Log ("groundCheckFoodRight : " +  _valorbool);
		bool _pI = ChekaPiso (groundCheckFoodLeft);
		bool _pD = ChekaPiso (groundCheckFoodRight);
		Debug.Log ("groundCheckFoodLeft : " +  _pI);
		Debug.Log ("groundCheckFoodRight : " +  _pD);


		if (Input.GetKeyDown (KeyCode.Space) && (isOnGround || _pD ||_pI) ) {
			_verticalSpeed = 15.0f;		
		}else {
			_verticalSpeed -= 0.5f;
		}





		vectorMovimiento += new Vector2 (0, _verticalSpeed);

		rigidbody2D.velocity = vectorMovimiento;
		//Debug.Log (rigidbody2D.velocity);
		Vector3 newScale = transform.localScale;

		if (h > 0) {
			newScale = new Vector3(1,1,1);
		}
		//si te mueves hacia la izquierda flipeamos el sprite
		//modificando la escala en X para que sea -1
		if( h < 0){
			newScale = new Vector3(-1,1,1);
		}
		transform.localScale = newScale;

	}


	bool ChekaPiso(GameObject GameObj){

		if (Physics2D.Linecast (transform.position, GameObj.transform.position)) {
			return true;
		}else{
			return  false;
		}
	}

	void Update(){
		SetAnimatorParameters ();

		if(Input.GetButtonDown("Fire1")){
			GameObject rocket = (GameObject)Instantiate(_rocketPrefab,_shootPos.position,Quaternion.identity);
			if(transform.localScale.x > 0)
				rocket.transform.right = Vector2.right;
			else
				rocket.transform.right = -Vector2.right;
		}
	}

	void SetAnimatorParameters(){
		float h = Input.GetAxis ("Horizontal");

		_animator.SetFloat ("speed", Mathf.Abs (h));
	}
}
