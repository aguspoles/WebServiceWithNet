using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace WeatherWS
{
    
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest weatherInLondonReq = WebRequest.Create("https://samples.openweathermap.org/data/2.5/weather?q=London,uk&appid=b6907d289e10d714a6e88b30761fae22");
            weatherInLondonReq.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)weatherInLondonReq.GetResponse();
            Console.WriteLine(response.StatusDescription);
            Stream dataStream = response.GetResponseStream();
      
            StreamReader reader = new StreamReader(dataStream);
  
            string json = reader.ReadToEnd();

            var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(WeatherObject));
            WeatherObject wObject = (WeatherObject)deserializer.ReadObject(ms);

            Console.WriteLine("Hellow " + wObject.name + ": coordinates(" + 
                wObject.coord.lat + "." + wObject.coord.lon + ")");

            DateTime sunsetDateTime = new DateTime(wObject.sys.sunset);
            DateTime sunriseDateTime = new DateTime(wObject.sys.sunrise);
            Console.WriteLine("The sunset time is: " + sunsetDateTime.ToString());
            Console.WriteLine("The sunrise time is: " + sunriseDateTime.ToString());

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
