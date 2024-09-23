using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;


namespace wysylanie_do_bazy_SQLite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {



            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=database.db;Version=3;";
            string im, naz;

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();


                string createTableQuery = @"CREATE TABLE IF NOT EXISTS osoby (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            imie TEXT NOT NULL,
                                            nazwisko TEXT NOT NULL
                                            );";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }


                //P

                im = imie.Text;
                naz = nazwisko.Text;


                string insertQuery = "INSERT INTO osoby (imie, nazwisko) VALUES (@imie, @nazwisko);";

                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@imie", im);
                    cmd.Parameters.AddWithValue("@nazwisko", naz);
                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("Dane zostały zapisane do bazy danych.");

                conn.Close();

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=database.db;Version=3;";

            using (SQLiteConnection conn1 = new SQLiteConnection(connectionString))
            {
                conn1.Open();

                string selectQuery = "SELECT * FROM osoby;";


                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn1))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                       

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string im1 = reader.GetString(1);
                            string na = reader.GetString(2);

                            wynik.Text += im1 + na + " ";

                        }
                    }
                }

            }

        }

    }
}