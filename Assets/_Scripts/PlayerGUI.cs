using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {


	// ==========================================================================
	// Variables
	// ==========================================================================

	// Size of the textures
	public Vector2 barSize = new Vector2(240, 40);

	// Health variables
	public Vector2 healthPos = new Vector2(20, 20);
	public float healthBarDisplay = 1;
	public Texture2D healthBarEmpty;
	public Texture2D healthBarFull;
	
	// Hunger variables
	public Vector2 hungerPos = new Vector2(20, 60);
	public float hungerBarDisplay = 1;
	public Texture2D hungerBarEmpty;
	public Texture2D hungerBarFull;
	
	// Thirst variables
	public Vector2 thirstPos = new Vector2(20, 100);
	public float thirstBarDisplay = 1;
	public Texture2D thirstBarEmpty;
	public Texture2D thirstBarFull;
	
	// Stamina variables
	public Vector2 staminaPos = new Vector2(20, 140);
	public float staminaBarDisplay = 1;
	public Texture2D staminaBarEmpty;
	public Texture2D staminaBarFull;

	// Fall rate
	public int healthFallRate = 150;
	public int hungerFallRate = 150;
	public int thirstFallRate = 100;
	public int staminaFallRate = 35;

	// 
//	private CharacterMotor chrMotor;
	private CharacterController chrController;

	public bool canJump = false;
	public float jumpTimer = 0.7f;



	// ==========================================================================
	// Functions
	// ==========================================================================



	// Use this for initialization
	void Start () {
		chrController = GetComponent<CharacterController>();
	}

	void OnGUI() {

		// Health GUI
		// ==========================================================================
		GUI.BeginGroup(new Rect(healthPos.x, healthPos.y, barSize.x, barSize.y));
		GUI.Box(new Rect (0, 0, barSize.x, barSize.y), healthBarEmpty);

		GUI.BeginGroup(new Rect(0, 0, barSize.x * healthBarDisplay, barSize.y));
		GUI.Box(new Rect(0, 0, barSize.x, barSize.y), healthBarFull);

		GUI.EndGroup();
		GUI.EndGroup();
		
		// Hunger GUI
		// ==========================================================================
		GUI.BeginGroup(new Rect(hungerPos.x, hungerPos.y, barSize.x, barSize.y));
		GUI.Box(new Rect (0, 0, barSize.x, barSize.y), hungerBarEmpty);
		
		GUI.BeginGroup(new Rect(0, 0, barSize.x * hungerBarDisplay, barSize.y));
		GUI.Box(new Rect(0, 0, barSize.x, barSize.y), hungerBarFull);
		
		GUI.EndGroup();
		GUI.EndGroup();
		
		// Thirst GUI
		// ==========================================================================
		GUI.BeginGroup(new Rect(thirstPos.x, thirstPos.y, barSize.x, barSize.y));
		GUI.Box(new Rect (0, 0, barSize.x, barSize.y), thirstBarEmpty);
		
		GUI.BeginGroup(new Rect(0, 0, barSize.x * thirstBarDisplay, barSize.y));
		GUI.Box(new Rect(0, 0, barSize.x, barSize.y), thirstBarFull);
		
		GUI.EndGroup();
		GUI.EndGroup();
		
		// Stamina GUI
		// ==========================================================================
		GUI.BeginGroup(new Rect(staminaPos.x, staminaPos.y, barSize.x, barSize.y));
		GUI.Box(new Rect (0, 0, barSize.x, barSize.y), staminaBarEmpty);
		
		GUI.BeginGroup(new Rect(0, 0, barSize.x * staminaBarDisplay, barSize.y));
		GUI.Box(new Rect(0, 0, barSize.x, barSize.y), staminaBarFull);
		
		GUI.EndGroup();
		GUI.EndGroup();
	}
	
	// Update is called once per frame
	void Update () {

		// Health control
		// ==========================================================================
		if ((hungerBarDisplay <= 0) && (thirstBarDisplay <= 0)) {
			healthBarDisplay -= Time.deltaTime / healthFallRate * 2;
		} else {
			if ((hungerBarDisplay <= 0) || (thirstBarDisplay <= 0)) {
				healthBarDisplay -= Time.deltaTime / healthFallRate;
			}
		}

		if (healthBarDisplay <= 0) {
			CharacterDeath();
		}

		// Hunger control
		// ==========================================================================
		if (hungerBarDisplay >= 0) {
			hungerBarDisplay -= Time.deltaTime / hungerFallRate;
		}
		
		if (hungerBarDisplay <= 0) {		// Can't go lower than min
			hungerBarDisplay = 0;
		}
		
		if (hungerBarDisplay >= 1) {		// Can't go higher than max
			hungerBarDisplay = 1;
		}
		
		// Thirst control
		// ==========================================================================
		if (thirstBarDisplay >= 0) {
			thirstBarDisplay -= Time.deltaTime / thirstFallRate;
		}
		
		if (thirstBarDisplay <= 0) {		// Can't go lower than min
			thirstBarDisplay = 0;
		}
		
		if (thirstBarDisplay >= 1) {		// Can't go higher than max
			thirstBarDisplay = 1;
		}
		
		// Stamina control
		// ==========================================================================
		if ((chrController.velocity.magnitude > 0) && (Input.GetKey(KeyCode.LeftShift))) {
			staminaBarDisplay -= Time.deltaTime / staminaFallRate;
		} else {
			staminaBarDisplay += Time.deltaTime / staminaFallRate;
		}

		// Jumping
		if ((Input.GetKey(KeyCode.Space)) && (canJump == true)) {
			staminaBarDisplay -= 0.2f;
			StartCoroutine(Wait(1));
		}

		if (canJump == false) {
			jumpTimer -= Time.deltaTime;
		}

		if (jumpTimer <= 0) {
			canJump = true;
			jumpTimer = 0.7f;
		}
		
		if (staminaBarDisplay <= 0.2f) {
			canJump = false;
		} else {
			canJump = true;
		}
		
		if (staminaBarDisplay <= 0) {		// Can't go lower than min
			staminaBarDisplay = 0;
		}
		
		if (staminaBarDisplay >= 1) {		// Can't go higher than max
			staminaBarDisplay = 1;
		}
	}

	void CharacterDeath() {
		Application.LoadLevel ("SIMPLELEVEL");
	}

	IEnumerator Wait(int delay) {
		yield return new WaitForSeconds(delay);
	}
}
