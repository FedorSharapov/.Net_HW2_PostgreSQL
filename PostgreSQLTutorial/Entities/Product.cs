using System;

namespace PostgreSQLTutorial.Entities
{
    class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int SubwayStationId { get; set; }
        public long UserId { get; set; }

        public override string ToString() 
        {
            return $"Id: {Id}\r\nName: {Name}\r\nPrice: {Price}\r\nDescription: {Description}\r\nSubwayStationId: {SubwayStationId}\r\nUserId: {UserId}\r\n";
        }
    }
}
