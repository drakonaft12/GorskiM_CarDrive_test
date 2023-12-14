using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff_Lights : MonoBehaviour
{
    [SerializeField] private GameObject _lightMini;
    [SerializeField] private Material _materiallightMini;
    [SerializeField] private GameObject _lightMaxi;
    [SerializeField] private Material _materiallightMaxi;
    [SerializeField] private GameObject _lightNight;
    [SerializeField] private Material _materiallightNight;
    [SerializeField] private GameObject _lightBack;
    [SerializeField] private Material _materiallightBack;
    [SerializeField] private GameObject _lightLeft;
    [SerializeField] private Material _materiallightLeft;
    [SerializeField] private GameObject _lightRight;
    [SerializeField] private Material _materiallightRight;

    private GameObject _car;
    private Rigidbody _rigcar;
    private int _perekl;
    private bool _isNight = false , _revers = false;
    void Start()
    {
        _car = gameObject.transform.parent.gameObject;
        _rigcar = _car.GetComponent<Rigidbody>();
        _lightMini.SetActive(false);
        _lightMaxi.SetActive(false);
        _lightNight.SetActive(false);
        _lightBack.SetActive(false);
        _lightLeft.SetActive(false);
        _lightRight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) { _perekl++; _perekl %= 3;
            switch (_perekl)
            {
                case 0:
                    Perecl(_lightMini, _materiallightMini, false); Perecl(_lightMaxi, _materiallightMaxi, false); _isNight = false;
                    break;
                case 1:
                    Perecl(_lightMini, _materiallightMini, true); _isNight = true;
                    break;
                case 2:
                    Perecl(_lightMaxi, _materiallightMaxi, true); _isNight = true;
                    break;
            }
        }

            
        //Debug.Log((_rigcar.velocity.normalized - _car.transform.forward).magnitude);
        if (Input.GetKey(KeyCode.S))
        {
            if((_rigcar.velocity.normalized - _car.transform.forward).magnitude < 1)
            {
                Perecl(_lightNight, _materiallightNight, true); Perecl(_lightBack, _materiallightBack, false); _revers = false;
            }
            else
            {
                Perecl(_lightNight, _materiallightNight, false); Perecl(_lightBack, _materiallightBack, true); _revers = true;
            }
        }
        else
        {
            Perecl(_lightNight, _materiallightNight, _isNight); Perecl(_lightBack, _materiallightBack, false);
        }

        if (Input.GetKey(KeyCode.A) || _revers && Input.GetKey(KeyCode.D))
        {
            Perecl(_lightLeft, _materiallightLeft, true); Perecl(_lightRight, _materiallightRight, false);
        }
        else if(Input.GetKey(KeyCode.D) || _revers && Input.GetKey(KeyCode.A))
        {
            Perecl(_lightLeft, _materiallightLeft, false); Perecl(_lightRight, _materiallightRight, true);
        }
        else
        {
            Perecl(_lightLeft, _materiallightLeft, false); Perecl(_lightRight, _materiallightRight, false);
        }

    }

    private void Perecl(GameObject obj, Material mat, bool on)
    {
        obj.SetActive(on);
        if (on)
        {
            mat.EnableKeyword("_EMISSION");
        }
        else
        {
            mat.DisableKeyword("_EMISSION");
        }
    }
}
