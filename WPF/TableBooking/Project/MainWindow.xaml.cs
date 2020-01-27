using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.IO;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        XmlReader reader;
        TablesList tables = new TablesList();
        TimingsList restaurantTimings = new TimingsList();
        public MainWindow()
        {
            InitializeComponent();
            CheckFileExpiry();
            FetchTables();
            SetTables();
            DefineRestaurantTimings();
            FetchBookings();
            UpdateLists();
            SetTimings();
        }

        public void FetchTables()
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;


            try
            {

                reader = XmlReader.Create("Tables.xml", settings);
                if (reader.ReadToDescendant("Table"))
                {
                    do
                    {
                        Table table = new Table();
                        reader.ReadStartElement("Table");
                        table.TableNumber = reader.ReadElementContentAsString(); ;
                        table.Seats = uint.Parse(reader.ReadElementContentAsString());
                        table.TableInfo = "Table - " + table.TableNumber +" | Seats - "+ table.Seats.ToString();
                        tables.Add(table);

                    } while (reader.ReadToNextSibling("Table"));
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        public void SetTables()
        {
            comboTableName.ItemsSource = tables.Tables;
            comboTableName.DisplayMemberPath = "TableInfo";
        }

        public void DefineRestaurantTimings()
        {

            restaurantTimings.Add(new Timing(1,2));
            restaurantTimings.Add(new Timing(2,3));
            restaurantTimings.Add(new Timing(3,4));
            restaurantTimings.Add(new Timing(4,5));
            restaurantTimings.Add(new Timing(5,6));
            restaurantTimings.Add(new Timing(6,7));
            restaurantTimings.Add(new Timing(7,8));
            restaurantTimings.Add(new Timing(8,9));
            restaurantTimings.Add(new Timing(9,10));
            restaurantTimings.Add(new Timing(10,11));
        }
        
        public void FetchBookings()
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            try
            {
                reader = XmlReader.Create("bookings.xml", settings);

                reader.ReadToDescendant("Booking");
                do
                {

                    reader.ReadStartElement("Booking");
                    uint bookingId = uint.Parse(reader.ReadElementContentAsString());
                    SharedFunctionalities.bookingIds.Add(bookingId);
                    Booking booking = new Booking(bookingId,
                                                  reader.ReadElementContentAsString(),
                                                  uint.Parse(reader.ReadElementContentAsString()),
                                                  new Timing(uint.Parse(reader.ReadElementContentAsString()),
                                                  uint.Parse(reader.ReadElementContentAsString())));
                    bookings.Add(booking);

                    reader.ReadStartElement("Customer");


                    uint customerId = uint.Parse(reader.ReadElementContentAsString());
                    SharedFunctionalities.accountNum.Add(customerId);
                    Customer customer = new Customer(customerId,
                                                     reader.ReadElementContentAsString(),
                                                     reader.ReadElementContentAsString(),
                                                     reader.ReadElementContentAsString());

                    customers.Add(customer);
                    reader.Skip();

                    for(int i = 0; i < tables.Count; i++)
                    {
                        if(tables[i].TableNumber == booking.TableNumber)
                        {
                            tables[i].Timings.Add(booking.Timing);
                        }
                    }

                } while (reader.ReadToNextSibling("Booking"));
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                if(reader != null)
                {
                    reader.Close();
                }
            }
        }
        public void CheckFileExpiry()
        {
            DateTime fileCreationtTime = File.GetLastWriteTime(@"bookings.xml");
            if(DateTime.Now.Date > fileCreationtTime.Date)
            {
                writer = XmlWriter.Create("bookings.xml");
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        public void SetTimings()
        {
            comboTableSearch.ItemsSource = restaurantTimings.Timings;
            comboTableSearch.DisplayMemberPath = "Duration";
        }

    }
}
