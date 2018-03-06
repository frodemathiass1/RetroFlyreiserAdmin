using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace RetroFlyreiser.Model
{
    public static class ErrorLogging
    {

        public static void LogError(Exception ex)
        {

            // Bruk denne for å skrive til lokal filsti. Mapp opp driten selv..

            string strPath = @"C:\Users\Public\FeilLogg.txt";
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("***Error Logging*************");
                sw.WriteLine("*******************************Start" + DateTime.Now);
                sw.WriteLine("Error Message:" + ex.Message);
                sw.WriteLine("Stack Trace:" + ex.StackTrace);
                sw.WriteLine("*******************************End " + DateTime.Now);
            }
        }


        // 
        // Bruk denne for å skrive til lokal filsti. Mapp opp driten selv..
        // Brukes kun på hentEnFlyplass/Kunde/Rute etc, som ikke har try/catch blokker
        public static void LogErrorString(string msg)
        {
            string strPath = @"C:\FeilLogg.txt";
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(strPath))
            {
                sw.WriteLine("***Error Logging*************");
                sw.WriteLine("*******************************Start" + DateTime.Now);
                sw.WriteLine("Error Message:" + msg);
            }
        }


        public static void ReadError()
        {
            string strPath = @"C:\Users\frode\Desktop\ErrorLog.txt";
            using (StreamReader sr = new StreamReader(strPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }



        // Bruk disse for å skrive til azure

        //string fileName = @"FeilLogg.txt";
        //fileName = Path.GetFullPath(fileName);
        //using (StreamWriter sw = new StreamWriter(fileName, true))
        //{
        //    sw.WriteLine("***Error Logging*************");
        //    sw.WriteLine("*******************************Start" + DateTime.Now);
        //    sw.WriteLine("Error Message:" + ex.Message);
        //    sw.WriteLine("Stack Trace:" + ex.StackTrace);
        //    sw.WriteLine("*******************************End " + DateTime.Now);
        //}


        //public static void LogErrorString(string msg)
        //{

        //    string fileName = @"FeilLogg.txt";
        //    fileName = Path.GetFullPath(fileName);

        //    using (StreamWriter sw = new StreamWriter(fileName, true))
        //    {
        //        sw.WriteLine("***Error Logging*************");
        //        sw.WriteLine("*******************************Start" + DateTime.Now);
        //        sw.WriteLine("Error Message:" + msg);
        //    }
        //}
    }

 }
