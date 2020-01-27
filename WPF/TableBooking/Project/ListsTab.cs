using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Project
{
    public partial class MainWindow : Window
    {
        public void EditDetails(object sender, RoutedEventArgs args)
        {
            if (listBoxBooking.SelectedItems.Count == 0)
            {
                lblListsTabMessage.Content = "Select a Booking first!";
            }
            else if(listBoxBooking.SelectedItems.Count > 1){

                lblListsTabMessage.Content = "Please Select only 1 booking!";
            }
            else
            {
                lblListsTabMessage.Content = String.Empty;
                Booking booking = listBoxBooking.SelectedItem as Booking;

                Window1 window = new Window1(ref booking,ref tables,ref customers,ref bookings);
                window.ShowDialog();

                UpdateLists();
                SaveToFile();

            }
        }
        public void DeleteBooking(object sender, RoutedEventArgs args)
        {
            if (listBoxBooking.SelectedItems.Count == 0)
            {
                lblListsTabMessage.Content = "Select a Booking first!";
            }
            else if (listBoxBooking.SelectedItems.Count > 1)
            {

                lblListsTabMessage.Content = "Only 1 booking can be deleted at a time!";
            }
            else
            {
                Booking booking = listBoxBooking.SelectedItem as Booking;

                for (int  i = 0; i<bookings.Count; i++)
                {
                    if (bookings[i].Id ==  booking.Id)
                    {

                        for (int  j =0;j<customers.Count;j++)
                        {
                            if (customers[j].Id == bookings[i].CustomerId)
                            {
                                customers.RemoveAt(j);
                            }
                        }

                        bookings.RemoveAt(i);
                    }
                }

                for (int i=0;i<tables.Count;i++)
                {
                    if(tables[i].TableNumber == booking.TableNumber)
                    {
                        for (int j=0;j<tables[i].Timings.Count;j++)
                        {
                            if (tables[i].Timings[j].StartTime == booking.Timing.StartTime)
                            {
                                tables[i].Timings.RemoveAt(j);
                            }
                        }
                    }
                }

                UpdateLists();
                SaveToFile();
            }
        }

    }
}
