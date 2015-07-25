#pragma strict

function Start () {
		var mesh : Mesh = GetComponent(MeshFilter).mesh;
		mesh.RecalculateNormals();
	}
	
		// Rotate the normals by speed every frame
var speed = 100.0;
function Update () {
		var mesh : Mesh = GetComponent(MeshFilter).mesh;
		var normals : Vector3[] = mesh.normals;
		var rotation : Quaternion = Quaternion.AngleAxis(Time.deltaTime * speed, Vector3.up);
		for (var i = 0; i < normals.Length; i++)
			normals[i] = rotation * normals[i];
		mesh.normals = normals;
	}