using EmployeesLibrary;
using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;

namespace EmployeesIOS
{
    public partial class MainPage : ContentPage
    {
        private EmployeesHelper eh;
        private List<EmployeeModel> employees;
        private StackLayout stack;
        private Button refreshButton;
        private int selectedEmployeeId = -1;

        public MainPage(EmployeesHelper eh)
        {
            this.eh = eh;
            Title = "Employees List";
            BindingContext = employees;

            employees = eh.GetEmployees();

            refreshButton = new Button { Text = "Refresh" };
            refreshButton.Clicked += RefreshButton_Clicked;

            stack = new StackLayout
            {
                Spacing = 10,
                Padding = 10,
                VerticalOptions = LayoutOptions.Start
            };

            stack.Children.Add(refreshButton);

            foreach (var employee in employees)
            {
                if (employee != null)
                {
                    var button = new Button
                    {
                        Text = "Delete",
                        TextColor = Color.WhiteSmoke,
                        BackgroundColor = Color.DarkRed,
                        Margin = new Thickness(5),
                        HorizontalOptions = new LayoutOptions(LayoutAlignment.End, true),
                        CommandParameter = employee.Id,
                    };
                    button.Clicked += Button_Clicked;

                        stack.Children.Add(new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label()
                            {
                                Text = employee.ToString(),
                            },
                            button
                        }
                    });
                }
            }

            stack.BindingContext = employees;

            Content = new ScrollView { Content = stack };
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            selectedEmployeeId = int.Parse(((Button)sender).CommandParameter.ToString());
            var a = new UIAlertView
            {
                Title = "Delete",
                Message = "Delete employee #" + selectedEmployeeId + "?",
            };
            a.AddButton("Cancel");
            a.AddButton("Ok");
            a.Clicked += A_Clicked;
            a.Show();
        }

        private void A_Clicked(object sender, UIButtonEventArgs e)
        {
            if (e.ButtonIndex == 1)
            {
                eh.DeleteEmployee(selectedEmployeeId);
            }
        }

        private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            employees = eh.GetEmployees();
        }
    }
}
