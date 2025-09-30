using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

public class TimeSpanAsStringSerializer : SerializerBase<TimeSpan>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TimeSpan value)
    {
        // salva como string no formato HH:mm
        context.Writer.WriteString(value.ToString(@"hh\:mm"));
    }

    public override TimeSpan Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return TimeSpan.ParseExact(value, @"hh\:mm", null);
    }
}
