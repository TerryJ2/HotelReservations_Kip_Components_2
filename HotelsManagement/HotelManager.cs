using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HotelsManagement
{
    public class Hotel
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public Features Features { get; set; }
        public List<Room> RoomList;

        public Hotel() { }

        public Hotel(int id, String name, String address)
        {
            this.ID = id;
            this.Name = name;
            this.Address = address;
            this.RoomList = new List<Room>();
        }
    }

    public class Features
    {
        public Boolean Laundry { get; set; }
        public Boolean AirConditioning { get; set; }
        public Boolean Shuttle { get; set; }
        public Boolean Breakfast { get; set; }
        public Distances Distances { get; set; }

        public Features () { }

        public Features (Boolean laundry = true, Boolean airconditioning = true, Boolean shuttle = true, Boolean breakfast = true)
        {
            this.Laundry = laundry;
            this.AirConditioning = airconditioning;
            this.Shuttle = shuttle;
            this.Breakfast = breakfast;
            this.Distances = new Distances();
        }
    }

    public class Distances
    {
        public Double Beach { get; set; }
        public Double Shopping { get; set; }
        public Double Airport { get; set; }

        public Distances () { }
    }

    public class Room
    {
        public enum BedType
        {
            KING,
            QUEEN,
            DOUBLE,
            SUITE
        }

        public BedType BedSize { get; set; }
        public int DailyRate { get; set; }
        public int Capacity { get; set; }
        //public int Current { get; set; }
        public String Name { get; set; }

        public Room() { }

        public Room (BedType bedSize = BedType.KING, int DailyRate = 0, int capacity = 2)
        {
            this.BedSize = bedSize;
            this.DailyRate = DailyRate;
            this.Capacity = capacity;
            
            switch(this.BedSize)
            {
                default:  case BedType.KING: this.Name = "Single King Bed"; break;
                case BedType.QUEEN: this.Name = "Single Queen Bed"; break;
                case BedType.DOUBLE: this.Name = "Two Double Beds"; break;
                case BedType.SUITE: this.Name = "One Bedroom Suite"; break;

            }

        }
    }

    public class InventoryType
    {
        public String Date { get; set; }
        public int HotelId { get; set; }
        public Room.BedType RoomType { get; set; }
        public int Quantity { get; set; }

        public InventoryType() { }

        public InventoryType(int hotelid, DateTime date, Room.BedType bedtype = Room.BedType.DOUBLE, int quantity = 0)
        {
            this.Date = date.ToShortDateString();
            this.HotelId = hotelid;
            this.RoomType = bedtype;
            this.Quantity = quantity;
        }
        
    }
}
