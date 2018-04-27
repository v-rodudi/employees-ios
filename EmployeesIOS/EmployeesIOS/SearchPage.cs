using EmployeesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace EmployeesIOS
{
    public class SearchPage : ContentPage
    {
        private Entry searchField = new Entry();
        private EmployeesHelper eh;
        private Label searchResult = new Label { IsVisible = false };

        public SearchPage (EmployeesHelper eh)
        {
            this.eh = eh;

            Title = "Search";

            var searchButton = new Button { Text = "Search" };

            searchButton.Clicked += SearchButton_Clicked;

            Content = new StackLayout {
                Children = {
                    searchField,
                    searchButton,
                    searchResult
                }
            };
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            var res = eh.GetEmployee(searchField.Text);
            searchResult.Text = res;
            searchResult.IsVisible = true;
        }
    }
}