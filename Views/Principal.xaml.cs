using calcivarS5B.Models;

namespace calcivarS5B.Views;

public partial class Principal : ContentPage
{
	public Principal()
	{
		InitializeComponent();
	}

    private int? selectedPersonId = null;

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        if (selectedPersonId.HasValue)
        {
            App.personRepo.UpdatePerson(selectedPersonId.Value, txtName.Text);
            lblStatus.Text = "Persona actualizada";
            selectedPersonId = null;
        }
        else
        {
            App.personRepo.AddNewPerson(txtName.Text);
            lblStatus.Text = "Persona agregada";
        }

        txtName.Text = string.Empty;
        RefreshPersonList();
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        RefreshPersonList();
    }

    private void RefreshPersonList()
    {
        List<Persona> persons = App.personRepo.GetAllPeople();
        listaPerson.ItemsSource = persons;
    }

    private void OnEditPersonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        int personId = (int)button.CommandParameter;

        var persona = App.personRepo.GetPersonById(personId);
        if (persona != null)
        {
            txtName.Text = persona.Name;
            selectedPersonId = personId; 
            lblStatus.Text = "Editando persona ID: " + personId;
        }
    }

    private void OnDeletePersonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        int personId = (int)button.CommandParameter;
        App.personRepo.DeletePerson(personId);
        lblStatus.Text = "Persona eliminada";
        RefreshPersonList();
    }
}