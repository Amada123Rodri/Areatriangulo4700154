namespace Areatriangulo4700154
{
    public partial class MainPage : ContentPage
    {
        int _editTrianguloId = 0;
        private readonly LocalDbService _dbService = new LocalDbService();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            double lado1 = double.Parse(lado1EntryField.Text);
            double lado2 = double.Parse(lado2EntryField.Text);
            double lado3 = double.Parse(lado3EntryField.Text);

            if (_editTrianguloId == 0)
            {
                await _dbService.Create(new Triangulo
                {
                    Lado1 = lado1,
                    Lado2 = lado2,
                    Lado3 = lado3
                });
            }
            else
            {
                await _dbService.Update(new Triangulo
                {
                    Id = _editTrianguloId,
                    Lado1 = lado1,
                    Lado2 = lado2,
                    Lado3 = lado3
                });
                _editTrianguloId = 0;
            }

            lado1EntryField.Text = string.Empty;
            lado2EntryField.Text = string.Empty;
            lado3EntryField.Text = string.Empty;

            ListView.ItemsSource = await _dbService.GetTriangulos();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var triangulo = (Triangulo)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
            switch (action)
            {
                case "Edit":
                    _editTrianguloId = triangulo.Id;
                    lado1EntryField.Text = triangulo.Lado1.ToString();
                    lado2EntryField.Text = triangulo.Lado2.ToString();
                    lado3EntryField.Text = triangulo.Lado3.ToString();
                    break;

                case "Delete":
                    await _dbService.Delete(triangulo);
                    ListView.ItemsSource = await _dbService.GetTriangulos();
                    break;
            }
        }
    }

}
