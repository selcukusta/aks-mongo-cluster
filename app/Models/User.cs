using System;
using MongoDB.Bson;

namespace OpenHackApp.Models
{
        public class User
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}