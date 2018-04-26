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

        public SearchPage (EmployeesHelper eh)
        {
            this.eh = eh;

            Title = "Search";

            var searchButton = new Button { Text = "Search" };

            searchButton.Clicked += SearchButton_Clicked;

            Content = new StackLayout {
                Children = {
                    searchField,
                    searchButton
                }
            };
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            var query = searchField.Text;

        }
    }
}