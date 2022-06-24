using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;


namespace WebProjectSon.Pages
{
    public class ChartModel : PageModel
    {
        private readonly ILogger<ChartModel> _logger;

        public ChartModel(ILogger<ChartModel> logger)
        {
            _logger = logger;
        }

        public int counterZeroFifteen { get; set; }
        public int counterFifteenThirty { get; set; }
        public int counterThirtyFourtyfive { get; set; }
        public int counterFourtfivePlus { get; set; }
        public int counterMale { get; set; }
        public int counterFemale { get; set; }


        public void OnGet()
        {
            counterZeroFifteen = 0;
            counterFifteenThirty = 0;
            counterThirtyFourtyfive = 0;
            counterFourtfivePlus = 0;
            counterMale = 0;
            counterFemale = 0;
            try
            {
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
                                PersonModel.PersonInfo person = new PersonModel.PersonInfo();
                                person.personBdate = reader.GetInt32(3);
                                person.sex = reader.GetString(4);

                                if (person.sex == "Female")
                                    counterFemale++;
                                else
                                    counterMale++;

                                if (person.personBdate > 0 & person.personBdate <= 15)
                                    counterZeroFifteen++;
                               else if (person.personBdate > 15 & person.personBdate <= 30)
                                    counterFifteenThirty++;
                                else if (person.personBdate > 30 & person.personBdate <= 45)
                                   counterThirtyFourtyfive++;
                                else
                                    counterFourtfivePlus++;
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
