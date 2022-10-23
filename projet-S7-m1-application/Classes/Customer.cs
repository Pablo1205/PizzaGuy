using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    internal class Customer
    {
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        private MySqlConnection conn = null;
        private bool existInDatabase = false;
        public int CustomerID { get; set; }

        public Customer(string PhoneNumber)
        {
            this.PhoneNumber = PhoneNumber;
        }

        public Customer()
        {

        }
        public Customer(int customerID)
        {
            this.CustomerID = customerID;
            GetUserInfoFromID();
        }
        private void GetUserInfoFromID()
        {
            MySqlConnection conn = this.GetConnection();
            string sql = "SELECT * from Customer WHERE customerID='" + this.CustomerID + "'";
            Console.WriteLine("ID OF CUSTOMER ==" + this.CustomerID);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                this.LastName = rdr[1].ToString();
                this.FirstName = rdr[2].ToString();
                this.PhoneNumber = rdr[3].ToString();
                this.City = rdr[4].ToString();
                this.Street = rdr[5].ToString();
                this.PostalCode = rdr[6].ToString();
            }
            rdr.Close();
            this.CloseConnection();
        }
        private MySqlConnection GetConnection()
        {
            if (this.conn != null) return this.conn;
            Database database = new Database();
            this.conn = database.conn;
            return this.conn;
        }
        private void CloseConnection()
        {
            this.conn.Close();
            this.conn = null;
        }
        public bool CheckIfUserIsInDatabase()
        {

            if (this.existInDatabase == true) return true;
            MySqlConnection conn = this.GetConnection();
            string sql = "SELECT * from Customer WHERE PhoneNumber='" + this.PhoneNumber + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                this.existInDatabase = true;
                this.LastName = rdr[1].ToString();
                this.FirstName = rdr[2].ToString();    
                this.Street = rdr[3].ToString();
                this.City = rdr[4].ToString();
                this.PostalCode = rdr[5].ToString();
                this.CustomerID = (int)rdr[0];
            }
            rdr.Close();
            this.CloseConnection();
            if (this.existInDatabase) return true;
            return false;
        }
        public void AddCustomerToDatabase(string lastName, string firstName, string street, string city, string postalCode)
        {
            this.LastName = lastName;
            this.FirstName = firstName;
            this.Street = street;
            this.City = city;
            this.PostalCode = postalCode;
            string sql = "INSERT INTO Customer (LastName, FirstName, Street, Town, PostalCode, PhoneNumber) VALUES ('" + LastName + "','" + FirstName + "', '" + street + "', '" + city + "', '" + postalCode + "', '" + PhoneNumber + "')";
            try
            {
                MySqlConnection conn = this.GetConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
        public string GetFirstName()
        {
            return this.FirstName;
        }

        public string GetLastName()
        {
            return this.LastName;
        }

        public string GetStreet()
        {
            return this.Street;
        }

        public string GetCity()
        {
            return this.City;
        }

        public string GetPhoneNumber()
        {
            return this.PhoneNumber;
        }

        public string GetPostalCode()
        {
            return this.PostalCode;
        }
    }
}
