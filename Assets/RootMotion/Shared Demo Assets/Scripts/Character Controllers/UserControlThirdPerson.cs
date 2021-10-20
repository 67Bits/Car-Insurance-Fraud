using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RootMotion.Demos {

	/// <summary>
	/// User input for a third person character controller.
	/// </summary>
	public class UserControlThirdPerson : MonoBehaviour {

		public Joystick joystick;
		public ControlNivel control_nivel;

		private CharacterMovement character_movement = null;

		// Input state
		public struct State {
			public Vector3 move;
			public Vector3 lookPos;
			public bool crouch;
			public bool jump;
			public int actionIndex;
		}

		public bool walkByDefault;        // toggle for walking state
		public bool canCrouch = true;
		public bool canJump = true;

		public State state = new State();			// The current state of the user input

		protected Transform cam;                    // A reference to the main camera in the scenes transform

		protected virtual void Start () {
			// get the transform of the main camera
			cam = Camera.main.transform;
			joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
			control_nivel = GameObject.FindGameObjectWithTag("controlnivel").GetComponent<ControlNivel>();

			character_movement = GameObject.FindGameObjectWithTag("MyCharacterMovement").GetComponent<CharacterMovement>();
		}


		protected virtual void Update () {
			// read inputs
			state.crouch = canCrouch && Input.GetKey(KeyCode.C);
			state.jump = canJump && Input.GetButton("Jump");

			//float h = Input.GetAxisRaw("Horizontal");
			//float v = Input.GetAxisRaw("Vertical");		
			float h = 0;
			float v = 0;
			if (joystick.Vertical > 0.2f || Input.GetKey(KeyCode.W))
			{
				if (!control_nivel.bool_win)
				{
					v = joystick.Vertical;
					character_movement.correr();
					//v = 1;
				}
			}
			if (joystick.Vertical < -0.2f || Input.GetKey(KeyCode.S))
			{
				if (!control_nivel.bool_win)
				{
					v = joystick.Vertical;
					character_movement.correr();
					//v = -1;
				}
			}
			if (joystick.Horizontal < -0.2f || Input.GetKey(KeyCode.A))
			{
				if (!control_nivel.bool_win)
				{
					h = joystick.Horizontal;
					character_movement.correr();
					//h = -1;
				}
			}
			if (joystick.Horizontal > 0.2f || Input.GetKey(KeyCode.D))
			{
				if (!control_nivel.bool_win)
				{
					h = joystick.Horizontal;					
					character_movement.correr();
					//h = 1;
				}
			}

			// calculate move direction
			Vector3 move = cam.rotation * new Vector3(h, 0f, v).normalized;

			// Flatten move vector to the character.up plane
			if (move != Vector3.zero) {
				Vector3 normal = transform.up;
				Vector3.OrthoNormalize(ref normal, ref move);
				state.move = move;
			} 
			else state.move = Vector3.zero;

			bool walkToggle = Input.GetKey(KeyCode.LeftShift);

			// We select appropriate speed based on whether we're walking by default, and whether the walk/run toggle button is pressed:
			float walkMultiplier = (walkByDefault ? walkToggle ? 1 : 0.5f : walkToggle ? 0.5f : 1);

			state.move *= walkMultiplier;
			
			// calculate the head look target position
			state.lookPos = transform.position + cam.forward * 100f;

		}
	}
}

