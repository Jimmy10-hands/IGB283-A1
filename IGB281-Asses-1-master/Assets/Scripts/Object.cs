using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour {
		public Material material;
		//public float angle = 1f;
		private Mesh mesh;
		public GameObject GameObject;
		//public GameObject Square;
		//public GameObject Square2;
 		private bool overLap1 = true;
 		private bool colourSwap = true;
 		private bool doScale = true;
 		private float scaler = 1.5f;
 		public float speedMultiplier;

		public Slider slider;
		public Slider angle;
		
 		private Vector3[] globalVert;
 		private Vector3 bounder1 = new Vector3();
 		private Vector3 bounder2 = new Vector3();

		public BoundaryPoints Square1; //test
		public BoundaryPoints Square2; //test

	// Use this for initialization
	void Start () {
	// Add a MeshFilter and MeshRenderer to the Empty GameObject
			gameObject.AddComponent<MeshFilter>();
			gameObject.AddComponent<MeshRenderer>();
	// Get the Mesh from the MeshFilter
			mesh = GetComponent<MeshFilter>().mesh;
	// Set the material to the material we have selected
			GetComponent<MeshRenderer>().material = material;
	// Clear all vertex and index data from the mesh
			mesh.Clear();
	// Create a triangle with points at (0, 0, 0), (0, 1, 0) and (1, 1, 0)
			mesh.vertices = new Vector3[] {
				new Vector3(0, 0, 0),
				new Vector3(0, 1, 0),
				new Vector3(1, 1, 0),
				new Vector3(-1, 1, 0),
				new Vector3(-2, 0, 0),
				new Vector3(2, 0, 0),
				new Vector3(2, 1, 0),
				new Vector3(-2, 1, 0),
				new Vector3(-3, 1, 0),
				new Vector3(3, 1, 0),
				new Vector3(-4, 1, 0),
				new Vector3(4, 1, 0),
				new Vector3(-3, 2, 0),
				new Vector3(3, 2, 0),
				new Vector3(-1, 2, 0),
				new Vector3(1, 2, 0),
				new Vector3(-1, 3, 0),
				new Vector3(1, 3, 0),
				new Vector3(0, 4, 0),
				new Vector3(0, 1, 0)
			};
	// Set the colour of the triangle
			mesh.colors = new Color[] {
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f),
				new Color(0.8f, 0.3f, 0.3f, 1.0f)
			};
	// Set vertex indicies
			mesh.triangles = new int[]{0, 1, 2, 3, 0, 1, 3, 4, 7, 2, 5, 6, 8, 9, 13, 8, 13, 12, 8, 10, 12, 10, 11, 13, 12, 15, 16, 16, 14, 17, 17, 15, 14, 15, 13, 17, 17, 18, 16};

			globalVert = mesh.vertices;

		}

		Matrix3x3 Scale (float scaleFactor) {
			
			Matrix3x3 scaleMatrix = new Matrix3x3();

			scaleMatrix.SetRow(0, new Vector3(scaleFactor, 0.0f, 0.0f));
			scaleMatrix.SetRow(1, new Vector3(0.0f, scaleFactor, 0.0f));
			scaleMatrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

			return scaleMatrix;

		}

		Matrix3x3 rotateRestrain (Vector3 offset) {

			Matrix3x3 restMatrix = new Matrix3x3();

			restMatrix.SetRow(0, new Vector3(1.0f, 0.0f, offset.x));
			restMatrix.SetRow(1, new Vector3(0.0f, 1.0f, offset.y));
			restMatrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

			return restMatrix;

		}

		Matrix3x3 Rotate (float angle) {

			Matrix3x3 matrix = new Matrix3x3();

			matrix.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f));
			matrix.SetRow(1, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
			matrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

			return matrix;
		}

		Matrix3x3 Translate () {

		//bounder1 = Square.transform.position;
			bounder1 = Square1.mesh.vertices[4]; //test
			bounder2 = Square2.mesh.vertices[4];

			Vector3 vectorCalc1 = -mesh.vertices[0];

			float dist = Vector3.Distance(mesh.vertices[0], bounder1);
			float dist2 = Vector3.Distance(mesh.vertices[0], bounder2);

			
			if (dist < 2.0f) {
				overLap1 = false;
				doScale = true;
			}
			else if (dist2 < 2.0f) {
				overLap1 = true;
				doScale = true;
			}	

			Matrix3x3 tranMatrix = new Matrix3x3();
			speedMultiplier = slider.value;

			if (overLap1 == true) {
				float xDistance = ((bounder1.x + vectorCalc1.x) * Time.deltaTime) * speedMultiplier;
				float yDistance = ((bounder1.y + vectorCalc1.y) * Time.deltaTime) * speedMultiplier;

				tranMatrix.SetRow(0, new Vector3(1.0f, 0.0f, xDistance));
				tranMatrix.SetRow(1, new Vector3(0.0f, 1.0f, yDistance));
				tranMatrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));
			}

			else if (overLap1 == false) {
				float xDistance = ((bounder2.x + vectorCalc1.x) * Time.deltaTime) * speedMultiplier;
				float yDistance = ((bounder2.y + vectorCalc1.y) * Time.deltaTime) * speedMultiplier;

				tranMatrix.SetRow(0, new Vector3(1.0f, 0.0f, xDistance));
				tranMatrix.SetRow(1, new Vector3(0.0f, 1.0f, yDistance));
				tranMatrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));
			}
			return tranMatrix;
		}

		void changeColour () {
			Color red = new Color(0.8f, 0.3f, 0.3f, 1.0f);
			Color blue = new Color(0.3f, 0.3f, 0.8f, 1.0f);
			Vector3 origin = mesh.vertices[0];
			Debug.Log(origin[0]);

			if (origin[0] <= 0)
			{
				mesh.colors = new Color[] {
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue,
					blue
				};
			}
			else
			{
				mesh.colors = new Color[] {
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red,
					red
				};
			}

		/*
			bounder1 = Square1.transform.position;
			bounder2 = Square2.transform.position;
			Vector3 vecBetween = new Vector3();
			Color red = new Color(0.8f, 0.3f, 0.3f, 1.0f);
			Color blue = new Color(0.3f, 0.3f, 0.8f, 1.0f);
			Color[] newcolor = mesh.colors;

			Vector3 midPointMath = bounder1 + bounder2;

			Vector3 midPoint = midPointMath / 2;

			float colDist = Vector3.Distance(mesh.vertices[0], midPoint);

			Debug.Log(midPointMath.y);
			Debug.Log(midPointMath.x);
			if (midPoint.x == 0 && midPoint.y == 0){

				if (bounder1.x < 0 || bounder1.x < 0){
					vecBetween = -bounder1 + bounder2;
				}
				else if (bounder2.x < 0 || bounder2.x < 0){
					vecBetween = -bounder2 + bounder1;
				}

				if (vecBetween.x > vecBetween.y){
					if (mesh.vertices[0].x > 0 && colourSwap == true) {
						colourSwap = false;
					}
					else if (mesh.vertices[0].x < 0 && colourSwap == false) {
						colourSwap = true;
					}
				}

				else if (vecBetween.y > vecBetween.x){
					if (mesh.vertices[0].y > 0 && colourSwap == true) {
						colourSwap = false;
					}
					else if (mesh.vertices[0].y < 0 && colourSwap == false) {
						colourSwap = true;
					}
				}
			}

			if (midPointMath.x > midPointMath.y){
				if (mesh.vertices[0].x > midPoint.x && colourSwap == true) {
					colourSwap = false;
				}
				else if (mesh.vertices[0].x < midPoint.x && colourSwap == false) {
					colourSwap = true;
				}
			}
			else if (midPointMath.y > midPointMath.x) {
				if (mesh.vertices[0].y > midPoint.y && colourSwap == true) {
					colourSwap = false;
				}
				else if (mesh.vertices[0].y < midPoint.y && colourSwap == false) {
					colourSwap = true;
				}
			}

			if (colourSwap == true) {

				for (int i = 0; i < mesh.colors.Length; i++){
					newcolor[i] = red;
					mesh.colors = newcolor;
				}

			}
			else if (colourSwap == false) {

				for (int i = 0; i < mesh.colors.Length; i++){
					newcolor[i] = blue;
					mesh.colors = newcolor;
				}

			}
		 */
		}


	    // Update is called once per frame
	    void Update()
	    {

	    	Vector3 centre = mesh.vertices[19];
	    	Vector3[] vertices = globalVert;

	    	Matrix3x3 T = Translate();

	    	for (int i = 0; i < vertices.Length; i++) {
	    		vertices[i] = T.MultiplyPoint(vertices[i]);
	    	}

	    	mesh.vertices = vertices;
	    	mesh.RecalculateBounds();

	    	changeColour();

	    	Matrix3x3 RR = rotateRestrain(centre);

			
	    	Matrix3x3 R = Rotate(angle.value * Time.deltaTime);
	    	Matrix3x3 RR2 = rotateRestrain(-centre);
	    	Matrix3x3 M = RR * R * RR2;

	    	for (int i = 0; i < vertices.Length; i++) {
	    		vertices[i] = M.MultiplyPoint(vertices[i]);
	    	}

	    	mesh.vertices = vertices;
	    	mesh.RecalculateBounds();

	    	float vertDist = Vector3.Distance(mesh.vertices[0], mesh.vertices[1]);

	    	if (vertDist >= 1.5f && scaler == 1.5f){
	    		doScale = false;
	    	}
	    	else if (vertDist <= 0.5f && scaler == 0.5f){
	    		doScale = false;
	    	}
	    	else {
	    		doScale = true;
	    	}

	    	if (mesh.vertices[0].x > 0) {
	    		scaler = 1.5f;
	    	}
	    	else if (mesh.vertices[0].x < 0) {
	    		scaler = 0.5f;
	    	}

	    	if (doScale == true) {
	    		Matrix3x3 S = Scale(scaler);

	    		for (int i = 0; i < vertices.Length; i++) {
	    			vertices[i] = S.MultiplyPoint(vertices[i]);
	    		}

	    		mesh.vertices = vertices;
	    		mesh.RecalculateBounds();

	    	}

	    }
	}
