using calcivarS5B.Utils;

namespace calcivarS5B
{
    public partial class App : Application
    {
        public static PersonRepository personRepo;
        public App(PersonRepository personrepository)
        {
            InitializeComponent();

            MainPage = new Views.Principal();
            personRepo = personrepository;
        }
    }
}
