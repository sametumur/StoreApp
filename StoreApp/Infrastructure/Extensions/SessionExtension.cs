using System.Text.Json;

namespace StoreApp.Infrastructure.Extensions;

public static class SessionExtension
{
    public static void AddMessage(this ISession session, string key, string value)
    {
        session.SetString(key, value);   
    }
    
    public static void AddJsonMessage(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));   
    }
    
    public static void AddObjectMessage<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));   
    }
    
    public static string? GetMessage(this ISession session, string key)
    {
        return session.GetString(key);
    }
    
    public static object? GetJsonMessage(this ISession session, string key)
    {
        var jsonString = session.GetString(key);
        if (string.IsNullOrEmpty(jsonString))
            return null;
        
        try
        {
            return JsonSerializer.Deserialize<object>(jsonString);
        }
        catch (JsonException)
        {
            return null;
        }
    }
    
    public static T? GetObjectMessage<T>(this ISession session, string key)
    {
        var jsonString = session.GetString(key);
        if (string.IsNullOrEmpty(jsonString))
            return default(T);
        
        try
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }
        catch (JsonException)
        {
            session.Remove(key);
            return default(T);
        }
    }
}