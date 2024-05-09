using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using VMFramework.GameLogicArchitecture;
using UnityEngine;

public static class CustomJSONConverter
{
    public static JsonConverter[] converters => new JsonConverter[]
    {
        new Vector2Converter(),
        new Vector2IntConverter(),
        new Vector3Converter(),
        new Vector3IntConverter(),
        new Vector4Converter(),
        new ColorConverter(),
        new GameTypeConverter(),
        new StringEnumConverter(),
    };
}

public class Vector2Converter : JsonConverter<Vector2>
{
    public override void WriteJson(JsonWriter writer, Vector2 value,
        JsonSerializer serializer)
    {
        writer.WriteValue($"({value.x}, {value.y})");
    }

    public override Vector2 ReadJson(JsonReader reader, Type objectType,
        Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var value = JToken.Load(reader).Value<string>();
        value = value.Trim('(', ')');
        var split = value.Split(',');
        if (split.Length == 2 &&
            float.TryParse(split[0].Trim(), out float x) &&
            float.TryParse(split[1].Trim(), out float y))
        {
            return new Vector2(x, y);
        }

        return existingValue;
    }
}

public class Vector2IntConverter : JsonConverter<Vector2Int>
{
    public override void WriteJson(JsonWriter writer, Vector2Int value,
        JsonSerializer serializer)
    {
        writer.WriteValue($"({value.x}, {value.y})");
    }

    public override Vector2Int ReadJson(JsonReader reader, Type objectType,
        Vector2Int existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var value = JToken.Load(reader).Value<string>();
        value = value.Trim('(', ')');
        var split = value.Split(',');
        if (split.Length == 2 &&
            int.TryParse(split[0].Trim(), out int x) &&
            int.TryParse(split[1].Trim(), out int y))
        {
            return new Vector2Int(x, y);
        }

        return existingValue;
    }
}

public class Vector3Converter : JsonConverter<Vector3>
{
    public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
    {
        JObject o = new()
        {
            { "x", value.x },
            { "y", value.y },
            { "z", value.z }
        };

        o.WriteTo(writer);
    }

    public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject o = JObject.Load(reader);

        return new((float)o.GetValue("x"), (float)o.GetValue("y"), (float)o.GetValue("z"));
    }
}

public class Vector3IntConverter : JsonConverter<Vector3Int>
{
    public override void WriteJson(JsonWriter writer, Vector3Int value, JsonSerializer serializer)
    {
        JObject o = new()
        {
            { "x", value.x },
            { "y", value.y },
            { "z", value.z }
        };

        o.WriteTo(writer);
    }

    public override Vector3Int ReadJson(JsonReader reader, Type objectType, Vector3Int existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject o = JObject.Load(reader);

        Debug.Log((int)o.GetValue("x"));
        Debug.Log((int)o.GetValue("y"));
        Debug.Log((int)o.GetValue("z"));

        return new((int)o.GetValue("x"), (int)o.GetValue("y"), (int)o.GetValue("z"));
    }
}

public class Vector4Converter : JsonConverter<Vector4>
{
    public override void WriteJson(JsonWriter writer, Vector4 value, JsonSerializer serializer)
    {
        JObject o = new()
        {
            { "x", value.x },
            { "y", value.y },
            { "z", value.z },
            { "w", value.w },
        };

        o.WriteTo(writer);
    }

    public override Vector4 ReadJson(JsonReader reader, Type objectType, Vector4 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject o = JObject.Load(reader);

        return new((float)o.GetValue("x"), (float)o.GetValue("y"), (float)o.GetValue("z"), (float)o.GetValue("w"));
    }
}

public class ColorConverter : JsonConverter<Color>
{
    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
    {
        JObject o = new()
        {
            { "r", value.r },
            { "g", value.g },
            { "b", value.b },
            { "a", value.a }
        };

        o.WriteTo(writer);
    }

    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject o = JObject.Load(reader);

        return new((float)o.GetValue("r"), (float)o.GetValue("g"), (float)o.GetValue("b"), (float)o.GetValue("a"));
    }
}

public class GameTypeConverter : JsonConverter<GameType>
{
    public override void WriteJson(JsonWriter writer, GameType value, JsonSerializer serializer)
    {
        writer.WriteValue(value.id);
    }

    public override GameType ReadJson(JsonReader reader, Type objectType, GameType existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        return (string)reader.Value;
    }
}
