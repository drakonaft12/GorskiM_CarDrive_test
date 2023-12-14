using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Car
{
    public class SimpleCar : MonoBehaviour
    {
        [Header("Steer")]
        [SerializeField]
        private float maxSteer = 45;
        [SerializeField]
        private Wheel[] steerWheels = Array.Empty<Wheel>();
        [Header("Power")]
        [SerializeField]
        private float power = 10;
        [SerializeField]
        private Wheel[] powerWheels = Array.Empty<Wheel>();
        [Header("UI")]
        [SerializeField] Text _textLabel;
        [Space]
        [SerializeField]
        private Lights lights;

        private int schet;
        private void Update()
        {
            UpdatSchet();
            Turning();
            Powering();
            PoweringOne();
            //Lights();
        }

        private void UpdatSchet()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                schet++; schet %= 3;
                switch (schet)
                {
                    case 0:
                        _textLabel.text = "Zadni";
                        break;
                    case 1:
                        _textLabel.text = "Peredni";
                        break;
                    case 2:
                        _textLabel.text = "Polni";
                        break;
                }
            }
            
        }
        private void Turning()
        {

            foreach (var wheelCollider in steerWheels)
            {
                wheelCollider.Steer(
                    Input.GetAxis("Horizontal")
                    * maxSteer
                );
            }
        }

        private void Powering()
        {
            if (schet == 0 || schet == 2)
            {
                foreach (var powerWheel in powerWheels)
                {
                    powerWheel.Torque(
                        Input.GetAxis("Vertical")
                        * power
                        * Time.deltaTime
                    );
                }
            }
        }
        private void PoweringOne()
        {
            if (schet == 1 || schet == 2)
            {
                foreach (var powerWheel in steerWheels)
                {
                    powerWheel.Torque(
                        Input.GetAxis("Vertical")
                        * power
                        * Time.deltaTime
                    );
                }
            }
        }

        private void Lights()
        {
            lights.TailLights(Input.GetAxis("Vertical") < 0);
        }
    }
}