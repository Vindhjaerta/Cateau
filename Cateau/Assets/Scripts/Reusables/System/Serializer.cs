using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
/*
     Author: Daniel Vindhjärta
     Last updated: 2018-02-02 
     Purpose:
     Serialize any object to a file and also be able to deserialize back again.
     
     Usage:
     Make sure that T is a serializable object by putting [System.Serializable] on every class member or
     use the interface ISerializable. Same thing for any class/object you use with the List<object>.
     When using the lists, simply upcast the objects when you retrieve them.
     
        string myString = "demo";
        List<object> data = new List<object>();
        data.Add(myString);
        bool result = Serializer.SaveToDisc("save.bin", data);
     
        data = Serializer.LoadFromDisc("save.bin");
        if(data != null)
        {
            myString = (string)data[0];
        }

     Dependencies:
     None
     
*/
public static class Serializer
    {

    public static bool SaveToDisc<T>(string filename, T data)
    {
        bool result = false;
        Stream stream = null;
        try
        {
            IFormatter formatter = new BinaryFormatter();
            stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, data);
            result = true;
        }
        catch
        {

        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }

        }
        return result;
    }

    public static T LoadFromDisc<T>(string filename)
    {
        T data = default(T);
        Stream stream = null;
        try
        {
            IFormatter formatter = new BinaryFormatter();
            stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            data = (T)formatter.Deserialize(stream);
        }
        catch
        {

        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }
        }
        return data;
    }

    public static bool SaveToDisc(string filename, List<object> objectList)
    {
        bool result = false;
        Stream stream = null;
        try
        {
            IFormatter formatter = new BinaryFormatter();
            stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);

            if(objectList != null)
            {
                formatter.Serialize(stream, objectList);
            }
            result = true;
        }
        catch
        {

        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }

        }
        return result;
    }

    public static List<object> LoadFromDisc(string filename)
    {
        List<object> data = null;
        Stream stream = null;
        try
        {
            IFormatter formatter = new BinaryFormatter();
            stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            data = (List<object>)formatter.Deserialize(stream);
        }
        catch
        {

        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }
        }
        return data;
    }
}

