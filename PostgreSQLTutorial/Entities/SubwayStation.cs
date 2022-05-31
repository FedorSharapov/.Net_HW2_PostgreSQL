using System;

namespace PostgreSQLTutorial.Entities
{
    class SubwayStation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id}\t{Name}";
        }
    }
}
