﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveConsole.Model
{
    public static class Tools
    {
        public static string ObjectToJson<T>(T objetToSerialize)
        {
            try
            {
                return JsonConvert.SerializeObject(objetToSerialize);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public static List<T> JsonToObject<T>(string jsonString)
        {
            try
            {
                return (List<T>)JsonConvert.DeserializeObject(jsonString);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public static void WriteData(string text, string filepath)
        {
            TextWriter writer = null;
            try
            {
                writer = new StreamWriter(filepath);
                writer.Write(text);
                writer.Close();
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string ReadData(string filepath)
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filepath);
                string temp = reader.ReadToEnd();
                reader.Close();
                return temp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}