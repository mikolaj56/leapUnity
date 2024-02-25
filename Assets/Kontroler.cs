using Leap;
using Leap.Unity;
using UnityEngine;

public class Kontroler : MonoBehaviour
{
    public LeapProvider leapProvider;
    public GameObject prefab, szescian, kula;
    private Rigidbody rbPrefab, rbSzescian, rbKula;
    public SkinnedMeshRenderer lewa,prawa;
    public Material czarny, niebieski;
    private Vector3 poczRozmiar1,poczRozmiar2,poczRozmiar3;
    private bool freeze = false;
    Vector3 lewaPoz, prawaPoz;
    private float minRozmiar = 0.1f; 
    private float maxRozmiar = 5.0f;
    private bool szczyp = false;
    private float poczatkowyDyst; 
    private float aktualnyDyst;
    private void Start()
    {
        rbPrefab = prefab.GetComponent<Rigidbody>();
        rbSzescian = szescian.GetComponent<Rigidbody>();
        rbKula = kula.GetComponent<Rigidbody>();
        leapProvider.OnUpdateFrame += LeapUpdate;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            prefab.SetActive(true);
            prefab.transform.position = new Vector3(0.0f, 0.2f, -9.0f);
            prefab.transform.rotation = Quaternion.Euler(0, 90, 0);
            prefab.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            szescian.SetActive(false);
            kula.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            prefab.SetActive(false);
            szescian.SetActive(true);
            szescian.transform.position = new Vector3(0.0f, 0.2f, -9.0f);
            szescian.transform.rotation = Quaternion.Euler(0, 90, 0);
            szescian.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            kula.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            prefab.SetActive(false);
            szescian.SetActive(false);
            kula.SetActive(true);
            kula.transform.position = new Vector3(0.0f, 0.2f, -9.0f);
            kula.transform.rotation = Quaternion.Euler(0, 90, 0);
            kula.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            freeze = !freeze;
            if (freeze)
            {
                prefab.transform.position = new Vector3(0.0f, 0.2f, -9.0f);
                szescian.transform.position = new Vector3(0.0f, 0.2f, -9.0f);
                kula.transform.position = new Vector3(0.0f, 0.2f, -9.0f);

                rbPrefab.constraints = RigidbodyConstraints.FreezePosition;
                rbSzescian.constraints = RigidbodyConstraints.FreezePosition;
                rbKula.constraints = RigidbodyConstraints.FreezePosition;
            }
            else
            {
                rbPrefab.constraints = RigidbodyConstraints.None;
                rbSzescian.constraints = RigidbodyConstraints.None;
                rbKula.constraints = RigidbodyConstraints.None;
            }
            
        }

    }     
    void Clamp(ref Vector3 vector)
    {
        vector.x = Mathf.Clamp(vector.x, minRozmiar, maxRozmiar);
        vector.y = Mathf.Clamp(vector.y, minRozmiar, maxRozmiar);
        vector.z = Mathf.Clamp(vector.z, minRozmiar, maxRozmiar);
    }
    void LeapUpdate(Frame frame)
    {
        
        Hand lewaReka = frame.GetHand(Chirality.Left);
        Hand prawaReka = frame.GetHand(Chirality.Right);
        
        if (lewaReka != null && prawaReka != null)
        {
           
            lewaPoz = lewaReka.PalmPosition;
            prawaPoz = prawaReka.PalmPosition;
            aktualnyDyst = Vector3.Distance(lewaPoz, prawaPoz);
            if (lewaReka.IsPinching() && prawaReka.IsPinching())
            {
                if (!szczyp)
                {
                    szczyp = true;
                    poczatkowyDyst = aktualnyDyst;
                    poczRozmiar1 = prefab.transform.localScale;
                    poczRozmiar2 = szescian.transform.localScale;
                    poczRozmiar3 = kula.transform.localScale;
                    lewa.material = niebieski;
                    prawa.material = niebieski;
                }
                    float skala = aktualnyDyst / poczatkowyDyst;
                    Vector3 nowyRozmiar1 = poczRozmiar1 * skala;
                    Vector3 nowyRozmiar2 = poczRozmiar2 * skala;
                    Vector3 nowyRozmiar3 = poczRozmiar3 * skala;

                    Clamp(ref nowyRozmiar1);
                    Clamp(ref nowyRozmiar2);
                    Clamp(ref nowyRozmiar3);

                    prefab.transform.localScale = nowyRozmiar1;
                    szescian.transform.localScale = nowyRozmiar2;
                    kula.transform.localScale = nowyRozmiar3;
            }
            else
            { 
                szczyp = false;
                lewa.material = czarny;
                prawa.material = czarny;
            }
        }

    }

}

