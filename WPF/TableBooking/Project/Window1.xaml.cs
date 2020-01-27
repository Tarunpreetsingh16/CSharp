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
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        TimingsList restaurantTimings =  new TimingsList();
        Booking booking;
        TablesList tables;
        CustomersList customers;
        BookingsList bookings;
        public Window1(ref Booking booking, ref TablesList tables, ref CustomersList customers, ref BookingsList bookings)
        {
            this.booking = booking;
            this.tables = tables;
            this.bookings = bookings;
            this.customers = customers;

            InitializeComponent();
            DefineRestaurantTimings();
            InitializeFields(ref tables, ref booking, ref customers);
        }
        public void InitializeFields(ref TablesList tables,ref Booking booking, ref CustomersList customers)
        {

            comboTables.ItemsSource = tables.Tables;
            comboTables.DisplayMemberPath = "TableInfo";

            for(int i = 0; i < tables.Count; i++)
            {
                if (tables[i].TableNumber == booking.TableNumber)
                {
                    comboTables.SelectedIndex = i;
                    break;
                }
            }
            txtCurrentTime.Text = booking.Timing.Duration;
            DisplayTiming();

            for (int i = 0;i<customers.Count;i++)
            {
                if (customers[i].Id == booking.CustomerId)
                {
                    txtName.Text = customers[i].Name;
                    txtEmail.Text = customers[i].Email;
                    txtPhone.Text = customers[i].Phone;
                    break;
                }
            }

        }

        public void CheckTimings(object sender, RoutedEventArgs args)
        {
            DisplayTiming();
        }

        public void DisplayTiming()
        {

            if (comboTables.SelectedIndex != -1)
            {
                comboTimings.SelectedIndex = -1;

                TimingsList timingsLeft = new TimingsList();

                Table table = comboTables.SelectedItem as Table;


                for (int i = 0; i < restaurantTimings.Count; i++)
                {
                    if (!table.Timings.Contains(restaurantTimings.Timings[i]))
                    {
                        timingsLeft.Add(restaurantTimings.Timings[i]);
                    }
                }

                comboTimings.ItemsSource = timingsLeft.Timings;
                comboTimings.DisplayMemberPath = "Duration";
            }
        }

        public void DefineRestaurantTimings()
        {

            restaurantTimings.Add(new Timing(1, 2));
            restaurantTimings.Add(new Timing(2, 3));
            restaurantTimings.Add(new Timing(3, 4));
            restaurantTimings.Add(new Timing(4, 5));
            restaurantTimings.Add(new Timing(5, 6));
            restaurantTimings.Add(new Timing(6, 7));
            restaurantTimings.Add(new Timing(7, 8));
            restaurantTimings.Add(new Timing(8, 9));
            restaurantTimings.Add(new Timing(9, 10));
            restaurantTimings.Add(new Timing(10, 11));
        }
        public void SaveDetails(object sender, RoutedEventArgs args)
        {
            if (comboTimings.SelectedIndex != -1)
            {
                Timing timing = comboTimings.SelectedItem as Timing;
                for(int i = 0; i < bookings.Count; i++)
                {
                    if (booking.Id == bookings[i].Id)
                    {
                        for (int j = 0; j < tables.Count; j++)
                        {
                            Table table = comboTables.SelectedItem as Table;
                            if (tables[j].TableNumber == table.TableNumber)
                            {
                                for (int k = 0; k < tables[j].Timings.Count; k++)
                                {
                                    if (tables[j].Timings[k].StartTime == bookings[i].Timing.StartTime)
                                    {
                                        tables[j].Timings.Timings.RemoveAt(k);
                                    }
                                }
                                tables[j].Timings.Add(timing);
                                
                            }
                        }

                        bookings[i].Timing = new Timing(timing.StartTime,timing.EndTime);
                        bookings[i].BookingForList = "Table - " + bookings[i].TableNumber + " | Time - " + bookings[i].Timing.Duration;

                    }
                }
            }
            this.Close();
        }
        public void CloseWindow(object sender,RoutedEventArgs args)
        {
            this.Close();
        }
    }
}
