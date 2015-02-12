using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;

//geocoding api key: 
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
            getData();
        }



        private String parseURL(String input)
        {
            String address = input;
            String city = ",+Edmonton,+AB,+CA";
            String locationInfo;
            String url = "https://maps.googleapis.com/maps/api/geocode/xml?address=";
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

        private void getData()
        {
            ArrayList dataset = addressData();
            WebClient client = new WebClient();
            Stream data = null;
            String url;
            XDocument xml = new XDocument();
            XNode node = null;
            System.Text.RegularExpressions.Regex pattern = new System.Text.RegularExpressions.Regex(@"(?<=>)(.*)(?=<)");
            System.Text.RegularExpressions.Match match = null;
            String address = null;
            String postal_code = null;


            for (int i = 0; i < dataset.Count; i++)
            {
                //set url
                url = parseURL(dataset[i].ToString());
                address = dataset[i].ToString();

                //connecting to API and retrieving information////

                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");//arbitrary header
                data = client.OpenRead(url);
                xml = XDocument.Load(data);
                foreach (XElement xe in xml.Descendants("type").Where(x => x.Value.Equals("postal_code")))
                {
                    node = xe.PreviousNode.PreviousNode;
                }

                if (node != null)
                {
                    match = pattern.Match(node.ToString());


                    if (match.Success)
                    {
                        postal_code = match.Value;


                        //write to DB
                        //Console.WriteLine(postal_code);
                        insertData(postal_code, address);
                    }
                }

            }

            data.Close();
          


            
        }

        private void messageLabel_Click(object sender, EventArgs e)
        {

        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private ArrayList addressData()
        {
            String connectionString = "Data Source=WIN-FSBHP3NVE9G;Initial Catalog=TRAFFIC_COLLISION;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command;
            String sql = "SELECT TOP 1000 Location FROM Combined_Data";
            SqlDataReader dataReader;
            ArrayList results = new ArrayList();
            String value;

                try
                {
                    conn.Open();
                    command = new SqlCommand(sql, conn);
                    dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        //MessageBox.Show(dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2));
                        value = dataReader.GetValue(0).ToString();
                        results.Add(value);
                        
                    }
                    dataReader.Close();
                    command.Dispose();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot open connection ! ");
                }

                return results;

        }


        private void insertData(String postal_code, String address)
        {
            String connectionString = "Data Source=WIN-FSBHP3NVE9G;Initial Catalog=TRAFFIC_COLLISION;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            String sql = "UPDATE Combined_Data SET Postal_Code = '"+postal_code+"' WHERE Location = '"+address+"'";
            SqlCommand command = new SqlCommand(sql, conn);

            try
            {
                command.Connection.Open();

                command.ExecuteNonQuery();

                command.Dispose();

                command.Connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open connection ! ");
            }
        }


    }

}




