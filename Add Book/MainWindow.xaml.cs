using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace Add_Book
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<List<string>> Contacts = new List<List<string>>();
		public MainWindow()
		{
			InitializeComponent();
			Contacts = ReadCSV();
			UpdateContactList();
		}

		private void ClearTextBoxes()
		{
			NameBox.Clear();
			AddBox.Clear();
			NumBox.Clear();
			EmailBox.Clear();
		}
		private void AddBtn_Click(object sender, RoutedEventArgs e)
		{
			List<string> NewEntry = new List<string>
			{
				NameBox.Text,
				AddBox.Text,
				NumBox.Text,
				EmailBox.Text
			};
			Contacts.Add(NewEntry);

			SaveContacts();
			MessageBox.Show(NameBox.Text + " has been added");
			UpdateContactList();
			ClearTextBoxes();

		}
		private void UpdateBtn_Click(object sender, RoutedEventArgs e)
		{
			if (ContactList.SelectedItem != null)
			{
				int SelectedEntry = ContactList.SelectedIndex;
				Contacts[SelectedEntry][0] = NameBox.Text;
				Contacts[SelectedEntry][1] = AddBox.Text;
				Contacts[SelectedEntry][2] = NumBox.Text;
				Contacts[SelectedEntry][3] = EmailBox.Text;

				SaveContacts();
				MessageBox.Show(ContactList.SelectedItem + " contact updated");
				UpdateContactList();
				ClearTextBoxes();
			}
		}

		private void RemoveBtn_Click(object sender, RoutedEventArgs e)
		{
			if (ContactList.SelectedItem != null)
			{
				MessageBox.Show(ContactList.SelectedItem + " removed");
				Contacts.RemoveAt(ContactList.SelectedIndex);

				SaveContacts();
				UpdateContactList();
				ClearTextBoxes();
			}
		}

		private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string SearchContact = SearchBox.Text.ToLower();

			List<List<string>> SearchFilter = new List<List<string>>();
			if (!string.IsNullOrEmpty(SearchContact))
			{
				SearchFilter = Contacts
					.Where(contact => contact.Any(field => field.ToLower().Contains(SearchContact)))
					.ToList();
			}
			else
			{
				SearchFilter = Contacts;
			}

			UpdateContactList(SearchFilter);
		}

		private void UpdateContactList(List<List<string>> contactsToShow = null)
		{
			ContactList.Items.Clear();

			if (contactsToShow == null)
			{
				foreach (var contact in Contacts)
				{
					ContactList.Items.Add(contact[0]);
				}
			}
			else
			{
				foreach (var contact in contactsToShow)
				{
					ContactList.Items.Add(contact[0]);
				}
			}
		}

		private void ContactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ContactList.SelectedItem != null)
			{
				int selectedIndex = ContactList.SelectedIndex;
				List<string> selectedContact = Contacts[selectedIndex];
				NameBox.Text = selectedContact[0];
				AddBox.Text = selectedContact[1];
				NumBox.Text = selectedContact[2];
				EmailBox.Text = selectedContact[3];
			}
		}

		private void SaveContacts()
		{
			using (StreamWriter writer = new StreamWriter("contacts.csv"))
			{
				foreach (List<string> contact in Contacts)
				{
					writer.WriteLine(string.Join(",", contact));
				}
			}
		}

		private List<List<string>> ReadCSV()
		{
			List<List<string>> loadedContacts = new List<List<string>>();

			if (File.Exists("contacts.csv"))
			{
				using (StreamReader reader = new StreamReader("contacts.csv"))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						List<string> parts = line.Split(',').ToList();
						loadedContacts.Add(parts);
					}
				}
			}
			return loadedContacts;
		}

		private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void NumBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void AddBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}
	}
}
		


