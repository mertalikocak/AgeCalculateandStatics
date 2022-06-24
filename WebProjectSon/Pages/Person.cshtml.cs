using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace WebProjectSon.Pages
{
    public class PersonModel : PageModel
    {
        public List<PersonInfo> listPerson = new List<PersonInfo>();

        public void OnGet()
        {
           try { 
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=webstore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT* FROM  dbo.people";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PersonInfo person = new PersonInfo();
                                person.personName = reader.GetString(1);
                                person.personSurname = reader.GetString(2);
                                person.personBdate = reader.GetInt32(3);
                                person.sex = reader.GetString(4);
                                person.city = reader.GetString(5);

                                listPerson.Add(person);
                            }
                        }
                    }

                }

           }
           catch(Exception ex)
            {

            }

        }

        [BindProperty]

        public String search { get; set; }

        [BindProperty]

        public String order { get; set; }



        public class PersonInfo
        {
            public string personName;
            public string personSurname;
            public int personBdate;
            public string sex;
            public string city;
        }
        public void OnPostSearch()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=webstore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int i = 0;
                    string query;
                    bool result = int.TryParse(search, out i);
                    if (search == null)
                    {
                        query = "SELECT* FROM  dbo.people";
                    }
                    else if (result)
                    {
                        query = "SELECT*  FROM  dbo.people WHERE personBdate='" + search + "'";
                    }
                    else
                    {
                        query = "SELECT*  FROM  dbo.people WHERE personName='" + search + "' or personSurname='" + search + "' or sex='" + search + "' or city='" + search + "'";
                    }
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PersonInfo person = new PersonInfo();
                                person.personName = reader.GetString(1);
                                person.personSurname = reader.GetString(2);
                                person.personBdate = reader.GetInt32(3);
                                person.sex = reader.GetString(4);
                                person.city = reader.GetString(5);

                                listPerson.Add(person);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void OnPostOrder()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=webstore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query;
                    if (order == "1")
                    {
                        query = "SELECT* FROM  dbo.people ORDER BY personBdate ASC ";
                    }
                    else if (order=="2")
                    {
                        query = "SELECT* FROM  dbo.people ORDER BY personBdate DESC";
                    }
                    else
                    {
                        query = "SELECT* FROM  dbo.people";
                    }
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PersonInfo person = new PersonInfo();
                                person.personName = reader.GetString(1);
                                person.personSurname = reader.GetString(2);
                                person.personBdate = reader.GetInt32(3);
                                person.sex = reader.GetString(4);
                                person.city = reader.GetString(5);

                                listPerson.Add(person);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
    

    }
}
