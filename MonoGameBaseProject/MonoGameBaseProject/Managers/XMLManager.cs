using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace EcoShoot.Managers
{
    public class XmlManager<T>
    {
        //Como es una clase genérica, todas las variables solo pueden ser Type :0
        public Type Type;

        public XmlManager()
        {
            this.Type = typeof(T);
        }

        //Métodos-Funciones
        public T Load(string path)
        {
            T instance;
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(Type);
                instance = (T)serializer.Deserialize(reader);
            }
            return instance;
        }


        public void Save(string path, object obj)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer serializer = new XmlSerializer(Type);
                serializer.Serialize(writer, obj);
            }
        }
    }
}