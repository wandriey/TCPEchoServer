/*
 * TCPEchoServer
 *
 * Author Michael Claudius, ZIBAT Computer Science
 * Version 1.0. 2014.02.10
 * Copyright 2014 by Michael Claudius
 * Revised 2014.09.01
 * All rights reserved
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCPEchoServer
{
    class TCPEchoServer
    {

        public static void Main(string[] args)
        {

           // IPAddress ip = new IPAddress("127.0.0.1");

           IPAddress ip = IPAddress.Parse("127.0.0.1");            //en varriable der kan holde ip-adresser

           
            TcpListener serverSocket = new TcpListener(ip, 6789);  //Lyter om der er nogle TCP'er (i vores tilfælde Clienter) der øsker
                                                                   //at oprette forbindelse til ip-adressen "127.0.0.1".
            //Alternatively, but c# dosn't like it.                         
            //TcpListener serverSocket = new TcpListener(6789);


            serverSocket.Start();                                 //Vi kalder metoden .Start, så det er først nu vi begynder at lytte.
            Console.WriteLine("Server started");

            TcpClient connectionSocket = serverSocket.AcceptTcpClient();  //Et TCP objekt bliver oprettet og sæt til en TCPlistner metode, 
            Console.WriteLine("Server activated");                        //så hvis der er en pending TCP request, acceptere connectionSocket den. 
            //Socket connectionSocket = serverSocket.AcceptSocket();


            Stream ns = connectionSocket.GetStream();                  //ligesom i client solutionen, vi oprettet 2 obejekter, der kan læse og skrive
           // Stream ns = new NetworkStream(connectionSocket);         //information. 

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();              //Det clienten skriver, gemmer vi i en variable, fordi vi øskser at sende det tilbage.
            string answer = "";
            while (message != null && message != "")     //Så længe at der bliver skrevet til Serveren, skal den læse det der bliver skrevet
            {                                            //og være istand til at skriv tilbage. <

                Console.WriteLine("Client: " + message);
                answer = message.ToUpper();
                sw.WriteLine(answer);
                message = sr.ReadLine();

            }

            ns.Close();
            connectionSocket.Close();
            serverSocket.Stop();

        }
    }
    
}


