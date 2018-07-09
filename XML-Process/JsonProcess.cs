using System;
using System.Collections.Generic;
using System.Text;


   public   class JsonProcess
    {

        public static string encode(Object obj)
        {
            return SimpleJson.SimpleJson.SerializeObject(obj);
        }

        public static T decode<T>(string value)
        {

            return SimpleJson.SimpleJson.DeserializeObject<T>(value);
        }
    }

