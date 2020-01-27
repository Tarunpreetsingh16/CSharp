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

using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Project
{
    public partial class MainWindow : Window
    {
        BookingsList bookings = new BookingsList();
        CustomersList customers = new CustomersList();
        PhoneNumberList phoneNumbers = new PhoneNumberList();

        BindingList<Booking> bookingsList = new BindingList<Booking>();
        BindingList<Customer> customersList = new BindingList<Customer>();


        XmlWriter writer;

        public void CreateBooking(object sender, RoutedEventArgs args)
        {
            //check if there is any inconsistency with the data input by the user
            /*
             result  => 0  = Blank fields
             result  => 2  = Wrong phone number format
             result  => 3  = Wrong email format
             */
            uint result = CheckData(txtName.Text, txtPhone.Text, txtEmail.Text);
            if (result == 0 || comboTableName.SelectedIndex == -1 || comboTimings.SelectedIndex == -1 )
            {
                lblCreationMessage.Content = "Please enter required fields! (*)";
            }
            else if (result == 2)
            {
                lblCreationMessage.Content = "Please enter number in correct format! Like - 4372234159";
            }
            else if (result == 3)
            {
                lblCreationMessage.Content = "Please enter email in correct format! Like - tsingh@conestoga.on";
            }
            else
            {
                lblCreationMessage.Content = String.Empty;


                //add customer to the list if the data is correct
                Customer customer = new Customer(txtName.Text, txtPhone.Text, txtEmail.Text);
                customer.Id = SharedFunctionalities.GenerateAccountNum();
                customers.Add(customer);


                Table table = comboTableName.SelectedItem as Table;
                Timing timing = comboTimings.SelectedItem as Timing;

                //create booking and add it to the list
                Booking booking = new Booking(table.TableNumber,customer.Id, timing );
                booking.Id = SharedFunctionalities.GenerateBookingId();
                bookings.Add(booking);

                lblCreationMessage.Content = "Booking created!";


                AddTimings(comboTableName.SelectedItem, comboTimings.SelectedItem);
                UpdateLists();
                SaveToFile();
                ClearInputFields();
            }
        }

        public uint CheckData(string name, string phoneNumber, string email)
        {
            if (name.Trim().Length == 0 || phoneNumber.Trim().Length == 0 || email.Trim().Length == 0)
            {
                return 0;
            }
            if (email.Trim().Length != 0)
            {
                string pattern = @"^[a-zA-Z0-9.!#$%&’*+\/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.([a-zA-Z0-9-]+)$";
                Match result = Regex.Match(email, pattern);

                if (!result.Success)
                {
                    return 3;
                }
            }
            if (phoneNumber.Trim().Length != 0)
            {
                string pattern = @"^([0-9]{10})$";
                Match result = Regex.Match(phoneNumber,pattern);

                if (!result.Success)
                {
                    return 2;
                }
            }
            return 1;
        }

        public void UpdateLists()
        {
            customersList.Clear();
            bookingsList.Clear();
            phoneNumbers.Clear();

            for(int i = 0; i < customers.Count; i++)
            {
                customersList.Add(customers[i]);
                if (!phoneNumbers.Contains(customers[i]))
                {
                    PhoneNumber phoneNumber = new PhoneNumber();
                    phoneNumber.Phone = customers[i].Phone;
                    phoneNumber.Ids.Add(customers[i].Id);
                    phoneNumbers.Add(phoneNumber);
                }
            }

            listBoxName.ItemsSource = customersList;
            listBoxName.DisplayMemberPath = "Name";

            listBoxPhone.ItemsSource = phoneNumbers.PhoneNumbers;
            listBoxPhone.DisplayMemberPath = "Phone";

            for (int i = 0; i < bookings.Count; i++)
            {
                bookingsList.Add(bookings[i]);
            }

            listBoxBooking.ItemsSource = bookingsList;
            listBoxBooking.DisplayMemberPath = "BookingForList";
        }

        public void ClearFields(object sender, RoutedEventArgs args)
        {
            ClearInputFields();
            lblCreationMessage.Content = String.Empty;
        }

        public void ClearInputFields()
        {
            txtEmail.Text = String.Empty;
            txtPhone.Text = String.Empty;
            txtName.Text = String.Empty;
            comboTimings.SelectedIndex = -1;
            comboTableName.SelectedIndex = -1;
            comboTimings.ItemsSource = null;
        }

        public void AddTimings(object selectedTable, object selectedTiming)
        {
            Table table = selectedTable as Table;
            Timing timing = selectedTiming as Timing;

            for (int i=0;i<tables.Count;i++)
            {
                if(tables[i].TableNumber == table.TableNumber)
                {
                    tables[i].Timings.Add(timing);
                }
            }
        }

        public void CheckTimings(object sender,RoutedEventArgs args)
        {
            if (comboTableName.SelectedIndex != -1)
            {
                comboTimings.SelectedIndex = -1;

                TimingsList timingsLeft = new TimingsList();

                Table table = comboTableName.SelectedItem as Table;


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

        public void HighlightCustomers(object sender, RoutedEventArgs args)
        {
            listBoxName.SelectedItems.Clear();
            listBoxPhone.SelectedItems.Clear();

            Booking booking = listBoxBooking.SelectedItem as Booking;

            if (booking != null)
            {
                for (int i = 0; i < listBoxName.Items.Count; i++)
                {
                    Customer customer = listBoxName.Items[i] as Customer;

                    if (customer.Id == booking.CustomerId)
                    {
                        listBoxName.SelectedItems.Add(listBoxName.Items[i]);
                    }
                    for (int j = 0; j < listBoxPhone.Items.Count; j++)
                    {
                        PhoneNumber number = listBoxPhone.Items[j] as PhoneNumber;

                        for (int k = 0; k < number.Ids.Count; k++)
                        {
                            if (number.Ids[k] == booking.CustomerId)
                            {
                                listBoxPhone.SelectedItems.Add(listBoxPhone.Items[j]);
                            }
                        }
                    }
                }
            }
            
        }
        public void SaveToFile()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            try
            {
                writer = XmlWriter.Create("bookings.xml", settings);

                writer.WriteStartDocument();
                writer.WriteStartElement("Bookings");
                for (int i = 0; i < bookings.Count; i++)
                {
                    writer.WriteStartElement("Booking");
                    writer.WriteElementString("BookingId", bookings[i].Id.ToString());
                    writer.WriteElementString("TableNumber", bookings[i].TableNumber);
                    writer.WriteElementString("CustomerId", bookings[i].CustomerId.ToString());
                    writer.WriteElementString("StartTime", bookings[i].Timing.StartTime.ToString());
                    writer.WriteElementString("EndTime", bookings[i].Timing.EndTime.ToString());

                    for (int j = 0; j < customers.Count; j++)
                    {
                        if (customers[j].Id == bookings[i].CustomerId)
                        {
                            writer.WriteStartElement("Customer");
                            writer.WriteElementString("CustomerId", customers[j].Id.ToString());
                            writer.WriteElementString("Name", customers[j].Name);
                            writer.WriteElementString("Phone", customers[j].Phone);
                            writer.WriteElementString("Email", customers[j].Email);
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                if(writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
