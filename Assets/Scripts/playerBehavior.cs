using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System;
using System.Collections.Generic;
using Microsoft.Win32;

public class playerBehavior : MonoBehaviour
{
    public GameObject parent;
    private Queue inputQueue = new Queue();
    public GameObject projectile;
    public GameObject projectile2;
    private Thread thread;
    public float shootInterval = 0.4f;
    private float lastShot = 0f;
    SerialPort sp = new SerialPort(AutodetectArduinoPort(), 9600);
    string data;
    Vector3 inputMovement;
    int Tolerance = 120;
    int elementsRecv = 7;
    private bool threading;
    private int equippedWeapon = 0;
    public int hp =100;
    public int shootSpeed =20;
    public GameObject fires;

    private int joyX;
    private int joyY;
    private int joyPress;
    private int whiteButton;
    private int redButton;
    private int blueButton;
    private int pot1;

    void Start()
    {
        
        sp.Open();
        sp.ReadTimeout = 20;
        
        StartThread();
    }

    void Update()
    {
        checkQueue();
        checkHp();
        processMovement();
    }

        
    //Starts a new thread. 
    //This is needed because SerialPort methods are blocking and cause lag if not handled in another thread
    public void StartThread()
    {
        thread = new Thread(ThreadLoop);
        threading = true;
        thread.Start();
    }

