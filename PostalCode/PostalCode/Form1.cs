using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Noesis.Javascript;
using System.IO;
using System.Net;
using Newtonsoft.Json;

//geocoding api key: &key=AIzaSyDvkrmvwL3RkHRNsqxP2IHHXjOqi5JU59g
//neighbourhood url example: https://maps.googleapis.com/maps/api/geocode/json?address=Winnetka&key=API_KEY
//address url example: https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=API_KEY
//address="1600+Amphitheatre+Parkway,+Mountain+View,+CA"
//url="https://maps.googleapis.com/maps/api/geocode/json?address=%s" % address




namespace PostalCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }




        private void retrieveDataButton_Click(object sender, EventArgs e)
        {
            getJson();  
          
        }

        //returns Json object
        private void getJson()
        {

            String dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\JSON";
            if (!Directory.Exists(dir))
            {
                DirectoryInfo di = Directory.CreateDirectory(dir);
            }

            //use forloop to grab address from file
            //function getInput() returns string = address!, use counter to return ID number
            String address = "Rutherford"; //temp string. actual string comes from DB
            //change API key every 2500~ by mod 2500
            String url = parseURL(address);
            createOutput(url, dir);
            messageLabel.Text = "Status: Succes!";
            
        }

        
        //takes in address or neighbourhood.
        //returns url sent to API
        private String parseURL(String input)
        {
            String address = input;
            String city = ",+Edmonton,+AB,+CA";
            String locationInfo;
            String url = "https://maps.googleapis.com/maps/api/geocode/json?address=";
            //need to build array of keys
            String key = "&key=AIzaSyDvkrmvwL3RkHRNsqxP2IHHXjOqi5JU59g";
            StringBuilder builder = new StringBuilder(address);
            int length = address.Length;

            builder.Replace(' ', '+', 0, length - 1);

            builder.Append(city);
            builder.Append(key);
            builder.Insert(0, url);
            locationInfo = builder.ToString();
            return locationInfo;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void apiBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void createOutput(String url, String dir)
        {

            //connecting to API and retrieving information////
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");//arbitrary header
            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            String result = reader.ReadToEnd();
            /////////////////////////////////////////////////

            //JSON Serializer to pick specific elements//////
            JsonSerializer serial = new JsonSerializer();
            //serial.Serialize(textFile, result);
            /////////////////////////////////////////////////

            //writing JSON result to file///////
            String filePath = dir + @"\" + System.DateTime.Now.Minute.ToString() + "_Json.txt";
            StreamWriter textFile = new StreamWriter(filePath);
            textFile.WriteLine(result);
            textFile.Close();
            ////////////////////////////////////

            //writing results to database//////
            //TODO
            ///////////////////////////////////

            //result test///
            //Console.WriteLine(filePath); //displays contents of json object in console
            ////////////////

            //closing streams/////
            data.Close();
            reader.Close();
            //////////////////////
        }

        private void messageLabel_Click(object sender, EventArgs e)
        {

        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
