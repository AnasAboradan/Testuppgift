using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace learingwell
{
    public struct Countries
    {
        public string countries_code { get; set; }
        public string Amount_male { get; set; }
        public string Amount_female { get; set; }

        public Countries(string code, string amount_male, string amount_female)
        {
            this.countries_code = code;
            this.Amount_male = amount_male;
            this.Amount_female = amount_female;
        }
    }
    
    
    class Chart_Data
    {
        private Root[] data;
        public Chart_Data(string Url)

        {
            using (var client = new HttpClient())
            {
                // make http request (get request)
                try
                {
                    var endpoint = new Uri(Url);
                    var result = client.GetAsync(endpoint).Result;
                    string json = result.Content.ReadAsStringAsync().Result;
                   
                    //convert the json file into c# classes to make the manipulation of the data easy
                    // Rott class repreents the json file
                    data = JsonConvert.DeserializeObject<Root[]>(json); 
                }
                catch
                { 
                    MessageBox.Show("An error occurred, check your network connection status or" +
                                    " The API link you provided");
                }
                
            }
        }


        // return a list of years the json file contains in order to make it easy to user to select the year
        // 
        public List<string> get_years()
        {
            List<string> l = new List<string>();
            foreach (var item in data)
            {
                if (!l.Contains(item.dimensions.ar))
                    l.Add(item.dimensions.ar);
            }

            return l;
        }


        // create dictionary for country code (as key) and use the struct countries (as value)
        //the function loop through the json file element by element 
        //and it extracts the number of male and female for each country code based on the number of years
        public Dictionary<string, Countries> GetCountries_code_Amount_MaleAnadFamale(string year)
        {
            var dictionary = new Dictionary<string, Countries>();
            foreach (var e in data)
            {
                if (e.dimensions.ar == year)
                {      
                    // check if the coutry code already found
                    if (dictionary.ContainsKey(e.dimensions.vardland_kod))
                    {
                        // update the dictionary if the key already exists
                        if (e.dimensions.kon_kod == "K")
                        {
                            dictionary[e.dimensions.vardland_kod] = new Countries(dictionary[e.dimensions.vardland_kod].countries_code,
                                                                                  dictionary[e.dimensions.vardland_kod].Amount_male,
                                                                                  e.observations.antal.value);
                        }

                        else if (e.dimensions.kon_kod == "M")
                        {
                            dictionary[e.dimensions.vardland_kod] = new Countries(dictionary[e.dimensions.vardland_kod].countries_code,
                                                                                  e.observations.antal.value,
                                                                                  dictionary[e.dimensions.vardland_kod].Amount_female);

                        }
                    }
                    else   // Add new country code to the dictionary if the key dose not exists
                    {
                        if (e.dimensions.kon_kod == "K")
                        {
                            dictionary.Add(e.dimensions.vardland_kod, new Countries(e.dimensions.vardland_kod,
                                                                                    "",
                                                                                    e.observations.antal.value));
                        }

                        else if (e.dimensions.kon_kod == "M")
                        {
                            dictionary[e.dimensions.vardland_kod] = new Countries(e.dimensions.vardland_kod,
                                                                                  e.observations.antal.value,
                                                                                  "");

                        }

                    }

                }
            }

            return dictionary;


        }


        
    }
}
