using EmployeesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmployeesIOS
{
    public partial class MainPage : ContentPage
    {
        private EmployeesHelper eh;
        private List<EmployeeModel> employees;
        private StackLayout stack;

        public MainPage(EmployeesHelper eh)
        {
            this.eh = eh;
            Title = "Employees List";
            
            employees = eh.GetEmployees();

            stack = new StackLayout
            {
                Spacing = 10,
                Padding = 10,
                VerticalOptions = LayoutOptions.Start
            };

            foreach (var employee in employees)
            {
                if (employee != null)
                {
                    stack.Children.Add(new Label()
                    {
                        Text = employee.ToString()
                    });
                }
            }

            stack.BindingContext = employees;

            Content = new ScrollView { Content = stack };
        }

        private void SearchBar_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string query = ((SearchBar)sender).Text;
            var empl = eh.GetEmployeeByName(query);
            if (empl != null)
            {
                employees = new List<EmployeeModel>
                {
                    empl
                };
            }
        }
    }
}
