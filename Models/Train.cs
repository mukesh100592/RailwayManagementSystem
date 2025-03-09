namespace RailwayManagementSystem.Models
{
    public class Train
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Type { get; set; } // 0 - Passenger, 1 - Freight
        public string Route { get; set; }
    }
}
