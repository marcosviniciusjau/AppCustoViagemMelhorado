using AppCustoViagemMelhorado.Model;
using AppCustoViagemMelhorado.View;
using Xamarin.Forms;

using System.Globalization;
using System.Threading;
using System.Collections.ObjectModel;


namespace AppCustoViagemMelhorado
{
    public partial class App : Application
    {
        public static ObservableCollection<Pedagio> ListaPedagios = new ObservableCollection<Pedagio>();
        public App()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            MainPage = new NavigationPage(new Dados());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}