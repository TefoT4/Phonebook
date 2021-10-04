using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Phonebook.Domains;

namespace Phonebook.Web.Pages
{
    public partial class ContactListComponent
    {
        [Inject] 
        private HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Contacts = new List<Contact>();

            var contactList = await Http.GetFromJsonAsync<List<Contact>>("Phonebook");
            contactList.ForEach(contact => Contacts.Add(contact));
        }
        
        [CascadingParameter]
        public IModalService Modal { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        public List<Contact> Contacts { get; set; }

        private async void DeleteContact(int contactId)
        {
            var deleteResponse = await Http.DeleteAsync($"Phonebook/{contactId}");

            if (deleteResponse.IsSuccessStatusCode)
            {
                var isDeleted = await deleteResponse.Content.ReadFromJsonAsync<bool>();

                if (isDeleted)
                {
                    Contacts.Remove(Contacts.First(x => x.Id == contactId));
                    InvokeAsync(StateHasChanged);
                    ToastService.ShowSuccess("Contact Deleted.");
                }
                else
                {
                    ToastService.ShowError($"Error Deleting Contact.");
                }
            }
        }

        private async void AddContact()
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(Contact), new Contact());
            var modalReference = Modal.Show<ContactComponent>("Add Contact", parameters);
            var modalResult = await modalReference.Result;

            if (!modalResult.Cancelled)
            {
                var contact = (Contact)modalResult.Data;
                Contacts.Add(contact);
                InvokeAsync(StateHasChanged);
            }
        }

        private async void UpdateContact(int id)
        {
            var parameters = new ModalParameters();
            parameters.Add("IsUpdateCommand", true);
            parameters.Add(nameof(Contact), Contacts.First(x => x.Id == id));

            var modalReference = Modal.Show<ContactComponent>("Edit Contact", parameters);
            var modalResult = await modalReference.Result;

            if (!modalResult.Cancelled)
            {
                var contact = (Contact) modalResult.Data;
                Contacts.Remove(Contacts.First(x => x.Id == contact.Id));
                Contacts.Add(contact);
                InvokeAsync(StateHasChanged);
            }
        }
    }
}
