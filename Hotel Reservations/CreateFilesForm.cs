using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelsManagement;
using System.IO;
using System.Xml;


namespace Hotel_Reservations
{
    public partial class CreateFilesForm : Form
    {

        public HotelDataManager hotelManager;
        public List<Hotel> hotel_list;
        public String hotels_filePath = "file_hotels.xml";
        public String inventories_filePath = "file_roominventory.xml";

        public CreateFilesForm()
        {
            InitializeComponent();

            //Create the manager
            this.hotelManager = new HotelDataManager();

            //Create some hotels
            const int amountOfHotels = 10;
            String[] hotelNames = new String[amountOfHotels] {
                "Bronze Nebula Hotel & Spa",
                "Lord's Valley Hotel",
                "Golden Grove Resort",
                "Viridian Vertex Hotel",
                "Mellow Bay Resorts",
                "Scarlet Raven Hotel",
                "Emerald Hotel",
                "Ocean Hotel",
                "Seven Seas Resort",
                "Stratosphere Hotel & Spa"
            };

            String[] hotelAddresses = new String[amountOfHotels] {
                "1600 N Lakeline Blvd #1726, Cedar Park, TX, 78613",
                "11 Logan Pl, Mastic, NY, 11950",
                "3583 Bayview Ave, Saint James City, FL, 33956",
                "168 Jones Chapel Shiloh Rd, Danielsville, GA, 30633",
                "3 Elm Pvt Dr, Somerville, AL, 35670",
                "1205 S Hitchite Ave, Wewoka, OK, 74884",
                "1015 W Sam Houston St, Pharr, TX, 78557",
                "270 Cherokee Dr, Saulsbury, TN, 38067",
                "163 Ems C31 Ln, Warsaw, IN, 46582",
                "8454 Wright St, Fort Campbell, KY, 42223"
            };

            Random r = new Random();

            for (int i = 0; i < amountOfHotels; i++)
            {            
                Hotel hotel = new Hotel(i, hotelNames[i], hotelAddresses[i]);
                hotel.Features = new Features(
                    (r.NextDouble() < 0.5) ? true : false, 
                    (r.NextDouble() < 0.5) ? true : false, 
                    (r.NextDouble() < 0.5) ? true : false, 
                    (r.NextDouble() < 0.5) ? true : false);
                hotel.Features.Distances.Airport = Math.Round(r.NextDouble() * 5, 2);
                hotel.Features.Distances.Beach = Math.Round(r.NextDouble() * 5, 2);
                hotel.Features.Distances.Shopping = Math.Round(r.NextDouble() * 5, 2);
           
                Room KB = new Room(Room.BedType.KING, 5);
                Room KB = new Room(Room.BedType.KING, r.Next(90 + 1) + 100, 5);
                hotel.RoomList.Add(KB);

                Room QB = new Room(Room.BedType.QUEEN, 4);
                Room QB = new Room(Room.BedType.QUEEN, r.Next(90 + 1) + 100, 4);
                hotel.RoomList.Add(QB);

                Room DB = new Room(Room.BedType.DOUBLE, 2);
                Room DB = new Room(Room.BedType.DOUBLE, r.Next(90 + 1) + 100, 2);
                hotel.RoomList.Add(DB);

                Room BS = new Room(Room.BedType.SUITE, 6);
                Room BS = new Room(Room.BedType.SUITE, r.Next(90 + 1) + 100, 6);
                hotel.RoomList.Add(BS);

                this.hotelManager.hotels.Add(hotel);
            }
        }

        private void btnDisplayHotels_Click(object sender, EventArgs e)
        {
            BrowserForm frm = new BrowserForm();
            frm.URL = hotels_filePath; // "hotels.xml";
            frm.ShowDialog();
        }

        private void btnDisplayInventory_Click(object sender, EventArgs e)
        {
            BrowserForm frm = new BrowserForm();
            frm.URL = inventories_filePath; //"roominventory.xml";
            frm.ShowDialog();
        }

        private void btnCreateHotels_Click(object sender, EventArgs e)
        {
            //Create the xml file
            this.hotelManager.writeToXML(this.hotelManager.hotels, hotels_filePath);
        }

        private void btnCreateInventory_Click(object sender, EventArgs e)
        {

            //Deserialize the xml
            String result = "";
            List<Hotel> hotels = new List<Hotel>();
            hotels = this.hotelManager.readFromXML(out result, hotels);
            List<InventoryType> inventories = new List<InventoryType>();
            //inventories = this.hotelManager.readFromXML(out result, inventories);

            int year = 2016;
            int month = 12;

            if (hotels != null)
            {
                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                {
                    DateTime dt = new DateTime(year, month, i);
                    // Console.WriteLine(dt.ToShortDateString());

                    //Loop through bed sizes
                    for (int rb = 0; rb < Enum.GetNames(typeof(Room.BedType)).Length; rb++)
                    {
                        InventoryType inventory = null;
                        Random r = new Random();

                        for (int h = 0; h < hotels.Count; h++)
                        {
                           
                            //Grab the hotel
                            Hotel hotel = hotels[h];
                            
                            //Create the inventory instance
                            inventory = new InventoryType(hotel.ID, dt);
                            inventory.Quantity = r.Next(0, 6);

                            //Cast int to enum
                            inventory.RoomType = (Room.BedType)rb;

                            //Add inventory instance to manager inventory list                        
                            this.hotelManager.inventory.Add(inventory);
                        }
                                                 
                    }
                   
                }
                
                //Create the xml file
                this.hotelManager.writeToXML(this.hotelManager.inventory, inventories_filePath);
            }

            /*
            System.Console.WriteLine("READING FILE!");
            System.Console.WriteLine( hotels.Capacity);
            for (int i = 0; i < hotels.Count; i++)
            {
                Console.WriteLine(hotels[i].Name);
            }
            */
            
            
        }
    }
}
