using EmployeesLibrary;
using System;

using Xamarin.Forms;

namespace EmployeesIOS
{
    public class AddEmployeePage : ContentPage
    {
        private EmployeesHelper eh;
        private Entry id;
        private Entry firstName;
        private Entry lastName;
        private Entry mail;

        public AddEmployeePage (EmployeesHelper eh)
        {
            this.eh = eh;

            Title = "Add Employee";

            id = new Entry { Placeholder = "ID" };
            firstName = new Entry { Placeholder = "First Name" };
            lastName = new Entry { Placeholder = "Last Name" };
            mail = new Entry { Placeholder = "Mail" };

            var addButton = new Button { Text = "Add" };
            addButton.Clicked += AddButton_Clicked;

            Content = new StackLayout {
                Children = {
                    id,
                    firstName,
                    lastName,
                    mail,
                    addButton,
                }
            };
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            eh.AddEmployee(new EmployeeModel { Id = int.Parse(id.Text), FirstName = firstName.Text, LastName = lastName.Text, Mail = mail.Text });
        }
    }
}