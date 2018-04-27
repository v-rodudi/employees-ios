using EmployeesLibrary;

using Xamarin.Forms;

namespace EmployeesIOS
{
	public class MyMainPage : TabbedPage
	{
        private EmployeesHelper eh;

        public MyMainPage ()
        {
            eh = new EmployeesHelper("employees.json");

            Children.Add(new MainPage(eh));
            Children.Add(new AddEmployeePage(eh));
            Children.Add(new SearchPage(eh));
        }
	}
}