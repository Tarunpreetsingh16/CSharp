using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Project
{
    public partial class MainWindow : Window
    {
        public void SearchTables (object sender,RoutedEventArgs args)
        {
            if (comboTableSearch.SelectedIndex != -1)
            {
                lblSearchMessage.Content = String.Empty;
                TablesList tablesLeft = new TablesList();
                Timing timing = comboTableSearch.SelectedItem as Timing;

                var tableQuery = from table in tables.Tables
                                 select table;

                foreach (Table table in tableQuery)
                {
                    var timingsQuery = from startTiming in table.Timings.Timings
                                       where startTiming.StartTime == timing.StartTime
                                       select startTiming.Duration;

                    if (timingsQuery.ToList().Count == 0)
                    {
                        tablesLeft.Add(table);
                    }
                }

                listBoxTables.ItemsSource = tablesLeft.Tables;
                listBoxTables.DisplayMemberPath = "TableInfo";
            }
            else
            {
                lblSearchMessage.Content = "Please select a time to search!";
            }
        }

        public void ClearTableSearch(object sender, RoutedEventArgs args)
        {
            listBoxTables.ItemsSource = null;
        }

        public void SearchCustomer(object sender, RoutedEventArgs args)
        {
            lblSearchMessage.Content = String.Empty;
            listBoxBookings.Items.Clear();

            if (txtPhoneForSearch.Text.Trim().Length != 0)
            {
                Customer booker;
                string phone = txtPhoneForSearch.Text;

                var customerQuery = from customer in customers.GetList()
                                    where customer.Phone == phone
                                    select customer;
                for (int i = 0; i < customerQuery.ToList().Count; i++)
                {
                    booker = customerQuery.ToList()[i];

                    Label lblBookingNumber = new Label();
                    lblBookingNumber.Content = "Booking " + (i + 1);
                    lblBookingNumber.FontWeight = FontWeights.UltraBlack;
                    listBoxBookings.Items.Add(lblBookingNumber);

                    Label lblCustomerId = new Label();
                    lblCustomerId.Content = "Customer Id";
                    listBoxBookings.Items.Add(lblCustomerId);

                    TextBox txtCustomerId = new TextBox();
                    txtCustomerId.Text = booker.Id.ToString();
                    txtCustomerId.IsEnabled = false;
                    txtCustomerId.Background = Brushes.Transparent;
                    txtCustomerId.FontWeight = FontWeights.Bold;
                    listBoxBookings.Items.Add(txtCustomerId);

                    Label lblCustomerName = new Label();
                    lblCustomerName.Content = "Name";
                    listBoxBookings.Items.Add(lblCustomerName);

                    TextBox txtCustomerName = new TextBox();
                    txtCustomerName.Text = booker.Name;
                    txtCustomerName.IsEnabled = false;
                    txtCustomerName.Background = Brushes.Transparent;
                    txtCustomerName.FontWeight = FontWeights.Bold;
                    listBoxBookings.Items.Add(txtCustomerName);

                    Label lblCustomerEmail = new Label();
                    lblCustomerEmail.Content = "Email";
                    listBoxBookings.Items.Add(lblCustomerEmail);

                    TextBox txtCustomerEmail = new TextBox();
                    txtCustomerEmail.Text = booker.Email;
                    txtCustomerEmail.IsEnabled = false;
                    txtCustomerEmail.Background = Brushes.Transparent;
                    txtCustomerEmail.FontWeight = FontWeights.Bold;
                    listBoxBookings.Items.Add(txtCustomerEmail);

                    var bookingQuery = from booking in bookingsList
                                       where booking.CustomerId == booker.Id
                                       select booking;

                    foreach (Booking booking in bookingQuery)
                    {


                        Label lblBookingId = new Label();
                        lblBookingId.Content = "Booking Id";
                        listBoxBookings.Items.Add(lblBookingId);

                        TextBox txtBookingId = new TextBox();
                        txtBookingId.Text = booking.Id.ToString();
                        txtBookingId.IsEnabled = false;
                        txtBookingId.Background = Brushes.Transparent;
                        txtBookingId.FontWeight = FontWeights.Bold;
                        listBoxBookings.Items.Add(txtBookingId);

                        Label lblBookingTableInfo = new Label();
                        lblBookingTableInfo.Content = "Table";
                        listBoxBookings.Items.Add(lblBookingTableInfo);

                        TextBox txtBookingTableInfo = new TextBox();
                        txtBookingTableInfo.Text = booking.BookingForList;
                        txtBookingTableInfo.IsEnabled = false;
                        txtBookingTableInfo.Background = Brushes.Transparent;
                        txtBookingTableInfo.FontWeight = FontWeights.Bold;
                        listBoxBookings.Items.Add(txtBookingTableInfo);
                    }

                    Label blank = new Label();
                    listBoxBookings.Items.Add(blank);
                }
                if (listBoxBookings.Items.Count == 0)
                {
                    lblSearchMessage.Content = "No bookings found for this number!";
                }
            }
            else
            {
                lblSearchMessage.Content = "Please enter a number first!";
            }
        }
    }
}