    //Detects arduino port, if there is none found defaults to COM8
    public static string AutodetectArduinoPort()
    {
        List<string> comports = new List<string>();
        RegistryKey rk1 = Registry.LocalMachine;
        RegistryKey rk2 = rk1.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum");
        string temp;
        foreach (string s3 in rk2.GetSubKeyNames())
        {
            RegistryKey rk3 = rk2.OpenSubKey(s3);
            foreach (string s in rk3.GetSubKeyNames())
            {
                if (s.Contains("VID") && s.Contains("PID"))
                {
                    RegistryKey rk4 = rk3.OpenSubKey(s);
                    foreach (string s2 in rk4.GetSubKeyNames())
                    {
                        RegistryKey rk5 = rk4.OpenSubKey(s2);
                        if ((temp = (string)rk5.GetValue("FriendlyName")) != null && temp.Contains("Arduino"))
                        {
                            RegistryKey rk6 = rk5.OpenSubKey("Device Parameters");
                            if (rk6 != null && (temp = (string)rk6.GetValue("PortName")) != null)
                            {
                                comports.Add(temp);
                            }
                        }
                    }
                }
            }
        }

        if (comports.Count > 0)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                if (comports.Contains(s))
                    return s;
            }
        }

        return "COM8";
    }

    public void ThreadLoop()
    {
        while (threading)
        {
            try
            {
                int c = sp.ReadChar();
                //Checking for header sent from arduino
                if(c == 'h')
                {
                    data = sp.ReadLine(); //Read the information
                    //Debug.Log(data);
                    string[] myvec3 = data.Split(',');

                    if (validData(myvec3))
                    {

                        inputQueue.Enqueue(myvec3);
                        
                    }
                }
                

            }
            


            catch (TimeoutException e)
            {
                //Debug.Log("timeout");
            }

            catch (System.Exception)
            {

            }



        }
    }

    public void checkQueue()
    {
        if(inputQueue.Count != 0)
        {
            string[] data = inputQueue.Dequeue() as string[];
            processData(data);
        }
        else
        {
            Debug.Log("not enough commands");
        }
    }

    public void processData(string[] Data)
    {

        joyX = int.Parse(Data[0]);
        joyY = int.Parse(Data[1]);
        joyPress = int.Parse(Data[2]);
        whiteButton = int.Parse(Data[3]);
        redButton = int.Parse(Data[4]);
        blueButton = int.Parse(Data[5]);
        pot1 = int.Parse(Data[6]);

        //Debug.Log(joyX+"."+ joyY+"." + joyPress + "." + whiteButton + "." + redButton + "." + blueButton + "." + pot1);


        if (joyY <= 512 + Tolerance && joyY >= 512 - Tolerance)
        {
            joyY = 0;
        }
        else
        {
            joyY = 512 - joyY;
        }
        if (joyX <= 512 + Tolerance && joyX >= 512 - Tolerance)
        {
            joyX = 0;
        }
        else
        {
            joyX = joyX - 512;
        }

        if (pot1 <= 512 + 400 && pot1 >= 400 - Tolerance)
        {
            pot1 = 0;
        }
        else
        {
            pot1 = pot1 - 512;
        }



        if (redButton == 1)
        {

            if (Time.time - lastShot >= shootInterval)
            {
                GameObject t = Instantiate(projectile);
                t.transform.position = gameObject.transform.position + new Vector3(0, -2f, 2.5f);

                Destroy(t, 2.5f);
                lastShot = Time.time;
            }
        }

        if (whiteButton == 1)
        {


            if (Time.time - lastShot >= shootInterval)

            {

                GameObject t = Instantiate(projectile2);
                //gameObject.transform.position + gameObject.transform.forward * 2 + gameObject.transform.right, gameObject.transform.rotation
                t.GetComponent<laserBehavior>().source = laserBehavior.laserSource.Player;
                GameObject p = Instantiate(projectile2);
                t.GetComponent<laserBehavior>().source = laserBehavior.laserSource.Player;
                p.transform.position = gameObject.transform.position + gameObject.transform.forward * 2 + gameObject.transform.right;
                t.transform.position = gameObject.transform.position + gameObject.transform.forward * 2 + -gameObject.transform.right;
                t.gameObject.GetComponent<Renderer>().material.color = p.gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
                t.transform.GetComponent<Rigidbody>().velocity = p.transform.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * shootSpeed;

                t.gameObject.layer = p.gameObject.layer = LayerMask.NameToLayer("playerProjectile");


                Destroy(t, 2.5f);
                Destroy(p, 2.5f);




                lastShot = Time.time;
            }
        }
            inputMovement = new Vector3(joyX, pot1 / 5, joyY);
            gameObject.transform.Translate(inputMovement * Time.deltaTime / 10);

        
    }

        private void processMovement()
    {
        
        //checks if ship outside of bounds 
        if (gameObject.transform.localPosition.y >= 16f)
        {
            Vector3 newPos = new Vector3(gameObject.transform.localPosition.x, 16f, gameObject.transform.localPosition.z);
            gameObject.transform.localPosition = newPos;
        }

        if (gameObject.transform.localPosition.y <= 2f)
        {
            Vector3 newPos = new Vector3(gameObject.transform.localPosition.x, 2f, gameObject.transform.localPosition.z);
            gameObject.transform.localPosition = newPos;
        }


        if (gameObject.transform.localPosition.x >= 4.5f)
        {
            Vector3 newPos = new Vector3(4.5f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            gameObject.transform.localPosition = newPos;
        }
        if (gameObject.transform.localPosition.x <= -4.5f)
        {
            Vector3 newPos = new Vector3(-4.5f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
            gameObject.transform.localPosition = newPos;
        }

        if (gameObject.transform.localPosition.z <= -7f)
        {
            Vector3 newPos = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -7f);
            gameObject.transform.localPosition = newPos;
        }
        if (gameObject.transform.localPosition.z >= 7f)
        {
            Vector3 newPos = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 7f);
            gameObject.transform.localPosition = newPos;
        }
    
    }
    //validates incoming data 
    public bool validData(string[] data)
    {
        if (data.Length == elementsRecv)
        {
            foreach (string i in data)
            {
                if(i == "")
                {
                    
                    return false;
                    
                }
            }
            
            return true;
            
        }
        
        
        return false;
        
    }

    void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("collision");



    }
    public void damageMessage(int damage)
    {
        hp = hp - damage;
        if (damage >= 5 && hp < 50)
        {
        GameObject f = Instantiate(fires);
        f.transform.position = gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-1.5f,1.5f),.5f, UnityEngine.Random.Range(-.5f, -.7f));
        f.transform.SetParent(gameObject.transform);
        //Destroy(f, (damage / 5f) + 4);
    }
}

    public void checkHp()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        sp.Close();
        Debug.Log("closed com port via destroy");
        
        threading = false;
        Debug.Log("closed thread via destroy");
    }



    void OnApplicationQuit()
    {
        Debug.Log("closed com port");
        sp.Close();
        threading = false;
        Debug.Log("Ended thread");
    }
}