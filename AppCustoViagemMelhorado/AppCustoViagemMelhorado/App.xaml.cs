using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCustoViagemMelhorado.Helper;
using System.IO;

namespace AppCustoViagemMelhorado
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelperPedagio database;
        static SQLiteDatabaseHelperViagem database1;

        public static SQLiteDatabaseHelperPedagio Database
        {
            get
            {
                if (database == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), "arquivo.db3");

                    database = new SQLiteDatabaseHelperPedagio(path);
                }

                return database;
            }
        }

        public static SQLiteDatabaseHelperViagem Database1
        {
            get
            {
                if (database1 == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), "arquivo1.db3");

                    database1 = new SQLiteDatabaseHelperViagem(path);
                }

                return database1;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.DadosViagemMelhorada());
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

