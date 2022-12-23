using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class LaserControl : MonoBehaviour
    {
        [SerializeField] private Color color = new Color(191 / 255, 36 / 225, 0);
        [SerializeField] private float colorIntensity = 4.3f;
        private float beamColorEnchance = 1;

        [SerializeField] private float maxLength = 100;
        [SerializeField] private float thickness = 9;
        [SerializeField] private float noiseScale = 3.14f;
        [SerializeField] private GameObject startVFX;
        [SerializeField] private GameObject endVFX;

        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();

            lineRenderer.material.color = color * colorIntensity;
            lineRenderer.material.SetFloat("_LaserThickness", thickness);
            lineRenderer.material.SetFloat("_LaserScale", noiseScale);

            /*
            Renderer startParticle = startVFX.transform.Find("Particles").GetComponent<Renderer>();
            Renderer startBeam = startVFX.transform.Find("Beam").GetComponent<Renderer>();
            Renderer endParticle = endVFX.transform.Find("Particles").GetComponent<Renderer>();
            Renderer endBeam = endVFX.transform.Find("Beam").GetComponent<Renderer>();

            startParticle.material.SetColor("_EmissionColor", color * (colorIntensity + beamColorEnchance));
            startBeam.material.SetColor("_EmissionColor", color * (colorIntensity + beamColorEnchance));
            endParticle.material.SetColor("_EmissionColor", color * (colorIntensity + beamColorEnchance));
            endBeam.material.SetColor("_EmissionColor", color * (colorIntensity + beamColorEnchance));
            */

            ParticleSystem[] particles = transform.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem p in particles) 
            {
                Renderer r = p.GetComponent<Renderer>();
                r.material.SetColor("_EmissionColor", color * (colorIntensity + beamColorEnchance));
            }
        }

        private void Start()
        {
            UpdateEndPosition();
        }

        private void Update()
        {
            UpdateEndPosition();
        }

        public void UpdatePosition(Vector2 startPosition, Vector2 direction)
        {
            direction = direction.normalized;
            transform.position = startPosition;
            float rotationZ = Mathf.Atan2(direction.y, direction.x); //radian
            transform.rotation = Quaternion.Euler(0, 0, rotationZ * Mathf.Rad2Deg);
        }

        private void UpdateEndPosition() 
        { 
            Vector2 startPosition = transform.position;
            float rotationZ = transform.rotation.eulerAngles.z; //degree
            rotationZ *= Mathf.Deg2Rad; //radian

            Vector2 direction = new Vector2(Mathf.Cos(rotationZ), Mathf.Sin(rotationZ));
            RaycastHit2D hit = Physics2D.Raycast(startPosition, direction.normalized);

            float length = maxLength;
            float laserEndRotation = 180;

            if (hit)
            {
                length = (hit.point - startPosition).magnitude;

                laserEndRotation = Vector2.Angle(direction, hit.normal);
                Debug.Log(laserEndRotation);
            }

            lineRenderer.SetPosition(1, new Vector2(length, 0));

            Vector2 endPosition= startPosition + length * direction;
            startVFX.transform.position = startPosition;
            endVFX.transform.position = endPosition;
            endVFX.transform.rotation = Quaternion.Euler(0, 0, laserEndRotation);
        }

    }
}
