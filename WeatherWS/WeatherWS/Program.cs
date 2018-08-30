using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

//PAYERIO.COM
namespace WeatherWS
{
    
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest weatherInLondonReq = WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?q=Madrid&appid=aa32db810ed545b6d4c4c08bdf31823f");
            weatherInLondonReq.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)weatherInLondonReq.GetResponse();
            Console.WriteLine(response.StatusDescription);
            Stream dataStream = response.GetResponseStream();
      
            StreamReader reader = new StreamReader(dataStream);
  
            string json = reader.ReadToEnd();
			string correctJason = json.Replace("base", "basek");

            var ms = new MemoryStream(Encoding.Unicode.GetBytes(correctJason));
            DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(WeatherObject));
            WeatherObject wObject = (WeatherObject)deserializer.ReadObject(ms);

            DateTime sunsetDateTime = new DateTime();
            DateTime sunriseDateTime = new DateTime();

			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			sunriseDateTime = dtDateTime.AddSeconds(wObject.sys.sunrise).ToLocalTime();
			//dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			sunsetDateTime = dtDateTime.AddSeconds(wObject.sys.sunset).ToLocalTime();

			Console.WriteLine("Hello " + wObject.name + ": coordinates(" + 
                wObject.coord.lat + "." + wObject.coord.lon + ")");
            Console.WriteLine("The sunset time is: " + sunsetDateTime.ToString());
            Console.WriteLine("The sunrise time is: " + sunriseDateTime.ToString());
			Console.WriteLine("base: " + wObject.basek);
			Console.WriteLine(wObject.weather[0].icon);

            Console.ReadKey();
    
            reader.Close();
            dataStream.Close();
            response.Close();
        }
    }

    [DataContract]
    public class WeatherObject {
        [DataMember]
        public Coordinate coord { get; set; }
        [DataMember]
        public Weather[] weather { get; set; }
        [DataMember]
        public String basek { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public float visibility { get; set; }
        [DataMember]
        public Wind wind { get; set; }
        [DataMember]
        public Cloud clouds { get; set; }
        [DataMember]
        public float dt { get; set; }
        [DataMember]
        public Sys sys { get; set; }
        [DataMember]
        public float id { get; set; }
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public float cod { get; set; }
    }

    [DataContract]
    public class Coordinate {
        [DataMember]
        public float lon { get; set; }
        [DataMember]
        public float lat { get; set; }
    }

    [DataContract]
    public class Weather {
        [DataMember]
        public float id { get; set; }
        [DataMember]
        public String main { get; set; }
        [DataMember]
        public String description { get; set; }
        [DataMember]
        public String icon { get; set; }
    }

    [DataContract]
    public class Main {
        [DataMember]
        public float temp { get; set; }
        [DataMember]
        public float pressure { get; set; }
        [DataMember]
        public float humidity { get; set; }
        [DataMember]
        public float temp_min { get; set; }
        [DataMember]
        public float temp_max { get; set; }
    }

    [DataContract]
    public class Wind
    {
        [DataMember]
        public float speed { get; set; }
        [DataMember]
        public float deg { get; set; }
    }

    [DataContract]
    public class Cloud {
        [DataMember]
        public float all { get; set; }
    }

    [DataContract]
    public class Sys {
        [DataMember]
        public float type { get; set; }
        [DataMember]
        public float id { get; set; }
        [DataMember]
        public float message { get; set; }
        [DataMember]
        public String country { get; set; }
        [DataMember]
        public long sunrise { get; set; }
        [DataMember]
        public long sunset { get; set; }
    }
}
