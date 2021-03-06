using UnityEngine;
using System.Collections;

// Overture = 0 & -180
public class LightController : MonoBehaviour {

	public Gradient nightDayColor;

	public float maxIntensity = 1.5f;
	public float minIntensity = 0f;
	public float minPoint = -0.2f;

	public float maxAmbient = 1f;
	public float minAmbient = 0f;
	public float minAmbietPoint = -0.2f;

	public Gradient nightDayFogColor;
	public AnimationCurve fogDensityCurve;
	public float fogScale = 1f;

	public float dayAtmosphereThickness = 0.4f;
	public float nightAtmosphereThickness = 0.87f;

	public Vector3 dayRotateSpeed;
	public Vector3 nightRotateSpeed;

	float skySpeed = 1f;

	Light mainLight;
	Skybox sky;
	Material skyMat;

	private void Awake () {
		mainLight = GetComponent<Light> ();
		skyMat = RenderSettings.skybox;
	}
	
	private void Update() {
		float tRange = 1 - minPoint;
		float dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
		float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

		mainLight.intensity = i;

		tRange = 1 - minAmbietPoint;
		dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minAmbietPoint) / tRange);
		i = ((maxAmbient - minAmbient) * dot) + minAmbient;
		RenderSettings.ambientIntensity = i;

		mainLight.color = nightDayColor.Evaluate (dot);
		RenderSettings.ambientLight = mainLight.color;

		RenderSettings.fogColor = nightDayFogColor.Evaluate (dot);
		RenderSettings.fogDensity = fogDensityCurve.Evaluate (dot) * fogScale;

		i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
		skyMat.SetFloat ("_AtmosphereThickness", i);

		if (dot > 0)
			transform.Rotate (dayRotateSpeed * Time.deltaTime * skySpeed);
		else
			transform.Rotate (nightRotateSpeed * Time.deltaTime * skySpeed);
	}
}
