using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Wired.Services
{
    public class WindService
    {
        private float _windSpeed = 0.02f;
        private float _spatialScale = 0.005f;

        public struct WindData
        {
            public float Intensity;
            public Vector3 Direction;
        }

        public WindData GetWindAt(Vector3 position)
        {
            float timeOffset = Time.time * _windSpeed;
            float sampleX = position.x * _spatialScale + timeOffset;
            float sampleZ = position.z * _spatialScale + timeOffset;

            float intensity = Mathf.PerlinNoise(sampleX, sampleZ);

            float angleNoise = Mathf.PerlinNoise(sampleX + 1000f, sampleZ + 1000f);

            float angle = angleNoise * 360f;

            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            return new WindData
            {
                Intensity = intensity,
                Direction = direction
            };
        }
    }
}
