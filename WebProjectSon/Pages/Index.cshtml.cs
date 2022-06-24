using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace WebProjectSon.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        
        [BindProperty]
        
        public String personName { get; set; }
       

        [BindProperty]
        public String personSurname { get; set; }

        [BindProperty]
        public DateTime personBdate { get; set; }

        [BindProperty]
        public String sex { get; set; }

        [BindProperty]
        public String city { get; set; }

        public String message { get; set; }

        public int age { get; set; }

        public List<cityInfo> listCity = new List<cityInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=webstore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT* FROM  dbo.cities";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cityInfo city = new cityInfo();
                                city.code = reader.GetString(0);
                                city.name = reader.GetString(1);

                                listCity.Add(city);
                            }
                        }
                    }
                }

            }

            catch (Exception ex)
            {

            }

        }

        public class cityInfo
        {
            public string code;
            public string name;
        }

        public void OnPost()
        {
            try
            {
                DateTime thisDate = DateTime.Today;
                if (
                    thisDate.Month > personBdate.Month |
                    (thisDate.Month == personBdate.Month &
                    thisDate.Day >= personBdate.Day)
                )
                {
                    age = thisDate.Year - personBdate.Year;
                }
                else
                {
                    age = thisDate.Year - personBdate.Year - 1;
                }

                message = String.Format("Welcome {0} {1}. Your age is {2}.Your sex is {3} and live in {4}.", personName, personSurname, age, sex, city);



                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=webstore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "insert into dbo.people(personName,personSurname,personBdate,sex,city) values('"+personName+"','"+personSurname+ "'," + age+ ",'" + sex+ "','" + city+"')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();

                    }
                        
                }
            }
            
        catch (Exception ex)
            {

            }

        }
            
    }
            
}


    
